using Animation;
using Helpers;
using Static_Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player {
    public partial class Player : PlayerInputControlsRevised.IPlayerActions, 
        PlayerInputControlsRevised.ILocomotionActions, 
        PlayerInputControlsRevised.IInventoryActions {
        #region Player General Actions

        /// <summary>
        /// Calls player input handler to raycast everything at the point of mouse click, with respect to camera
        /// and returns a nullable RaycastHit
        /// The method then decides what the next course of action should be
        /// At the moment, the only decisions are to do:
        ///     Nothing & return
        ///     Pass a target location to the seeker
        /// When left-mouse-button is pressed
        /// </summary>
        /// <param name="context">Delegate callback</param>
        public void OnPrimaryClick(InputAction.CallbackContext context) {
            Debug.Log("PrimaryClickCalled");
            var raycastHit = _raycasting.GetRaycastOnClick();
            
            if (raycastHit == null) return;
            var castedRaycast = (RaycastHit) raycastHit;
            
            if (castedRaycast.collider.CompareTag(TagHelper.TerrainTag)) {
                MoveToTerrainVector(castedRaycast);
            }
        }
        
        #endregion

        #region Locomotion

        /// <summary>
        /// Handles jumping logic for the player agent
        /// </summary>
        /// <param name="context">Delegate callback</param>
        public void OnJump(InputAction.CallbackContext context) {
            _animationHandler.TriggerBool(AnimationParameterStatics.Jump);
        }

        /// <summary>
        /// Can implement a system for keyboard movement
        /// </summary>
        /// <param name="context"></param>
        public void OnMovement(InputAction.CallbackContext context) {

        }

        #endregion

        #region Inventory
        
        /// <summary>
        /// Equips a new projectile weapon scriptableobject to the ProjectileWeapon field
        /// Should be used with some sort of button or key
        /// This method is used more for debug in it's current state
        /// See <see cref="Joints"/>
        /// </summary>
        public void OnInventoryOne(InputAction.CallbackContext context) {
            _equipment.EquipProjectileWeapon(
                _skeleton.GetJointTransform(Joints.RightHand),
                ProjectileWeapon);
        }

        /// <summary>
        /// Currently set to throw a grenade; placeholder
        /// </summary>
        /// <param name="context"></param>
        public void OnInventoryTwo(InputAction.CallbackContext context) {
            _animationHandler.SetAnimatorTrigger("ThrowGrenade");
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="context"></param>
        public void OnInventoryThree(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="context"></param>
        public void OnInventoryFour(InputAction.CallbackContext context) {
        }

        #endregion

    }
}