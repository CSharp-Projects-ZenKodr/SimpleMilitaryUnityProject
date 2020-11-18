using UnityEngine;

namespace Animation {
    public class AnimatorInitializer {
        #region Constructor & Initializer

        /// <summary>
        /// Sets all agent animator parameters to (subjective) default values
        /// </summary>
        public AnimatorInitializer(Animator animator, float speed = 0f, int idleAnimation = 0, int weaponType = 0, 
            bool death = false, bool jump = false, bool crouch = false, bool grounded = true, bool fullAuto = false, 
            bool reload = false, bool staticAnim = true, float headHorizontal = 0.0f, float headVertical = 0.0f,
            int deathType = 2, float bodyHorizontal = 0.0f, float bodyVertical = 0.0f) {
            
            #region Floats

            animator.SetFloat(AnimationParameterStatics.Speed, speed);
            animator.SetFloat(AnimationParameterStatics.HeadHorizontal, headHorizontal);
            animator.SetFloat(AnimationParameterStatics.HeadVertical, headVertical);
            animator.SetFloat(AnimationParameterStatics.BodyHorizontal, bodyHorizontal);
            animator.SetFloat(AnimationParameterStatics.BodyVertical, bodyVertical);

            #endregion

            #region Integers

            animator.SetInteger(AnimationParameterStatics.IdleAnimation, idleAnimation);
            animator.SetInteger(AnimationParameterStatics.WeaponTypeInt, weaponType);

            #endregion

            #region Bools

            animator.SetBool(AnimationParameterStatics.Death, death);
            animator.SetBool(AnimationParameterStatics.Jump, jump);
            animator.SetBool(AnimationParameterStatics.Crouch, crouch);
            animator.SetBool(AnimationParameterStatics.Grounded, grounded);
            animator.SetBool(AnimationParameterStatics.FullAuto, fullAuto);
            animator.SetBool(AnimationParameterStatics.Reload, reload);
            animator.SetBool(AnimationParameterStatics.Static, staticAnim);

            #endregion
        }

        #endregion
    }
}