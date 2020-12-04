using MonoManagers.Root;
using UnityEngine;

namespace Systems.Input {
    public class InputBrain : Singleton<MonoBehaviour> {
        #region Private Fields

        private PlayerInputControlsRevised _inputControls;
        private PlayerInputControlsRevised.PlayerActions _playerActions;
        private PlayerInputControlsRevised.LocomotionActions _locomotionActions;
        private PlayerInputControlsRevised.InventoryActions _inventoryActions;

        #endregion

        #region Public Fields 

        public PlayerInputControlsRevised.PlayerActions PlayerActions => _playerActions;
        public PlayerInputControlsRevised.LocomotionActions LocomotionActions => _locomotionActions;
        public PlayerInputControlsRevised.InventoryActions InventoryActions => _inventoryActions;
        
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
            _playerActions = _inputControls.Player;
            _locomotionActions = _inputControls.Locomotion;
            _inventoryActions = _inputControls.Inventory;
        }
        
        #endregion
    }
}