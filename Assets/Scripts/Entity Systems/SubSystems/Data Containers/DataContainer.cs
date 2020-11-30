using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Entity_Systems.SubSystems.Data_Containers {
    public abstract class DataContainer<T> : ScriptableObject where T : class {
        #region Container List - Abstract

        public virtual List<T> DataList { get; }
        
        #endregion

        #region Data Container Dictionary - Used for Querying Data

        protected Dictionary<string, T> _dictionary = null;

        /// <summary>
        /// Initializes the dictionary
        /// They key assigned is related to a state/animation identifier
        /// This does require a string literal to access
        /// </summary>
        public virtual Dictionary<string, T> Dictionary {
            get {
                if (_dictionary == null) {
                    _dictionary = new Dictionary<string, T>();
        
                    PopulateDictionary();
                }

                return _dictionary;
            }
        }
        
        /// <summary>
        /// Initializes the dictionary if it is null, populates it otherwise
        /// </summary>
        private void PopulateDictionary() {
            if (DataList.Count == 0) {
                Debug.LogError("DataList does not contain any items.");
                
                return;
            }
            
            foreach (var item in DataList) {
                if (!_dictionary.ContainsKey(item.ToString())) {
                    _dictionary.Add(item.ToString(), item);
                }
            }
        }

        #endregion

        #region Query Dictionary

        /// <summary>
        /// Queries Dictionary for a clip based on the string literal input
        /// </summary>
        /// <param name="key">The clip name to return</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">key was 'null'</exception>
        public T Query([NotNull] string key) {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (key == "") {
                Debug.Log("Please input a valid string identifier");

                return null;
            }

            var value = _dictionary.Single(c => c.Key == key).Value;

            if (value == null) {
                Debug.LogError($"The clip associated with {key} return 'null'.");
                
                return null;
            }

            return value;
        }

        #endregion
    }
}