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
        /// Sets & gets the AIPath.Speed property.
        /// </summary>
        public float Speed {
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

        public bool IsRunning { get; set; }

        #endregion
        
        #region Constructor

        public Locomotion(Transform transform, AIPath ai, float minSpeed = 0f, float maxSpeed = 4.0f) {
            _transform = transform;
            _ai = ai;
            _minSpeed = minSpeed;
            _maxSpeed = maxSpeed;
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
        /// Returns the AI Path's current speed
        /// </summary>
        /// <returns></returns>
        public float GetCurrentSpeed() {
            return _ai.velocity.magnitude;
        }

        /// <summary>
        /// Register methods to the onTargetReached StateAction
        /// </summary>
        /// <param name="delegate">Subscribing method</param>
        public void RegisterDelegateToAiOnPathComplete(AIPath.OnTargetReachedDelegate @delegate) {
            _ai.onTargetReached += @delegate;
        }

        #endregion
    }
}