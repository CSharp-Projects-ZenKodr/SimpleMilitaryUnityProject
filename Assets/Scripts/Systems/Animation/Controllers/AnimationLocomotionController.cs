using System;
using System.Collections;
using System.Collections.Generic;
using Systems.Animation;
using Systems.Animation.Helpers;
using Systems.SubSystems.Data_Containers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity_Systems.SubSystems.Animation {
    public class AnimationLocomotionController {
        #region Private Fields

        private readonly AnimationDataContainer _animationDataContainer;
        private Coroutine _idleSequenceCoroutine = null;
        private readonly List<Action> _callBackActions = new List<Action>();

        #endregion
        
        #region Constructor

        public AnimationLocomotionController(AnimationDataContainer animationDataContainer, AnimationBrain brain) {
            _animationDataContainer = animationDataContainer;
            _callBackActions.Add( () => StartIdleSequence(_animationDataContainer, brain));   
        }

        #endregion
        
        #region Public Methods
        //
        // /// <summary>
        // /// Updates the agent's locomotion animations. Attempt to blend from walk to run and run to walk.
        // /// </summary>
        // /// <param name="relativeSpeed">Relative locomotion velocity magnitude</param>
        // /// <param name="_animationDataContainer">Data _animationDataContainer for animation clips</param>
        // /// <param name="brain">The animation brain - used to called Animate method</param>
        // public void UpdateLocomotionAnimation(float relativeSpeed, AnimationDataContainer _animationDataContainer, AnimationBrain brain) {
        //     if (relativeSpeed <= 0.001f) {
        //         brain.Animate(AnimationDictionaryKeys.Idle);
        //
        //         return;
        //     }
        //     
        //     ClipState.Transition outAnim = null, playAnim = null;
        //
        //     if (relativeSpeed > 0.001f && relativeSpeed <= 2.0f) {
        //         playAnim = brain.QueryController.QueryDataContainer(AnimationDictionaryKeys.Walk, _animationDataContainer);
        //         //outAnim = _brain.QueryDataContainer(AnimationDictionaryKeys.Run);
        //     }
        //     
        //     else if (relativeSpeed > 3.0f) {
        //         playAnim = brain.QueryController.QueryDataContainer(AnimationDictionaryKeys.Run, _animationDataContainer);
        //         //outAnim = _brain.QueryDataContainer(AnimationDictionaryKeys.Walk);
        //     }
        //
        //     //else outAnim = playAnim = _animationDataContainer.Query(AnimationDictionaryKeys.Walk);
        //
        //     brain.Animate(out var playState, playAnim);
        //    // _brain.AnimancerTryGet(out var outState, playState, playAnim, outAnim);
        //     
        //    
        //     // if (_brain.Animancer.States.TryGet(outAnim, out var outState) 
        //     //     
        //     //     && outState.IsPlaying)
        //     //     playState.NormalizedTime = outState.NormalizedTime;
        // }

        /// <summary>
        /// Stops the current idle sequence coroutine (if not null)
        /// Starts a new instance of idle sequence coroutine
        /// </summary>
        public void StartIdleSequence(AnimationDataContainer container, AnimationBrain brain) {
            ResetIdleCr();
            brain.Animate(AnimationDictionaryKeys.Idle);

            //_idleSequenceCoroutine = brain.Animancer.StartCoroutine(StartIdleAnimationSequenceCr(container, brain));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates random time for idle animation actions to play
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartIdleAnimationSequenceCr() {
            var randomTime = Random.Range(5.0f, 12.0f);
            //var randomIdleClipToPlay = Random.Range(1, brain.QueryController.QueryDataContainerIdleListCount(_animationDataContainer));

            yield return new WaitForSeconds(randomTime);

            //var clipToPlay = brain.QueryController.QueryDataContainerIdleList(randomIdleClipToPlay, _animationDataContainer);
            //brain.Animate(out var state, clipToPlay, 0.25f, _callBackActions);

            //yield return state;
        }

        /// <summary>
        /// If the idle coroutine is not null, it will stop it and set it's value to null
        /// </summary>
        private void ResetIdleCr() {
            if (_idleSequenceCoroutine != null) return;

            _idleSequenceCoroutine = null;
        }
        
        #endregion
    }
}