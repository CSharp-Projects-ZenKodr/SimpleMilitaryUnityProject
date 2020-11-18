using System;
using System.Linq;
using UnityEngine;

namespace Entity_Systems {
    public class Raycasting {
        #region Private Fields & Properties

        private readonly Camera _camera;

        #endregion

        #region Constructor

        public Raycasting(Camera camera) {
            _camera = camera;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Will be called by the Agent script
        /// Raycasts ALL colliders at mouseposition, relative to camera
        /// Returns the first collider the Raycast hits
        /// </summary>
        /// <returns></returns>
        public RaycastHit? GetRaycastOnClick() {
            var hits = Physics.RaycastAll(
                _camera.ScreenPointToRay(Input.mousePosition),
                Mathf.Infinity
            );
            var distance = new float[hits.Length];

            for (var i = 0; i < hits.Length; i++) {
                distance[i] = hits[i].distance;
            }

            Array.Sort(distance, hits);

            return (hits.Length < 1) ? (RaycastHit?) null : hits.First();
        }

        #endregion
    }
}