using System.Collections;
using Animancer;
using Entity_Systems.SubSystems.Animation.AnimationDataContainers;
using UnityEngine;

namespace Entity_Systems.SubSystems.Animation {
    public class AnimationQueryController {
        #region Private Readonly Fields

        private readonly MonoBehaviour _mono;
        private readonly Animator _animator;

        #endregion

        #region Constructor

        public AnimationQueryController(MonoBehaviour mono, Animator animator) {
            _mono = mono;
            _animator = animator;
        }

        #endregion
        
        #region Public Methods - Helper Methods for the Brain

        /// <summary>
        /// Queries the animation data container to return an animation clip
        /// </summary>
        /// <param name="animId">String literal to use in the query</param>
        /// <param name="container">Data container for agent's animations</param>
        /// <returns></returns>
        public ClipState.Transition QueryDataContainer(string animId, AnimDataContainer container) {
            return container.Query(animId);
        }

        /// <summary>
        /// Queries animation data container idle sequencer and returns in input parameter that matches the list index
        /// </summary>
        /// <param name="index">List index value to return</param>
        /// <param name="container">Data container for agent's animations</param>
        /// <returns></returns>
        public ClipState.Transition QueryDataContainerIdleList(int index, AnimDataContainer container) {
            return container.IdleSequence[index]; 
        }

        /// <summary>
        /// Queries the agent's animation data container to return the idle sequence list count
        /// </summary>
        /// <returns></returns>
        public int QueryDataContainerIdleListCount(AnimDataContainer container) {
            return container.IdleSequence.Count;
        }

        /// <summary>
        /// Returns the delta between parent transform.position and the prefab transform.position
        /// </summary>
        /// <returns></returns>
        public Vector3 QueryAnimatorDeltaPosition() {
            return _animator.deltaPosition;
        }

        /// <summary>
        /// Calls the Monobehaviour method: StartCoroutine
        /// </summary>
        /// <param name="startedRoutine">Optional: outputs the started coroutine as a variable</param>
        /// <param name="routine">The method to start</param>
        public void StartCoroutine(out Coroutine startedRoutine, IEnumerator routine) {
            startedRoutine = _mono.StartCoroutine(routine);
        }

        // /// <summary>
        // /// Attempts to blend two animations and sync their normalized time field. 
        // /// </summary>
        // /// <param name="outState">The state to out, if it is currently playing</param>
        // /// <param name="playState">The state Animancer is currently in</param>
        // /// <param name="playAnim">The assumed clip Animancer is currently playing</param>
        // /// <param name="tryAnim">The clip to check against; is this clip playing?</param>
        // public void AnimancerTryGet(out AnimancerState outState, ClipState.Transition tryAnim) {
        //     if (Animancer.States.TryGet(tryAnim, out outState) && outState.IsPlaying)
        //         playState.NormalizedTime = outState.NormalizedTime;
        // }

        #endregion
    }
}