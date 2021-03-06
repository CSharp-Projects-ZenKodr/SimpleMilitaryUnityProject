﻿using Systems.Animation;
using Systems.SubSystems.Data_Containers;
using Animancer;
using Entity_Systems;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities {
    #region Required Mono Components
    
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(RaycastModifier))]
    [RequireComponent(typeof(SimpleSmoothModifier))]
    [RequireComponent(typeof(AnimancerComponent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]

    #endregion
    
    public abstract class Agent : MonoBehaviour {
        #region Systems

        protected Pathing _pathing;    // Handles Agent pathing & navigation
        public Pathing Pathing => _pathing;
        
        protected Raycasting _raycasting;    // Handles all raycasting required by agent
        public Raycasting Raycasting => _raycasting;
        
        protected Locomotion _locomotion;    // Controls speed, direction, location, locomotion, etc.
        public Locomotion Locomotion => _locomotion;
        
        protected Skeleton _skeleton;    // Provides access to the join system of the agent's prefab model
        public Skeleton Skeleton => _skeleton;

        #endregion

        #region Brains

        protected AnimationBrain _animationBrain;
        public AnimationBrain AnimationBrain => _animationBrain;

        #endregion

        #region Monobehaviour Components

        protected AudioSource _audioSource;

        #endregion

        #region Containers

        [FormerlySerializedAs("animationDataContainer")] [FormerlySerializedAs("_animDataContainer")] [FormerlySerializedAs("_container")] [SerializeField] 
        protected AnimationDataContainer _animationDataContainer = null;
        public AnimationDataContainer AnimationDataContainer => _animationDataContainer;

        [SerializeField] protected AudioDataContainer _audioDataContainer;
        public AudioDataContainer AudioDataContainer => _audioDataContainer;

        #endregion

        #region Monobehaviour Methods

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
            _animationBrain = new AnimationBrain(GetComponent<AnimancerComponent>(), _animationDataContainer, GetComponent<Animator>());
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Called after Awake. Should put the agent in a valid state; i.e. 'enable' any dependecies
        /// </summary>
        protected virtual void OnEnable() { }

        /// <summary>
        /// Exact opposite of OnEnable()
        /// </summary>
        protected virtual void OnDisable() { }

        #endregion
        
        #region Abstract Methods
        
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