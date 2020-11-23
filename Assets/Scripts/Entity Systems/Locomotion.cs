using Pathfinding;
using UnityEngine;

namespace Entity_Systems {
    public class Locomotion {
        #region Private Fields
        
        private readonly Transform _transform;    // Agent's transform component
        private readonly AIPath _ai;    // Agen'ts AIPath component, should not be interacted w/ in inspector 

        private readonly float _maxSpeed;    // Defined in Agent's catalog
        private readonly float _minSpeed;    // Defined in Agent's catalog

        #endregion
        
        #region Properties

        /// <summary>
        /// Sets & gets the AIPath.MaxSpeed property.
        /// </summary>
        public float MaxSpeed {
            get => _ai.maxSpeed;
            set {
                if (value < _minSpeed || value > _maxSpeed) {
                    Debug.LogError($"Input speed was out of range. Passed to: {_transform.gameObject.name}");

                    return;
                }

                _ai.maxSpeed = value;
            }
        }
        public bool CanSearch {
            get => _ai.canSearch;
            set => _ai.canSearch = value;
        }

        #endregion

        #region Event Subscription
    
        /// <summary>
        /// Pass any delegates that should be invoked when the Agent reaches it's target destination
        /// </summary>
        /// <param name="delegate">Delegate to subscribe to OnTargetReachedDelegate</param>
        public void SubToOnTargetReach(AIPath.OnTargetReachedDelegate @delegate) {
            _ai.onTargetReached += @delegate;
        }

        #endregion
        
        #region Constructor

        public Locomotion(Transform transform, AIPath ai, float minSpeed = 2.0f, float maxSpeed = 4.0f) {
            _transform = transform;
            _ai = ai;
            _minSpeed = minSpeed;
            _maxSpeed = maxSpeed;
            
            SubToOnTargetReach(OnTargetReached);
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

        public float GetCurrentSpeed() {
            return _ai.velocity.magnitude;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Once pathfinding destination is reached, this method will set the animator speed to 'idle'
        /// </summary>
        private void OnTargetReached() {
            MaxSpeed = 2.0f;
            CanSearch = false;
        }

        #endregion
    }
}