using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using Entity_Systems.SubSystems.Animation;
using Entity_Systems.SubSystems.Animation.AnimationDataContainers;
using Entity_Systems.SubSystems.Animation.Helpers;
using Interfaces;
using UnityEngine;

namespace Entity_Systems {
    public class AnimationBrain : IOnEnable {
        #region Dependencies

        protected readonly AnimDataContainer _container;
        protected readonly Animator _animator;
        
        #endregion
        
        #region Public Helper Classes
        
        public readonly AnimancerComponent Animancer;
        public readonly AnimationLocomotionController LocomotionController;
        public readonly AnimationQueryController QueryController;
        
        #endregion

        #region Constructor

        public AnimationBrain(AnimancerComponent animancer, AnimDataContainer container, 
                Animator animator) {
            Animancer = animancer;
            _container = container;
            _animator = animator;
            LocomotionController = new AnimationLocomotionController(this, container);
            QueryController = new AnimationQueryController(animancer, _animator);
            
            Debug.Assert(container.IdleSequence.Count > 1, "Idle sequence must have at least 1 element");
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Set the Animancer to 'idle' on game start
        /// </summary>
        public void Enable() {
            //Animate(AnimId.Idle);
            
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

            var state = Animancer.Play(clip, fadeDuration);
            
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

            state = Animancer.Play(clip, fadeDuration);
            
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
             var state = Animancer.Play(animation, fadeDuration);
             
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
            
            state = Animancer.Play(animation, fadeDuration);
            
            if (onEndCallbacks != null) {
                foreach (var action in onEndCallbacks) {
                    state.Events.OnEnd += action;
                }
            }

            return state;
        }

        #endregion
    }
}