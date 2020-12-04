using Systems.SubSystems.Data_Containers;
using Animancer;
using UnityEngine;

namespace Entity_Systems.SubSystems.Animation {
    public class AnimationQueryController {
        #region Private Readonly Fields

        private readonly Animator _animator;
        private readonly AnimationDataContainer _animationDataContainer;

        #endregion

        #region Constructor

        public AnimationQueryController(Animator animator, AnimationDataContainer animationDataContainer) {
            _animator = animator;
            _animationDataContainer = animationDataContainer;
        }

        #endregion
        
        #region Public Methods - Helper Methods for the Brain

        /// <summary>
        /// Queries the animation data _animationDataContainer to return an animation clip
        /// </summary>
        /// <param name="animId">String literal to use in the query</param>
        /// <returns></returns>
        public ClipState.Transition QueryDataContainer(string animId) {
            return _animationDataContainer.Query(animId);
        }

        /// <summary>
        /// Queries animation data _animationDataContainer idle sequencer and returns in input parameter that matches the list index
        /// </summary>
        /// <param name="index">List index value to return</param>
        /// <returns></returns>
        public ClipState.Transition QueryDataContainerIdleList(int index) {
            //return _animationDataContainer.IdleSequence[index]; 
            return null;
        }

        /// <summary>
        /// Queries the agent's animation data _animationDataContainer to return the idle sequence list count
        /// </summary>
        /// <returns></returns>
        public int QueryDataContainerIdleListCount() {
            //return _animationDataContainer.IdleSequence.Count;
            return 0;
        }

        /// <summary>
        /// Returns the delta between parent transform.position and the prefab transform.position
        /// </summary>
        /// <returns></returns>
        public Vector3 QueryAnimatorDeltaPosition() {
            return _animator.deltaPosition;
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