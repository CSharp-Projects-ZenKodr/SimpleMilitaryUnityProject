using System;
using System.Collections.Generic;
using Entities;
using Entity_Systems.Finite_State_Machines.Helpers;
using Entity_Systems.Finite_State_Machines.StateAction;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States {
    [Serializable]
    public abstract class State<T> : ScriptableObject, IState<T> where T : Agent {
        [SerializeField] protected Priority _priority;
        public Priority Priority => _priority;

        [SerializeField] protected bool _isTickable = false;
        public bool IsTickable => _isTickable;

        public virtual List<StateAction<T>> ActionList { get; }

        /// <summary>
        /// Always returns true unless overridden.
        /// </summary>
        public virtual bool CanEnterState(T previousState) => true;

        /// <summary>
        /// Always returns true unless overridden.
        /// </summary>
        public virtual bool CanExitState(T nextState) => true;

        /// <summary>
        /// Can be overridden to define logic for when the state is entered
        /// </summary>
        public virtual void OnEnterState(T agent) {
            Debug.Log($"{agent.name} is entering {name}");
            if (_isTickable) 
                Debug.Log($"{name} is a tickable state");
        }

        /// <summary>
        /// Can be overridden to define logic for when the state is exited
        /// </summary>
        public virtual void OnExitState(T agent) {
            Debug.Log($"{agent.name} is leaving {name}");
        }

        /// <summary>
        /// Core method of a state; logic for processing and implementing the state goes through this method
        /// </summary>
        /// <param name="agent"></param>
        public virtual void Tick(T agent) {
            Debug.Log("Action List Count: " + ActionList.Count);
            if (ActionList == null) return;
            Debug.Log("Actionlist has been queried.");
            foreach (var action in ActionList) {
                action.Act(agent);
            }
        }
    }
}