using System;
using System.Collections.Generic;
using Entities;
using Entity_Systems.Finite_State_Machines.Helpers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines {
    public class StateMachine<T> where T : IState<State> {
        #region Private Readonly Fields - Machine Core

        private readonly Agent _agent;
        private readonly Dictionary<Priority, State> _statesDict;

        #endregion

        #region Properties

        private State _currentState;

        public State CurrentState {
            get => _currentState;
            set {
                if (value == _currentState) {
                    Debug.LogWarning("The next state is already assigned as the CurrentState.");

                    return;
                }

                if (_currentState !=null) _currentState.OnEnterState(_agent);
                
                _currentState = value;
                _currentState.OnEnterState(_agent);
            }
        }

        #endregion

        #region Constructor

        public StateMachine(Agent agent, List<State> states) {
            _agent = agent;
            _statesDict = new Dictionary<Priority, State>();

            InitializeStatesDictionary(states);
        }

        #endregion

        #region State Machine - System Handling

        /// <summary>
        /// Should be called in the Update loop
        /// If the current state needs to be updated each frame, IsTickable should return true
        /// </summary>
        public void InvokeStateTick() {
            if (CurrentState != null && CurrentState.IsTickable) {
                CurrentState.Tick(_agent);
            }
        }

        /// <summary>
        /// Forces the State Machine to invoke a new state
        /// Should be called on Awake() so an Agent has a baseline state to default to
        /// </summary>
        /// <param name="priority"></param>
        public void ForceNextState(Priority priority) {
            try {
                var state = GetState(priority);

                if (state != null) {
                    Debug.Log($"{_agent.name} was forced to a new state: {state.name}");
                    CurrentState = state;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
        
        /// <summary>
        /// Attempts to return a state from the _statesDict that matches key 'priorty'
        /// Will throw an exception if a state is not found matching 'priority'
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public State GetState(Priority priority) {
            try {
                return _statesDict[priority];
            }
            catch (Exception e) {
                Console.WriteLine(e + $" could not retrieve state from {_agent.name}");
                throw;
            }
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Populates the _statesDict dictionary with the key being the serialized 'Priority' of the state
        /// and the value being the State Scriptableobject
        /// </summary>
        /// <param name="states">List of states provided by the agent</param>
        private void InitializeStatesDictionary(List<State> states) {
            if (states == null) {
                Debug.LogError($"{_agent.name} needs to assign states to it's State List field.");

                return;
            }

            foreach (var state in states) {
                if (!_statesDict.ContainsKey(state.Priority)) {
                    _statesDict.Add(state.Priority, state);
                }
            }
        }
        
        #endregion
    }
}