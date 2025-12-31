using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities.Projectiles.Data
{
    using ShatteredIceStudio.AbilitySystem.Effectors;

    [CreateAssetMenu(fileName = "Simple Projectile Data", menuName = "Ability System/Projectiles/Simple Projectile")]
    public class SimpleProjectileData : ScriptableObject
    {
        [field: SerializeField] public Effector Effector;
        [field: SerializeField] public float MaxSpeed = 5f;
        [field: SerializeField] public float Acceleration = 1f;

        /// <summary>
        /// Projectile's lifetime. Values smaller or equal to 0 result in infinite lifetime.
        /// </summary>
        [field: SerializeField] public float Lifetime = 0f;
    }
}