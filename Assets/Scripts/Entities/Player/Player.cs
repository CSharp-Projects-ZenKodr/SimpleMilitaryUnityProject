using Entity_Systems;
using Entity_Systems.Finite_State_Machines.Helpers;
using Entity_Systems.SubSystems.Animation.Helpers;
using Helpers;
using Interfaces;
using Items.Weapons;
using Static_Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

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
        private InputBrain _inputBrain;

        #endregion

        #region Properties

        public Combat Combat => _combat;
        public Equipment Equipment => _equipment;
        public InputBrain InputBrain => _inputBrain;
        
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
        }

        /// <summary>
        /// Will initialize or put agent specific systems into a valid and functional state
        /// </summary>
        protected override void InitializeAgentSpecificDependencies() { }

        #endregion
        
        #region Initializations - Interfaces - System Event Subscription
        
        public void SubscribeToSubSystemEvents() {
            _inputBrain.PlayerActions.PrimaryClick.performed += InteractWithPrimaryClick;
            _inputBrain.LocomotionActions.Run.performed += InteractWithRun;
            _inputBrain.InventoryActions.InventoryOne.performed += InteractWithInventoryOne;
            
            _locomotion.RegisterDelegateToAiOnPathComplete(InteractWithOnDestinationReached);
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
        
        #endregion
        
        #region Private Methods - Utilization of Agent's Systems - Input Brain

        // *************************************************************************************Input Brain Interactions
        /// <summary>
        /// Logic for when the 'left-mouse' button is pressed
        /// </summary>
        /// <param name="context"></param>
        internal void InteractWithPrimaryClick(InputAction.CallbackContext context) {
            var raycastHit = _raycasting.GetRaycastOnClick();
            
            if (raycastHit == null) return;
            var castedRaycast = (RaycastHit) raycastHit;
            
            if (castedRaycast.collider.CompareTag(TagHelper.TerrainTag)) {
                HandleRaycastingTerrain(castedRaycast);
            }
        }

        /// <summary>
        /// Logic for when the 'run' button is pressed
        /// </summary>
        internal void InteractWithRun(InputAction.CallbackContext context) {
            _locomotion.Speed = 4.0f;
            _locomotion.IsRunning = !_locomotion.IsRunning;
            _stateMachine.CurrentState = _stateMachine.GetState(Priority.Run);
        }

        /// <summary>
        /// Logic for when the player reaches their target destination, A* Pathfinding
        /// </summary>
        internal void InteractWithOnDestinationReached() {
            _locomotion.Speed = 0.0f;
            _locomotion. CanSearch = false;
            _locomotion.IsRunning = false;
            _stateMachine.CurrentState = _stateMachine.GetState(Priority.Idle);
        }
        
        /// <summary>
        /// Logic for when the 'crouch' button is pressed
        /// </summary>
        /// <param name="context"></param>
        internal void InteractWithCrouch(InputAction.CallbackContext context) { }

        internal void InteractWithInventoryOne(InputAction.CallbackContext context) {
            _equipment.EquipProjectileWeapon(
                _skeleton.GetJointTransform(Joints.RightHand),
                ProjectileWeapon);
        }

        #endregion

        #region Private Methods - Helpers

        /// <summary>
        /// Logic for handling a raycast collider with terrain
        /// Determines if locomotion is running or walk and sets speed/animation accordingly
        /// </summary>
        /// <param name="castedRaycast"></param>
        private void HandleRaycastingTerrain(RaycastHit castedRaycast) {
            if (_locomotion.IsRunning) {
                _stateMachine.CurrentState = _stateMachine.GetState(Priority.Run);
                _locomotion.Speed = 4.0f;
            }
            else {
                _stateMachine.CurrentState = _stateMachine.GetState(Priority.Walk);
                _locomotion.Speed = 2.0f;
            }

            _locomotion.CanSearch = true;
            _locomotion.SetDestinationAndSearchPathNonNormalized(_pathing.GetNodeDestination(castedRaycast.point));
        }

        #endregion
    }
}