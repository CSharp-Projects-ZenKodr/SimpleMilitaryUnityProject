using Entities;
using Entity_Systems.Finite_State_Machines.Helpers;
using Entity_Systems.Finite_State_Machines.States;
using UnityEngine;

namespace Entity_Systems.Finite_State_Machines {
    public abstract class State : ScriptableObject, IState<State> {
        [SerializeField] protected Priority _priority;
        public Priority Priority => _priority;

        [SerializeField] protected bool _isTickable = false;
        public bool IsTickable => _isTickable;
        
        /// <summary>
        /// Always returns true unless overridden.
        /// </summary>
        public virtual bool CanEnterState(State previousState) => true;

        /// <summary>
        /// Always returns true unless overridden.
        /// </summary>
        public virtual bool CanExitState(State nextState) => true;

        /// <summary>
        /// Can be overridden to define logic for when the state is entered
        /// </summary>
        public virtual void OnEnterState(Agent agent) {
            Debug.Log($"{agent.name} is entering {name}");
            if (_isTickable) Debug.Log($"{name} is a tickable state");
        }

        /// <summary>
        /// Can be overridden to define logic for when the state is exited
        /// </summary>
        public virtual void OnExitState(Agent agent) {
            Debug.Log($"{agent.name} is leaving {name}");
        }

        public virtual void Tick(Agent agent) { }
    }
}