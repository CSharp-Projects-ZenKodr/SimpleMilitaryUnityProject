using Animancer;
using Entity_Systems;
using Interfaces;
using Items.Weapons;
using UnityEngine;

namespace Entities.Player {
    /// <summary>
    /// Inherits the following classes:     Pathing     Locomotion     Raycasting     Skeleton     AnimationBrain
    /// </summary>
    public class Player : Agent, ISubscribeToSubSystemEvents {
        #region DEBUGGING FIELDS

        public ProjectileWeapon ProjectileWeapon;

        #endregion

        #region Properties

        private AnimationBrain _animationBrain;
        public AnimationBrain AnimationBrain => _animationBrain;

        #endregion
        
        #region Private Fields & Properties

        private Combat _combat;
        private Equipment _equipment;
        private InputBrain _inputBrain;

        #endregion

        #region Protected Overrided Methods - Initializations

        protected override void Awake() {
            base.Awake();
            CreateAgentSpecificDependencies();
            InitializeAgentSpecificDependencies();
        }

        protected override void OnEnable() {
            base.OnEnable();
            
            _animationBrain.Enable();
            _inputBrain = FindObjectOfType<InputBrain>();
            SubscribeToSubSystemEvents();
        }

        protected override void OnDisable() {
            base.OnDisable();
        }

        /// <summary>
        /// Handles creation of any specific systems that this agent needs
        /// </summary>
        protected override void CreateAgentSpecificDependencies() {
            _combat = new Combat();
            _equipment = new Equipment();
            _animationBrain = new AnimationBrain(GetComponent<AnimancerComponent>(),
                _container,
                GetComponent<Animator>());
        }

        /// <summary>
        /// Will initialize or put agent specific systems into a valid and functional state
        /// </summary>
        protected override void InitializeAgentSpecificDependencies() { }

        #endregion
        
        #region Public Methods - Initializations - Interfaces
        
        public void SubscribeToSubSystemEvents() {
            _inputBrain.PlayerActions.Actions.PrimaryClick.performed += _ =>
                _inputBrain.PlayerActions.OnPrimaryClick(_raycasting, _locomotion, _pathing);
            _inputBrain.LocomotionActions.Actions.Run.performed += _ =>
                _inputBrain.LocomotionActions.OnRun(_locomotion, _animationBrain);
        }

        #endregion

        #region Override Methods - Protected

        /// <summary>
        /// See <see cref="AnimationBrain"/>
        /// </summary>
        protected override void OnAnimatorMove() {
            base.OnAnimatorMove();
            
            transform.position += _animationBrain.QueryController.QueryAnimatorDeltaPosition();
        }

        /// <summary>
        /// See <see cref="AnimationBrain"/>
        /// </summary>
        protected override void Update() {
            base.Update();
            var speed = _locomotion.GetCurrentSpeed();
            
            _animationBrain.LocomotionController.UpdateLocomotionAnimation(speed, _container);

        }
        
        #endregion
    }
}