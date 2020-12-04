using Pathfinding;
using UnityEngine;

namespace Entity_Systems {
    public class Pathing {
        #region Private Fields & Properties

        private readonly Seeker _seeker;

        #endregion

        #region Constructor

        public Pathing(Seeker seeker) {
            _seeker = seeker;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calls the AI's pathing system and handles path creation to destination
        /// </summary>
        /// <param name="targetDestination">The vector3 destination</param>
        public Vector3 GetNodeDestination(Vector3 targetDestination) {
            return AstarPath.active.GetNearest(targetDestination, NNConstraint.Default).position;
        }

        #endregion
    }
}