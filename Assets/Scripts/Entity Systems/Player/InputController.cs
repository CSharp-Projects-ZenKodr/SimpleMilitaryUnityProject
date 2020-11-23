using Entity_Systems.Player.InputControllerSubSystems;
using Interfaces;

namespace Entity_Systems.Player {
    public class InputController : IOnEnable, IOnDisable {
        #region Private Readonly Fields

        private readonly PlayerInputControlsRevised _inputControls;

        #endregion

        #region Public Readonly Fields

        public readonly PlayerActions PlayerActions;
        public readonly LocomotionActions LocomotionActions;
        public readonly InventoryActions InventoryActions;
        
        #endregion
        
        #region Constructor

        public InputController() {
            _inputControls = new PlayerInputControlsRevised();
            PlayerActions = new PlayerActions(_inputControls.Player);
            LocomotionActions = new LocomotionActions(_inputControls.Locomotion);
            InventoryActions = new InventoryActions(_inputControls.Inventory);
        }

        #endregion

        #region Initialization
        
        /// <summary>
        /// Enables the input controller
        /// </summary>
        public void Enable() {
            _inputControls.Enable();
        }

        /// <summary>
        /// Disables the input controller
        /// </summary>
        public void Disable() {
            _inputControls.Disable();
        }
        
        #endregion
    }
}