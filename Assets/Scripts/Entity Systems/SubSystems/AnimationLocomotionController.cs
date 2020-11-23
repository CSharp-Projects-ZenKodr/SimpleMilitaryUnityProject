using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using Animation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity_Systems.SubSystems {
    public class AnimationLocomotionController {
        #region Properties 

        private readonly AnimationBrain _brain;

        #endregion

        #region Private Fields

        private Coroutine _idleSequenceCoroutine = null;

        #endregion
        
        #region Constructor

        public AnimationLocomotionController(AnimationBrain animationBrain) {
            _brain = animationBrain;
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Updates the agent's locomotion animations. Attempt to blend from walk to run and run to walk.
        /// </summary>
        /// <param name="relativeSpeed">Relative locomotion velocity magnitude</param>
        public void UpdateLocomotionAnimation(float relativeSpeed) {
            if (relativeSpeed <= 0.001f) {
                _brain.Animate(AnimId.Idle);

                return;
            }
            
            ClipState.Transition outAnim = null, playAnim = null;

            if (relativeSpeed > 0.001f && relativeSpeed <= 2.0f) {
                playAnim = _brain.QueryDataContainer(AnimId.Walk);
                //outAnim = _brain.QueryDataContainer(AnimId.Run);
            }
            
            else if (relativeSpeed > 3.0f) {
                playAnim = _brain.QueryDataContainer(AnimId.Run);
                //outAnim = _brain.QueryDataContainer(AnimId.Walk);
            }

            //else outAnim = playAnim = _container.Query(AnimId.Walk);

            _brain.Animate(out var playState, playAnim);
           // _brain.AnimancerTryGet(out var outState, playState, playAnim, outAnim);
            
           
            // if (_brain._animancer.States.TryGet(outAnim, out var outState) 
            //     
            //     && outState.IsPlaying)
            //     playState.NormalizedTime = outState.NormalizedTime;
        }

        /// <summary>
        /// Stops the current idle sequence coroutine (if not null)
        /// Starts a new instance of idle sequence coroutine
        /// </summary>
        public void StartIdleSequence() {
            ResetIdleCr();
            _brain.Animate(AnimId.Idle);

            _brain.StartCoroutine(out _idleSequenceCoroutine,StartIdleAnimationSequenceCr());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates random time for idle animation actions to play
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartIdleAnimationSequenceCr() {
            var randomTime = Random.Range(5.0f, 12.0f);
            var randomIdleClipToPlay = Random.Range(1, _brain.QueryDataContainerIdleListCount());

            yield return new WaitForSeconds(randomTime);

            var clipToPlay = _brain.QueryDataContainerIdleList(randomIdleClipToPlay);
            _brain.Animate(out var state, clipToPlay, 0.25f, new List<Action> {StartIdleSequence});

            yield return state;
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