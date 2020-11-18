using UnityEngine;

namespace Animation {
    public class AnimationHandler {
        #region Private Readonly Fields

        private readonly Animator _animator;
        private readonly AnimatorInitializer _animatorInitializer;

        #endregion

        #region Constructor

        public AnimationHandler(Animator animator) {
            _animator = animator;
            _animatorInitializer = new AnimatorInitializer(animator);
        }

        #endregion

        #region Private Fields & Properties

        private Coroutine _randomIdleAnimationCoroutine;

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Controls what idle animation will be played. This should be invoked w/ '0' upon movement
        /// </summary>
        /// <param name="idleState">Sets Animator's Animation_int value</param>
        public void SetAnimationInt(AnimationIdleStates idleState) {
            _animator.SetInteger(AnimationParameterStatics.IdleAnimation, (int)idleState);
        }
        
        /// <summary>
        /// 'Speed_f' to a value between 0 & 1
        ///     0 = Idle
        ///     Greater than 0, but less than/equal to 0.5 = Walk
        ///     Greater than 0.5, but less than/equal to 1.0 = Run
        /// </summary>
        /// <param name="locomotionIdentifier">See <see cref="AnimationSpeedStates"/>; sets animator speed </param>
        public void SetLocomotionAnimation(float locomotionIdentifier) {
            _animator.SetFloat(AnimationParameterStatics.Speed, locomotionIdentifier); 
        }

        public void SetAnimatorBodyFloatValues(float bodyHorizontal, float bodyVertical) {
            _animator.SetFloat(AnimationParameterStatics.BodyHorizontal, bodyHorizontal);
            _animator.SetFloat(AnimationParameterStatics.BodyVertical, bodyVertical);
        }
        
        /// <summary>
        /// Creates a weighted system to randomly apply random idle animations upon exiting a locomotion state
        /// Can invoke other random idle animations via coroutines
        /// </summary>
        public void SetToRandomIdleAnimation() {
        }
        
        #endregion
    }
}