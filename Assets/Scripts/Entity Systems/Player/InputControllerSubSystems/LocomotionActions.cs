using Animation;
using Entity_Systems.SubSystems.Animation.Helpers;

namespace Entity_Systems.Player.InputControllerSubSystems {
    public class LocomotionActions {
        #region Public Readonly Fields

        public readonly PlayerInputControlsRevised.LocomotionActions Actions;

        #endregion

        #region Constructor

        public LocomotionActions(PlayerInputControlsRevised.LocomotionActions actions) {
            Actions = actions;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles on Player running should handle. OnRun is invoked during call context callback phases:
        /// Started, Performed, and Canceled. Logic is required to differentiate between each phase.
        /// </summary>
        /// <param name="pLocomotion">The player agent's locomotion system</param>
        /// <param name="pAnimationBrain">The player agent's Animation Brain</param>
        public void OnRun(Locomotion pLocomotion, AnimationBrain pAnimationBrain) {
            pLocomotion.MaxSpeed = 4.0f;
            pAnimationBrain.Animate(out var state, AnimId.Run, 0.1f);
        }

        #endregion
        /*
         *
         *  #region Locomotion

        /// <summary>
        /// Handles jumping logic for the player agent
        /// </summary>
        /// <param name="context">Delegate callback</param>
        public void OnJump(InputAction.CallbackContext context) {
            //_animationBrain.TriggerBool(AnimationParameterStatics.Jump);
        }

        /// <summary>
        /// Can implement a system for keyboard movement
        /// </summary>
        /// <param name="context"></param>
        public void OnMovement(InputAction.CallbackContext context) {
            // Need to define implementation at a later date
        }

            
        }

        #endregion
         */
    }
}