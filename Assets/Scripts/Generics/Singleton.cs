using UnityEngine;

namespace Generics {
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        #region Private Static Fields
        
        // This will check if the singleton is about to be destory
        private static bool _shuttingDown = false;
        private static object _lock = new object();
        private static T _instance;

        #endregion

        /// <summary>
        /// Single accessor property
        /// "_shuttingDown is an arbitrary label for quitting the application
        /// </summary>
        public static T instance {
            get {
                if (_shuttingDown) {
                    return null;
                }

                lock (_lock) {
                    if (_instance is null) {
                        // Try to get existing instance
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (_instance is null) {
                            // Create a new GameObject and attach the singleton Monobehaviour
                            var singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            
                            // Name the object to identify the type of singleton
                            singleton.name = typeof(T).ToString() + " Singleton";
                            
                            // Persist the singleton instance
                            DontDestroyOnLoad(singleton);
                        }
                    }
                }

                return _instance;
            }
        }

        #region Virtual Override Methods

        /// <summary>
        /// A method to allow creation of dependencies in the Awake() method
        /// </summary>
        protected virtual void CreateDependencies() {}

        #endregion
        
        #region Private Methods

        private void OnApplicationQuit() {
            _shuttingDown = true;
        }

        private void OnDestroy() {
            _shuttingDown = true;
        }

        #endregion
    }
}