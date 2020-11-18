using UnityEngine;

namespace Helpers {
    public static class ExtensionMethods {
        public static T GetComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component {
            var t = parent.transform;
            
            foreach(Transform tr in t) {
                if(tr.CompareTag(tag)) {
                    return tr.GetComponent<T>();
                }
            }
            
            return null;
        }
    }
}