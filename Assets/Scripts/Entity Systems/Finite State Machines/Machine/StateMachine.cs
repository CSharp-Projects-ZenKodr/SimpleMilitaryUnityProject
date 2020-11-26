using System;
using System.Collections.Generic;
using Entities;
using Entity_Systems.Finite_State_Machines.Helpers;
using Entity_Systems.Finite_State_Machines.States;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.Machine {
    public abstract class StateMachine<T> where T : Agent {
        #region Private Readonly Fields - Machine Core

        private readonly T _agent;
        private readonly Dictionary<Priority, State<T>> _statesDict;

        #endregion

        #region Properties

        private State<T> _currentState;

        public virtual State<T> CurrentState {
            get => _currentState;
            set {
                if (_currentState == null) {
                    _currentState = value;
                    _currentState.OnEnterState(_agent);
                }
                
                if (value == _currentState) {
                    return;
                }

                if (_currentState.CanExitState(value) && _currentState != null) {
                    _currentState.OnExitState(_agent);
                    _currentState = value;
                    _currentState.OnEnterState(_agent);
                }
                else {
                    Debug.Log($"{value.name} was already set to the current state.");
                }
            }
        }

        #endregion

        #region Constructor

        protected StateMachine(T agent, IReadOnlyCollection<State<T>> states) {
            _agent = agent;
            _statesDict = new Dictionary<Priority, State<T>>();

            InitializeStatesDictionary(states);
        }

        #endregion

        #region State Machine - System Handling - Abstract & Virtual Methods

        /// <summary>
        /// Should be called in the Update loop
        /// If the current state needs to be updated each frame, IsTickable should return true
        /// </summary>
        public abstract void InvokeStateTick(T agent);

        /// <summary>
        /// Forces the State Machine to invoke a new state
        /// Should be called on Awake() so an Agent has a baseline state to default to
        /// </summary>
        /// <param name="priority"></param>
        public virtual void ForceNextState(Priority priority) {
            try {
                if (_statesDict.ContainsKey(priority)) {
                    CurrentState = _statesDict[priority];
                    
                    Debug.Log($"{_agent.name} was forced into the state {CurrentState.name}");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e + $": try to force a state change but state was not found in {_agent.name}'s dictionary");
                throw;
            }
        }
        
        /// <summary>
        /// Attempts to return a state from the _statesDict that matches key 'priorty'
        /// Will throw an exception if a state is not found matching 'priority'
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public virtual State<T> GetState(Priority priority) {
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
        private void InitializeStatesDictionary(IReadOnlyCollection<State<T>> states) {
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