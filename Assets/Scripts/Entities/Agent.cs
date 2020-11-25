using System.Collections.Generic;
using Animancer;
using Entity_Systems;
using Entity_Systems.Finite_State_Machines;
using Entity_Systems.Finite_State_Machines.Helpers;
using Entity_Systems.SubSystems.Animation.AnimationDataContainers;
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
        protected StateMachine<State> _stateMachine;

        #endregion
        
        #region SerializeFields & Properties - Animation Related
        
        [SerializeField] protected AnimDataContainer _container = null;
        [SerializeField] protected List<State> _states;

        public Raycasting Raycasting => _raycasting;
        public Pathing Pathing => _pathing;
        public Locomotion Locomotion => _locomotion;
        public Skeleton Skeleton => _skeleton;
        public AnimDataContainer Container => _container;
        public AnimationBrain AnimationBrain => _animationBrain;
        
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
            _stateMachine = new StateMachine<State>(this, _states);
        }

        /// <summary>
        /// Called after Awake. Should put the agent in a valid state; i.e. 'enable' any dependecies
        /// </summary>
        protected virtual void OnEnable() {
           _stateMachine.ForceNextState(Priority.Idle);
        }

        /// <summary>
        /// Exact opposite of OnEnable()
        /// </summary>
        protected virtual void OnDisable() {
        }

        #endregion

        #region Update Loop

        protected virtual void Update() {
            _stateMachine.InvokeStateTick();
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