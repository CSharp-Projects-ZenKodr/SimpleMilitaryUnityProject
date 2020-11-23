namespace Entity_Systems.Player.InputControllerSubSystems {
    public class InventoryActions {
        #region Public Readonly Fields

        public readonly PlayerInputControlsRevised.InventoryActions Actions;

        #endregion

        #region Constructor

        public InventoryActions(PlayerInputControlsRevised.InventoryActions actions) {
            Actions = actions;
        }
        
        #endregion
        
        /*
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
        /// **********************************NOT YET IMPLEMENTED
        /// </summary>
        /// <param name="context"></param>
        public void OnInventoryTwo(InputAction.CallbackContext context) {

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

        #endregion*/
    }
}