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

        /// <summary>
        /// Sets the agent's body floats in the animation controller
        /// </summary>
        /// <param name="bodyHorizontal"></param>
        /// <param name="bodyVertical"></param>
        public void SetAnimatorBodyFloatValues(float bodyHorizontal, float bodyVertical) {
            _animator.SetFloat(AnimationParameterStatics.BodyHorizontal, bodyHorizontal);
            _animator.SetFloat(AnimationParameterStatics.BodyVertical, bodyVertical);
        }

        /// <summary>
        /// Sets the agent's weapon type int in the animation controller
        /// </summary>
        /// <param name="type"></param>
        public void SetWeaponTypeInt(int type) {
            _animator.SetInteger(AnimationParameterStatics.WeaponTypeInt, type);
        }

        /// <summary>
        /// Sets a boolean animator parameter based on a value
        /// </summary>
        /// <param name="parameter">An AnimationParameterStatic</param>
        /// <param name="value">True or false</param>
        public void TriggerBool(string parameter) {
            _animator.SetBool(parameter, true);
            //_animator.animator
        }

        public void SetAnimatorTrigger(string triggerName) {
            _animator.SetTrigger(triggerName);
            
        }
        
        /// <summary>
        /// Creates a weighted system to randomly apply random idle animations upon exiting a locomotion state
        /// Can invoke other random idle animations via coroutines
        /// </summary>
        public void SetToRandomIdleAnimation() {
        }

        /// <summary>
        /// When an animation is applied to a game object and contains root motion, the delta between the parent
        /// game object and the object with the animator on it will be in sync via ReturnDeltaPositionVector
        /// </summary>
        /// <returns></returns>
        public Vector3 ReturnDeltaPositionVector() {
            return _animator.deltaPosition;
        }
        
        #endregion
    }
}