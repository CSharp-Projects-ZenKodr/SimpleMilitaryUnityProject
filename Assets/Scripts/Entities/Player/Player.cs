using Entity_Systems;
using Entity_Systems.Player.Handlers;
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
        
        #region Private Fields & Properties

        private PlayerInputControlsRevised _inputControls;
        private Combat _combat;
        private Equipment _equipment;

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
            
            _inputControls.Enable();
        }

        protected override void OnDisable() {
            base.OnDisable();
            
            _inputControls.Disable();
        }

        /// <summary>
        /// Handles creation of any specific systems that this agent needs
        /// </summary>
        protected override void CreateAgentSpecificDependencies() {
            _combat = new Combat();
            _equipment = new Equipment();
            _inputControls = new PlayerInputControlsRevised();
            _inputStateHandler = new InputStateHandler();
        }

        /// <summary>
        /// Will initialize or put agent specific systems into a valid and functional state
        /// </summary>
        protected override void InitializeAgentSpecificDependencies() {
            _inputControls.Player.SetCallbacks(this);
            _inputControls.Locomotion.SetCallbacks(this);
            _inputControls.Inventory.SetCallbacks(this);
        }

        #endregion
        
        #region Public Methods - Initializations - Interfaces
        
        public void SubscribeToSubSystemEvents() {
            _locomotion.SubToOnTargetReach(OnTargetReached);
        }
        
        #endregion

        #region Private Methods - Event Subscribers Via Interfaces
        
        /// <summary>
        /// Once pathfinding destination is reached, this method will set the animator speed to 'idle'
        /// </summary>
        private void OnTargetReached() {
            _locomotion.MaxSpeed = 2.0f;
            _locomotion.CanSearch = false;
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Will be called if a Player agent clicks on a terrain collider and has an accessible path to the
        /// RaycastHit point. Sets the animation to walk. If the agent is not walking, sets to running (implied)
        /// </summary>
        /// <param name="raycastHit">The raycast from where the mouse was clicked to the terrain collider</param>
        private void MoveToTerrainVector(RaycastHit raycastHit) {
            _locomotion.CanSearch = true;
            _locomotion.SetDestinationAndSearchPathNonNormalized(_pathing.GetNodeDestination(raycastHit.point));
        }
       
        #endregion
    }
}