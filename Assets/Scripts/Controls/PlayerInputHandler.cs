using Interfaces;

namespace Controls {
    public class PlayerInputHandler : IOnAwake, IOnEnable, IOnDisable {
        #region Public Fields & Properties

        public PlayerInputControlsRevised PlayerInputControls { get; private set; }

        #endregion
       
        #region Private Fields & Properties


        #endregion

        #region Constructor

        public PlayerInputHandler() {
            PlayerInputControls = new PlayerInputControlsRevised();
        }

        #endregion

        #region Public Methods


        #endregion

        #region Interface Methods

        public void Awake() {
            
        }
        
        public void Enable() {
            PlayerInputControls.Enable();
        }

        public void Disable() {
            PlayerInputControls.Disable();
        }

        #endregion

        #region Private Methods
        

        #endregion
    }
}
