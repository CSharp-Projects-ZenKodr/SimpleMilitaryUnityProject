using System.Linq;
using UnityEngine;

namespace Entity_Systems {
    public class Skeleton {
        #region Private Fields & Properties

        private readonly GameObject _agentGameObject;

        #endregion

        #region Constructor

        public Skeleton(GameObject _gameObject) {
            _agentGameObject = _gameObject;
        }
        
       #endregion
       
       #region Public Methods

       /// <summary>
       /// Returns the transform of the input game object
       /// </summary>
       /// <param name="jointToGet">The joint identifier; will return it's transform if 'Agent'
       /// is not null
       /// </param>
       /// <returns></returns>
       public Transform GetJointTransform(string jointToGet) {
           if (jointToGet is null) {
               Debug.Log("Do Agent has been assigned to serialized field. Please assign an appropriate reference.");
                
               return null;
           }

           return _agentGameObject.GetComponentsInChildren<Transform>().FirstOrDefault(
               o => o.CompareTag(jointToGet));
       }

       #endregion
    }
}