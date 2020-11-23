using Animancer;
using Entity_Systems;
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

        #endregion

        #region Protected/Private Fields & Properties - Animation Related

        protected AnimationBrain _animationBrain;    // Handles all animations related to the agent
        
        #endregion

        #region SerializeFields - Animation Related

        [SerializeField] private AnimDataContainer _container = null;

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
            _animationBrain = new AnimationBrain(GetComponent<AnimancerComponent>(),
                _container,
                GetComponent<Animator>());
        }

        /// <summary>
        /// Called after Awake. Should put the agent in a valid state; i.e. 'enable' any dependecies
        /// </summary>
        protected virtual void OnEnable() {
            _animationBrain.Enable();
        }

        /// <summary>
        /// Exact opposite of OnEnable()
        /// </summary>
        protected virtual void OnDisable() {
            
        }

        /// <summary>
        /// Not currently being implemented
        /// </summary>
        protected virtual void Start() {
            
        }

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
        /// Updates the agent's locomotion animations
        /// </summary>
        private void Update() {
            _animationBrain.Update(_locomotion.GetCurrentSpeed());
        }
        
        /// <summary>
        /// Realigns the animator gameobject transform with the parnte gameobject's transform
        /// </summary>
        private void OnAnimatorMove() {
            transform.position += _animationBrain.QueryAnimatorDeltaPosition();
        }

        #endregion
    }
}