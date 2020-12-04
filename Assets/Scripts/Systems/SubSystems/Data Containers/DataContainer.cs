using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Systems.SubSystems.Data_Containers {
    public abstract class DataContainer<T> : SerializedScriptableObject 
        where T : class {
        #region Container Dictionary

        [SerializeField] protected Dictionary<string, T> _dictionary = new Dictionary<string, T>();
        [HideInInspector] public Dictionary<string, T> Dictionary => _dictionary;
        
        #endregion

        #region Query Dictionary

        /// <summary>
        /// Queries Dictionary for a clip based on the string literal input
        /// </summary>
        /// <param name="key">The clip name to return</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">key was 'null'</exception>
        public T Query(string key) {
            try {
                var hasValue = _dictionary.TryGetValue(key, out var value);

                if (hasValue) {
                    return value;
                }
                
                Debug.Log($"No value was found with the corresponding key: {key}");

                return null;
            }
            catch (Exception e) {
                Console.WriteLine("A valid enum identifier was not input: " + e.Message);
                throw;
            }
        }

        #endregion
    }
}