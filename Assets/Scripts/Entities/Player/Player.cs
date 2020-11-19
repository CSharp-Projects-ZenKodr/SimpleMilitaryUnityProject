using Animation;
using Controls;
using Entity_Systems;
using Helpers;
using Interfaces;
using Items.Weapons;
using Static_Helpers;
using UnityEngine;

namespace Entities.Player {
    /// <summary>
    /// Inherits the following classes:     Pathing     Locomotion     Raycasting     Skeleton     AnimationHandler
    /// </summary>
    public class Player : Agent, ISubscribeToSubSystemEvents {
        #region DEBUGGING FIELDS

        public ProjectileWeapon ProjectileWeapon;

        #endregion
        
        #region Private Fields & Properties

        private PlayerInputHandler _playerInputHandler;
        private Combat _combat;
        private Equipment _equipment;

        #endregion

        #region Events

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
            
            _playerInputHandler.Enable();
        }

        protected override void OnDisable() {
            base.OnDisable();
            
            _playerInputHandler.Disable();
        }

        protected override void CreateAgentSpecificDependencies() {
            _playerInputHandler = new PlayerInputHandler();
            _combat = new Combat();
            _equipment = new Equipment();
        }

        protected override void InitializeAgentSpecificDependencies() {
            _playerInputHandler.Awake();
        }

        #endregion
        
        #region Public Methods - Initializations - Interfaces
        
        public void SubscribeToSubSystemEvents() {
            _playerInputHandler.PlayerInputControls.Player.ClickToMove.performed += callback => OnClick();
            _playerInputHandler.PlayerInputControls.Player.EquipProjectileWeapon.performed +=
                callback => EquipProjectileWeapon();
            _locomotion.GetAIPath().onTargetReached += OnTargetReached;
        }
        
        #endregion

        #region Private Methods - Event Subscribers

        /// <summary>
        /// Calls player input handler to raycast everything at the point of mouse click, with respect to camera
        /// and returns a nullable RaycastHit
        /// The method then decides what the next course of action should be
        /// At the moment, the only decisions are to do:
        ///     Nothing & return
        ///     Pass a target location to the seeker
        /// </summary>
        private void OnClick() {
            var raycastHit = _raycasting.GetRaycastOnClick();
            
            if (raycastHit == null) return;
            var castedRaycast = (RaycastHit) raycastHit;
            
            if (castedRaycast.collider.CompareTag(TagHelper.TerrainTag)) {
                MoveToTerrainVector(castedRaycast);
            }
        }

        /// <summary>
        /// Once pathfinding destination is reached, this method will set the animator speed to 'idle'
        /// </summary>
        private void OnTargetReached() {
            _animationHandler.SetLocomotionAnimation(AnimationSpeedStates.Idle);
            _locomotion.GetAIPath().canSearch = false;
        }

        /// <summary>
        /// Equips a new projectile weapon scriptableobject to the ProjectileWeapon field
        /// Should be used with some sort of button or key
        /// This method is used more for debug in it's current state
        /// See <see cref="Joints"/>
        /// </summary>
        private void EquipProjectileWeapon() {
                _equipment.EquipProjectileWeapon(
                    _skeleton.GetJointTransform(Joints.RightHand), 
                    ProjectileWeapon);
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Will be called if a Player agent clicks on a terrain collider and has an accessible path to the
        /// RaycastHit point
        /// </summary>
        /// <param name="raycastHit">The raycast from where the mouse was clicked to the terrain collider</param>
        private void MoveToTerrainVector(RaycastHit raycastHit) {
            _locomotion.GetAIPath().canSearch = true;
            _locomotion.SetDestinationAndSearchPath(_pathing.GetNodeDestination(raycastHit.point));
            _animationHandler.SetAnimationInt(AnimationIdleStates.Normal);
            _animationHandler.SetLocomotionAnimation(AnimationSpeedStates.Walk);
        }

        //private void Jump 
        private void Jump() {
            
        }
        
        #endregion
    }
}