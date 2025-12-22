using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities
{
    using Projectiles;

    public class ACSimpleProjectile : AbilityController
    {
        [SerializeField] private SimpleProjectile projectilePrefab;

        public override void Aim()
        {
            base.Aim();
        }

        public override void Cast()
        {
            base.Cast();
        }
    }
}