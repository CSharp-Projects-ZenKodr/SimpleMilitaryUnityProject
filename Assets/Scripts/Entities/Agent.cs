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

        #region SerializeFields - Animation Related

        [SerializeField] protected AnimDataContainer _container = null;
        public AnimDataContainer Container => _container;
        
        #endregion

        #region SerializedFields - State Machine - States

        //[SerializeField] protected 

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
        protected virtual void Update() { }
        
        /// <summary>
        /// Realigns the animator gameobject transform with the parnte gameobject's transform
        /// </summary>
        protected virtual void OnAnimatorMove() { }

        #endregion
    }
}