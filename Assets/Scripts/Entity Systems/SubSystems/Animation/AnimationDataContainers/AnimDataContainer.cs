using System;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using JetBrains.Annotations;
using UnityEngine;

namespace Entity_Systems.SubSystems.Animation.AnimationDataContainers {
    [CreateAssetMenu(order = 1, fileName = "AnimDataContainer", menuName = "Animations/Containers/AnimDataContainer")]
    public class AnimDataContainer : ScriptableObject {
        #region List of Agent Animations

        [SerializeField] private List<ClipState.Transition> AnimList = new List<ClipState.Transition>();

        #endregion

        #region TransitionDictionary

        private Dictionary<string, ClipState.Transition> _animDict;
        public Dictionary<string, ClipState.Transition> AnimDict => _animDict;

        #endregion
        
        #region IdleAnimationsSequence

        [SerializeField] private List<ClipState.Transition> _idleSequence = null;
        public List<ClipState.Transition> IdleSequence => _idleSequence;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the animation transtion dictionary
        /// They key assigned to each animation clip is the clip's name
        /// This does require a string literal to access
        /// </summary>
        private void OnEnable() {
            Debug.Log("Animator Data Container has been enabled");
            Debug.Assert(AnimList.Count > 0, "Please add animations to the 'AnimList' field");
            
            _animDict = new Dictionary<string, ClipState.Transition>();

            foreach (var anim in AnimList) {
                if (anim.Name == "") {
                    Debug.Log("Detected a null clip.");
                    continue;
                }

                else {
                    AnimDict.Add(anim.Name, anim);
                    Debug.Log($"Added {anim.Name}");
                }
            }
        }

        #endregion

        #region Query Dictionary

        /// <summary>
        /// Queries AnimDict for a clip based on the string literal input
        /// </summary>
        /// <param name="animId">The clip name to return</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">animId was 'null'</exception>
        public ClipState.Transition Query([NotNull] string animId) {
            if (animId == null) throw new ArgumentNullException(nameof(animId));
            if (animId == "") {
                Debug.Log("Please input a valid string identifier");

                return null;
            }

            var clip = _animDict.Single(c => c.Key == animId).Value;

            if (clip == null) {
                Debug.LogError($"The clip associated with {animId} return 'null'.");
                
                return null;
            }

            return clip;
        }

        #endregion
    }
}