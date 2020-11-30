using Animancer;
using Entity_Systems.SubSystems.Animation.AnimationDataContainers;
using UnityEngine;

namespace Entity_Systems.SubSystems.Animation {
    public class AnimationQueryController {
        #region Private Readonly Fields

        private readonly Animator _animator;
        private readonly AnimDataContainer _container;

        #endregion

        #region Constructor

        public AnimationQueryController(Animator animator, AnimDataContainer container) {
            _animator = animator;
            _container = container;
        }

        #endregion
        
        #region Public Methods - Helper Methods for the Brain

        /// <summary>
        /// Queries the animation data container to return an animation clip
        /// </summary>
        /// <param name="animId">String literal to use in the query</param>
        /// <returns></returns>
        public ClipState.Transition QueryDataContainer(string animId) {
            return _container.Query(animId);
        }

        /// <summary>
        /// Queries animation data container idle sequencer and returns in input parameter that matches the list index
        /// </summary>
        /// <param name="index">List index value to return</param>
        /// <returns></returns>
        public ClipState.Transition QueryDataContainerIdleList(int index) {
            //return _container.IdleSequence[index]; 
            return null;
        }

        /// <summary>
        /// Queries the agent's animation data container to return the idle sequence list count
        /// </summary>
        /// <returns></returns>
        public int QueryDataContainerIdleListCount() {
            //return _container.IdleSequence.Count;
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