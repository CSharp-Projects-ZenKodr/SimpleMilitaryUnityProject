namespace Entity_Systems.Player.Handlers {
    public class InputStateHandler {
        /// <summary>
        /// Tracks the current state of being crouched
        /// </summary>
        private bool _isCrouched = false;
        public bool IsCrouch {
            get => _isCrouched;
            set => _isCrouched = value;
        }
    }
}