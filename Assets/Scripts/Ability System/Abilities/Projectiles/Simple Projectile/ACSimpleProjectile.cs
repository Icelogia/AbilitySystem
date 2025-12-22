using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities
{
    using Projectiles;

    public class ACSimpleProjectile : AbilityController
    {
        [SerializeField] private SimpleProjectile projectilePrefab;

        private Vector3 aimDirection;

        public override void Aim()
        {
            base.Aim();
        }

        public override void Cast()
        {
            base.Cast();
            var projectile = projectilePrefab.gameObject.Spawn<SimpleProjectile>(transform.position);
            projectile.SetDirection(aimDirection);
        }
    }
}