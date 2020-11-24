using System;
using System.Collections.Generic;
using Animancer;
using Animancer.FSM;
using Entity_Systems;
using Entity_Systems.SubSystems.Animation.AnimationDataContainers;
using Finite_State_Machines;
using Pathfinding;
using UnityEngine;

namespace Entities {
    #region Required Mono Components
    
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(RaycastModifier))]
    [RequireComponent(typeof(SimpleSmoothModifier))]
    [RequireComponent(typeof(AnimancerComponent))]
    [RequireComponent(typeof(Animator))]

    #endregion
    
    public abstract class Agent : MonoBehaviour {
        #region Protected Fields & Propreties - Core Systems

        protected Pathing _pathing;    // Handles Agent pathing & navigation
        protected Raycasting _raycasting;    // Handles all raycasting required by agent
        protected Locomotion _locomotion;    // Controls speed, direction, location, locomotion, etc.
        protected Skeleton _skeleton;    // Provides access to the join system of the agent's prefab model
        protected AnimationBrain _animationBrain;

        #endregion

        #region SerializeFields & Properties - Animation Related
        
        [SerializeField] protected AnimDataContainer _container = null;
        public AnimDataContainer Container => _container;
        
        #endregion

        #region SerializedFields - State Machine

        [SerializeField] private List<AgentState> _agentStates;
        public StateMachine<AgentState> StateMachine { get; private set; }

        #endregion

        #region Delegate Definitions

        private Action _forceEnterIdleState;    // Every Agent base animation state should start out in 'Idle'

        #endregion
        
        #region Protected Virtual Methods

        /// <summary>
        /// Gets all agent's Monobehaviour components & creates new instances of required systems
        /// </summary>
        protected virtual void Awake() {
            var tr = GetComponent<Transform>();
            
            _pathing = new Pathing(
                GetComponent<Seeker>());
            _raycasting = new Raycasting(Camera.main);
            _locomotion = new Locomotion(tr, GetComponent<AIPath>());
            _skeleton = new Skeleton(gameObject);
            _animationBrain = new AnimationBrain(GetComponent<AnimancerComponent>(), _container, GetComponent<Animator>());
            
            StateMachine = new StateMachine<AgentState>();
            StateMachine.ForceSetState(_agentStates[0]);            
        }

        /// <summary>
        /// Called after Awake. Should put the agent in a valid state; i.e. 'enable' any dependecies
        /// </summary>
        protected virtual void OnEnable() {
        }

        /// <summary>
        /// Exact opposite of OnEnable()
        /// </summary>
        protected virtual void OnDisable() {
            
        }

        #endregion

        #region Protected Abstract Methods

        
        /// <summary>
        /// Handles population of any dependencies that are specific to that agent
        /// </summary>
        protected abstract void CreateAgentSpecificDependencies();

        /// <summary>
        /// Initializes any agent specific dependencies (systems) that were created
        /// Should put the system into a valid state
        /// </summary>
        protected abstract void InitializeAgentSpecificDependencies();

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Realigns the animator gameobject transform with the parnte gameobject's transform
        /// </summary>
        protected virtual void OnAnimatorMove() { }

        #endregion
    }
}