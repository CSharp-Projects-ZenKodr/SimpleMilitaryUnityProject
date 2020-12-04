using Systems.Equipment.Weapons;
using UnityEngine;

namespace Systems.Equipment {
    public class Equipment {
        #region Public Fields & Properties

        public ProjectileWeapon CurrentProjectileWeapon => _currentProjectileWeapon;

        #endregion
        
        #region Private Fields & Properties

        private ProjectileWeapon _currentProjectileWeapon;

        #endregion

        #region Public Methods - Weapons

        /// <summary>
        /// Sets the agent's currently equipped projectile weapon to a new ProjectileWeapon
        /// </summary>
        /// <param name="jointToAssignTo">The join to parent the weapon to. Typically left/right hand</param>
        /// <param name="projectileWeapon">The ProjectileWeapon to eqip</param>
        public void EquipProjectileWeapon(Transform jointToAssignTo, ProjectileWeapon projectileWeapon) {
            if (jointToAssignTo is null || projectileWeapon is null || projectileWeapon == _currentProjectileWeapon) {
                if (_currentProjectileWeapon == projectileWeapon) Debug.Log("Equipped weapon is the same!");
                
                return;
            }

            _currentProjectileWeapon = projectileWeapon;
           
            projectileWeapon.InstantiateAssetReference(jointToAssignTo);
        }
        
        #endregion
    }
}