using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities
{
    using Projectiles;
    using Indicators;

    public class ACSimpleProjectile : AbilityController
    {
        [SerializeField] private DirectionalIndicator indicatorPrefab;
        [SerializeField] private SimpleProjectile projectilePrefab;

        private Vector3 aimDirection;
        private DirectionalIndicator indicator;

        public override void Aim()
        {
            base.Aim();
            indicator = indicatorPrefab.gameObject.Spawn<DirectionalIndicator>(transform.position);
        }

        public override void Cast()
        {
            base.Cast();

            indicator.Recycle();
            indicator = null;

            var projectile = projectilePrefab.gameObject.Spawn<SimpleProjectile>(transform.position);
            projectile.SetDirection(aimDirection);
            projectile.SetOwner(holder.GetAttributeSet());
        }

        protected override void Update()
        {
            base.Update();

            if(holder != null)
                aimDirection = (holder.GetTargetPosition() - transform.position).normalized;

            if (indicator != null)
                indicator.SetDirection(aimDirection);
        }
    }
}