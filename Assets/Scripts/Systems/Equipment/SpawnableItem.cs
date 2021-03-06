﻿using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Systems.Equipment {
    public class SpawnableItem : ScriptableObject {
        #region Insantiation Fields For Display

        [SerializeField] private AssetReference _prefabReference = null;

        #endregion

        #region Public Method to Instantiate Model

        /// <summary>
        /// Handles loading prefab reference addressable
        /// </summary>
        /// <param name="jointToSpawnTo">Transform parent</param>
        public void InstantiateAssetReference(Transform jointToSpawnTo) {
            if (jointToSpawnTo == null) {
                Debug.Log("Input parameter 'jointToSpawnTo' was null");

                return;
            }
            
            if (!_prefabReference.RuntimeKeyIsValid()) {
                Debug.LogError($"Invalid prefab reference key: {_prefabReference.RuntimeKey}");
                
                return;
            }

            CreateAssetGameObject(jointToSpawnTo);
        }

        /// <summary>
        /// Helper method to handle async creation of prefab reference object
        /// </summary>
        /// <param name="jointToSpawnTo"></param>
        private void CreateAssetGameObject(Transform jointToSpawnTo) {
            var prefab = Addressables.LoadAssetAsync<GameObject>(_prefabReference);
            
            prefab.Completed += handle => {
                _prefabReference.InstantiateAsync(jointToSpawnTo);
            };
        }

        #endregion
    }
}