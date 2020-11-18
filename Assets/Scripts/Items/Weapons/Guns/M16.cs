using UnityEngine;

namespace Items.Weapons.Guns {
    [CreateAssetMenu(fileName = "Projectile Weapon", menuName = "Create Weapon/Projectiles/M16")]
    public class M16 : ProjectileWeapon {
        public override void TriggerProjectile() {
            throw new System.NotImplementedException();
        }

        public override void TriggerProjectileAbility() {
            throw new System.NotImplementedException();
        }
    }
}
