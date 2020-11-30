using System.Collections.Generic;
using Entity_Systems;
using Entity_Systems.Finite_State_Machines.Helpers;
using Entity_Systems.Finite_State_Machines.Machine;
using Entity_Systems.Finite_State_Machines.States.Wrappers;
using Entity_Systems.SubSystems.Audio.Brains;
using Entity_Systems.SubSystems.Audio.Data_Containers;
using Helpers;
using Interfaces;
using Interfaces.States;
using Items.Weapons;
using Static_Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Human {
    /// <summary>
    /// Inherits the following classes:     Pathing     Locomotion     Raycasting     Skeleton     AnimationBrain
    /// </summary>
    public class Human : Agent, ISubscribeToSubSystemEvents, IStateWrapper<StateWrapperHuman> {
        #region DEBUGGING FIELDS

        public ProjectileWeapon ProjectileWeapon;

        #endregion
        
        #region Private Fields - System Related

        private Combat _combat;
        private Equipment _equipment;
        private InputBrain _inputBrain;
        private HumanAudioBrain _audioBrain;
        private HumanStateMachine _machine;
        [SerializeField] private HumanAudioDataContainer _audioContainer;
        [SerializeField] private List<StateWrapperHuman> _states;

        #endregion

        #region Public Fields - System Realted

        public HumanAudioBrain AudioBrain => _audioBrain;
        public HumanAudioDataContainer AudioContainer => _audioContainer;

        #endregion

        #region Private Fields - Human Related

        private bool _isLMBTrigger = false;

        #endregion

        #region Properties

        public Combat Combat => _combat;
        public Equipment Equipment => _equipment;
        public InputBrain InputBrain => _inputBrain;
        public HumanStateMachine Machine => _machine;
        public List<StateWrapperHuman> States => _states; 
        
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
            _machine.ForceNextState(Priority.Idle);
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
            _audioBrain = new HumanAudioBrain(this, _audioSource);
            _machine = new HumanStateMachine(this, _states);
        }

        /// <summary>
        /// Will initialize or put agent specific systems into a valid and functional state
        /// </summary>
        protected override void InitializeAgentSpecificDependencies() { }

        #endregion

        #region Update Loop

        /// <summary>
        /// Invokes the Machine's current state actions, if allowable
        /// </summary>
        private void Update() {
            if (Machine.CurrentState.IsTickable) {
                Machine.InvokeStateTick(this);
            }
        }

        /// <summary>
        /// Handles raycasting and pathing while the primary button is 'actuated'
        /// </summary>
        private void FixedUpdate() {
            if (_isLMBTrigger) {
                HandlePrimaryClickInputHeld();
            }
        }

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
        /// Sets '_isLMBTrigger' to true if primary button is press, remains true if pressed and held, sets to false
        /// when the button is released
        /// </summary>
        /// <param name="context"></param>
        internal void InteractWithPrimaryClick(InputAction.CallbackContext context) {
            _isLMBTrigger = !_isLMBTrigger;
        }

        /// <summary>
        /// Logic for when the 'run' button is pressed
        /// </summary>
        internal void InteractWithRun(InputAction.CallbackContext context) {
            Machine.CurrentState = Machine.GetState(Priority.Run);
        }

        /// <summary>
        /// Logic for when the player reaches their target destination, A* Pathfinding
        /// </summary>
        internal void InteractWithOnDestinationReached() {
            Machine.CurrentState = Machine.GetState(Priority.Idle);
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
                Machine.CurrentState = Machine.GetState(Priority.Run);
            }
            else {
                Machine.CurrentState = Machine.GetState(Priority.Walk);
            }

            _locomotion.CanSearch = true;
            _locomotion.SetDestinationAndSearchPathNonNormalized(_pathing.GetNodeDestination(castedRaycast.point));
        }

        /// <summary>
        /// Is invoked if the primary button was actuated or is continuously invoked if it is held
        /// </summary>
        private void HandlePrimaryClickInputHeld() {
            var raycastHit = _raycasting.GetRaycastOnClick();
            
            if (raycastHit == null) return;
            var castedRaycast = (RaycastHit) raycastHit;
            
            if (castedRaycast.collider.CompareTag(TagHelper.TerrainTag)) {
                HandleRaycastingTerrain(castedRaycast);
            }
        }
        
        #endregion
    }
}