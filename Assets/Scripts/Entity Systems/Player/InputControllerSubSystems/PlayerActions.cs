using Interfaces.Animations;
using Static_Helpers;
using UnityEngine;

namespace Entity_Systems.Player.InputControllerSubSystems {
    public class PlayerActions : IOnPrimaryClick, IOnCrouch {
        #region Public Readonly Fields

        public PlayerInputControlsRevised.PlayerActions Actions;

        #endregion

        #region Constructor

        public PlayerActions(PlayerInputControlsRevised.PlayerActions actions) {
            Actions = actions;
        }

        #endregion
        
        #region Player Primary Action Methods - Public

        /// <summary>
        /// Calls player input handler to raycast everything at the point of mouse click, with respect to camera
        /// and returns a nullable RaycastHit
        /// The method then decides what the next course of action should be
        /// At the moment, the only decisions are to do:
        ///     Nothing & return
        ///     Pass a target location to the seeker
        /// When left-mouse-button is pressed
        /// </summary>
        /// <param name="pRaycasting">The Player's raycasting system</param>
        /// <param name="pLocomotion">The Player's locomotion system</param>
        /// <param name="pPathing">The Player's pPathing system</param>
        public void OnPrimaryClick(Raycasting pRaycasting, Locomotion pLocomotion, Pathing pPathing) {
            var raycastHit = pRaycasting.GetRaycastOnClick();
            
            if (raycastHit == null) return;
            var castedRaycast = (RaycastHit) raycastHit;
            
            if (castedRaycast.collider.CompareTag(TagHelper.TerrainTag)) {
                pLocomotion.CanSearch = true;
                pLocomotion.SetDestinationAndSearchPathNonNormalized(pPathing.GetNodeDestination(castedRaycast.point));
            }
        }

        public void OnCrouch() {
            throw new System.NotImplementedException();
        }
        
        #endregion
    }
}