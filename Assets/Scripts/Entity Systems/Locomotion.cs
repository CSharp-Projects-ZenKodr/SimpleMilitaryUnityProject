using Pathfinding;
using UnityEngine;

namespace Entity_Systems {
    public class Locomotion {
        #region Private Fields & Properties
        
        private readonly Transform _transform;
        private readonly AIPath _ai;

        #endregion

        #region Public Return Methods

        public AIPath GetAIPath() {
            return _ai;
        }
        
        #endregion
        
        #region Constructor

        public Locomotion(Transform transform, AIPath ai) {
            _transform = transform;
            _ai = ai;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Moves to a destination vector on A* mesh; assumes a point has been calculated on a navmesh node
        /// </summary>
        /// <param name="destinationVector">The attempted vector to get</param>
        public void SetDestinationAndSearchPathNonNormalized(Vector3 destinationVector) {
            _ai.destination = destinationVector;
            _ai.SearchPath();
        }

        /// <summary>
        /// Similar to SetDestinationAndSearchPathNonNormalized, but normalizes the vector
        /// Used to move player via keyboard input
        /// </summary>
        /// <param name="destinationVectorToNormalize"></param>
        public void SetDestinationAndSearchPathNormalized(Vector3 destinationVectorToNormalize) {
            
        }

        #endregion
    }
}