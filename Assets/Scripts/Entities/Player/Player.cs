using Entity_Systems;
using Entity_Systems.Player;
using Interfaces;
using Items.Weapons;

namespace Entities.Player {
    /// <summary>
    /// Inherits the following classes:     Pathing     Locomotion     Raycasting     Skeleton     AnimationBrain
    /// </summary>
    public class Player : Agent, ISubscribeToSubSystemEvents {
        #region DEBUGGING FIELDS

        public ProjectileWeapon ProjectileWeapon;

        #endregion
        
        #region Private Fields & Properties

        private Combat _combat;
        private Equipment _equipment;
        private InputController _inputController;

        #endregion

        #region Protected Overrided Methods - Initializations

        protected override void Awake() {
            base.Awake();
            CreateAgentSpecificDependencies();
            InitializeAgentSpecificDependencies();
            SubscribeToSubSystemEvents();
        }

        protected override void OnEnable() {
            base.OnEnable();
            
            _inputController.Enable();
        }

        protected override void OnDisable() {
            base.OnDisable();
            
            _inputController.Disable();
        }

        /// <summary>
        /// Handles creation of any specific systems that this agent needs
        /// </summary>
        protected override void CreateAgentSpecificDependencies() {
            _combat = new Combat();
            _equipment = new Equipment();
            _inputController = new InputController();
        }

        /// <summary>
        /// Will initialize or put agent specific systems into a valid and functional state
        /// </summary>
        protected override void InitializeAgentSpecificDependencies() {

        }

        #endregion
        
        #region Public Methods - Initializations - Interfaces
        
        public void SubscribeToSubSystemEvents() {
            _inputController.PlayerActions.Actions.PrimaryClick.performed += _ =>
                _inputController.PlayerActions.OnPrimaryClick(_raycasting, _locomotion, _pathing);
            _inputController.LocomotionActions.Actions.Run.performed += _ =>
                _inputController.LocomotionActions.OnRun(_locomotion, _animationBrain);
        }
        
        #endregion
    }
}