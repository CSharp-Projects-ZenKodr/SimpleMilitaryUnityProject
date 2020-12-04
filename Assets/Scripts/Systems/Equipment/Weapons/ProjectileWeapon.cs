using System;
using UnityEngine;

namespace Systems.Equipment.Weapons {
    public abstract class ProjectileWeapon : SpawnableItem {
        #region Public Fields

        public float BaseDamage;
        public float BaseProjectileForce;
        
        /// <summary>
        /// Please see <see cref="AnimationWeaponTypes"/> for information on how to choose a correct value
        /// </summary>
        public float WeaponTypeInt;
        
        [SerializeField] public BodyFloats AnimatorBodyFloats;

        #endregion

        #region Private Serialized Fields

        [SerializeField] public GameObject Muzzle; 

        #endregion

        #region Virtual Methods

        /// <summary>
        /// How the weapon will hand firing a projectile
        /// May require additional helper methods
        /// </summary>
        public abstract void TriggerProjectile();
        
        /// <summary>
        /// An ability that is unique to a given defined weapon
        /// </summary>
        public abstract void TriggerProjectileAbility();

        #endregion

        #region Float Struct
        // Struct to dictate animator body floats, dependent on type of weapon
        [Serializable] public struct BodyFloats {
            public float BodyHorzIdle;
            public float BodyVertIdle;
            public float BodyHorzWalk;
            public float BodyVertWalk;
            public float BodyHorzRun;
            public float BodyVertRun;
        }

        #endregion
    }
}