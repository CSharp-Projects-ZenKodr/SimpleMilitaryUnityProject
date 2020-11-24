using Entity_Systems.Player.InputControllerSubSystems;
using Generics;
using UnityEngine;

namespace Entity_Systems {
    public class InputBrain : Singleton<MonoBehaviour> {
        #region Private Fields

        private PlayerInputControlsRevised _inputControls;
        private PlayerActions _playerActions;
        private LocomotionActions _locomotionActions;
        private InventoryActions _inventoryActions;

        #endregion

        #region Public Fields

        public PlayerActions PlayerActions => _playerActions;
        public LocomotionActions LocomotionActions => _locomotionActions;
        public InventoryActions InventoryActions => _inventoryActions;
        
        #endregion
        
        #region Mono Methods

        private void Awake() {
            CreateDependencies();
        }

        /// <summary>
        /// Enables the input controller
        /// </summary>
        private void OnEnable() {
            _inputControls.Enable();
        }

        /// <summary>
        /// Disables the input controller
        /// </summary>
        private void OnDisable() {
            _inputControls.Disable();
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Creates all dependencies for input control
        /// </summary>
        protected override void CreateDependencies() {
            _inputControls = new PlayerInputControlsRevised();
            _playerActions = new PlayerActions(_inputControls.Player);
            _locomotionActions = new LocomotionActions(_inputControls.Locomotion);
            _inventoryActions = new InventoryActions(_inputControls.Inventory);
        }
        
        #endregion
    }
}