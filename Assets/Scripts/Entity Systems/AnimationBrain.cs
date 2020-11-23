using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using Animation;
using Entity_Systems.SubSystems;
using Entity_Systems.SubSystems.Animation;
using Entity_Systems.SubSystems.Animation.AnimationDataContainers;
using Entity_Systems.SubSystems.Animation.Helpers;
using Interfaces;
using UnityEngine;

namespace Entity_Systems {
    public class AnimationBrain : IOnEnable {
        #region Dependencies

        private readonly AnimancerComponent _animancer;
        private readonly AnimDataContainer _container;
        private readonly AnimationLocomotionController _locomotionController;
        private readonly Animator _animator;
        
        #endregion

        #region Constructor

        public AnimationBrain(AnimancerComponent animancer, AnimDataContainer container, 
                Animator animator) {
            _animancer = animancer;
            _container = container;
            _animator = animator;
            _locomotionController = new AnimationLocomotionController(this);
            
            Debug.Assert(container.IdleSequence.Count > 1, "Idle sequence must have at least 1 element");
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Set the Animancer to 'idle' on game start
        /// </summary>
        public void Enable() {
            Animate(AnimId.Idle);
            
            Debug.Assert(_container.IdleSequence.First().Clip.isLooping);    // Idle animations SHOULD be looping
            Debug.Assert(_container.IdleSequence.Count > 1);    // A sequence should be greater than 1 element
        }

        #endregion

        #region Public Methods - Core Animate Methods

        /// <summary>
        /// The core method for playing animations and generating states
        /// </summary>
        /// <param name="animId">String literal of the animation to play; call AnimId static</param>
        /// <param name="onEndCallbacks">Optional: methods to subscribe to Events.OnEnd</param>
        /// <param name="fadeDuration">Optional: the blend time for the animation</param>
        public void Animate(string animId, float fadeDuration = 0.25f, IEnumerable<Action> onEndCallbacks = null) {
            var clip = _container.Query(animId);
            if (clip == null) return ;

            var state = _animancer.Play(clip, fadeDuration);
            
            if (onEndCallbacks != null) {
                foreach (var action in onEndCallbacks) {
                    state.Events.OnEnd += action;
                }
            }
        }

        /// <summary>
        /// The core method for playing animations and generating states
        /// </summary>
        /// <param name="state">AnimancerState to return (the clip being played)</param>
        /// <param name="animId">String literal of the animation to play; call AnimId static</param>
        /// <param name="onEndCallbacks">Optional: methods to subscribe to Events.OnEnd</param>
        /// <param name="fadeDuration">Optional: the blend time for the animation</param>
        public AnimancerState Animate(out AnimancerState state, string animId, float fadeDuration = 0.25f, IEnumerable<Action> onEndCallbacks = null) {
            state = null;
            
            var clip = _container.Query(animId);
            if (clip == null) return null;

            state = _animancer.Play(clip, fadeDuration);
            
            if (onEndCallbacks != null) {
                foreach (var action in onEndCallbacks) {
                    state.Events.OnEnd += action;
                }
            }

            return state;
        }

        /// <summary>
                 /// The core method for playing animations and generating states 
                 /// </summary>
                 /// <param name="animation">The animation clip to play, as ITransition</param>
                 /// <param name="fadeDuration">Optional: the blend time for the animation</param>
                 /// <param name="onEndCallbacks">Optional: methods to subscribe to Events.OnEnd</param>
         public void Animate(ITransition animation, float fadeDuration = 0.25f,
             IEnumerable<Action> onEndCallbacks = null) {
             
             if (animation == null) return ;
             var state = _animancer.Play(animation, fadeDuration);
             
             if (onEndCallbacks != null) {
                 foreach (var action in onEndCallbacks) {
                     state.Events.OnEnd += action;
                 }
             }
         }

        /// <summary>
        /// The core method for playing animations and generating states 
        /// </summary>
        /// <param name="state">The return state</param>
        /// <param name="animation">The animation clip to play, as ITransition</param>
        /// <param name="fadeDuration">Optional: the blend time for the animation</param>
        /// <param name="onEndCallbacks">Optional: methods to subscribe to Events.OnEnd</param>
        public AnimancerState Animate(out AnimancerState state, ITransition animation, float fadeDuration = 0.25f,
            IEnumerable<Action> onEndCallbacks = null) {
            state = null;
            if (animation == null) return null;
            
            state = _animancer.Play(animation, fadeDuration);
            
            if (onEndCallbacks != null) {
                foreach (var action in onEndCallbacks) {
                    state.Events.OnEnd += action;
                }
            }

            return state;
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
            return _container.IdleSequence[index];
        }

        /// <summary>
        /// Queries the agent's animation data container to return the idle sequence list count
        /// </summary>
        /// <returns></returns>
        public int QueryDataContainerIdleListCount() {
            return _container.IdleSequence.Count;
        }

        /// <summary>
        /// Returns the delta between parent transform.position and the prefab transform.position
        /// </summary>
        /// <returns></returns>
        public Vector3 QueryAnimatorDeltaPosition() {
            return _animator.deltaPosition;
        }

        /// <summary>
        /// Updates the agent's locomotion animations
        /// </summary>
        /// <param name="speed">The agent's current speed</param>
        public void Update(float speed) {
            _locomotionController.UpdateLocomotionAnimation(speed);
        }

        /// <summary>
        /// Calls the Monobehaviour method: StartCoroutine
        /// </summary>
        /// <param name="startedRoutine">Optional: outputs the started coroutine as a variable</param>
        /// <param name="routine">The method to start</param>
        public void StartCoroutine(out Coroutine startedRoutine, IEnumerator routine) {
            startedRoutine = _animancer.StartCoroutine(routine);
        }

        // /// <summary>
        // /// Attempts to blend two animations and sync their normalized time field. 
        // /// </summary>
        // /// <param name="outState">The state to out, if it is currently playing</param>
        // /// <param name="playState">The state Animancer is currently in</param>
        // /// <param name="playAnim">The assumed clip Animancer is currently playing</param>
        // /// <param name="tryAnim">The clip to check against; is this clip playing?</param>
        // public void AnimancerTryGet(out AnimancerState outState, ClipState.Transition tryAnim) {
        //     if (_animancer.States.TryGet(tryAnim, out outState) && outState.IsPlaying)
        //         playState.NormalizedTime = outState.NormalizedTime;
        // }

        #endregion
    }
}