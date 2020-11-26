using System;
using Entities;
using Entity_Systems.Finite_State_Machines.Helpers;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines.States {
    [Serializable]
    public abstract class State<T> : ScriptableObject, IState<T> where T : Agent {
        [SerializeField] protected Priority _priority;
        public Priority Priority => _priority;

        [SerializeField] protected bool _isTickable = false;
        public bool IsTickable => _isTickable;

        /// <summary>
        /// Always returns true unless overridden.
        /// </summary>
        public virtual bool CanEnterState(State<T> previousState) => true;

        /// <summary>
        /// Always returns true unless overridden.
        /// </summary>
        public virtual bool CanExitState(State<T> nextState) => true;

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
        /// <param name="agent">Agent to query statemachine</param>
        public virtual void Tick(T agent) { }
    }
}