using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Modificators
{
    using Attributes;

    [CreateAssetMenu(fileName = "Health Based Damage Modificator", menuName = "Ability System/Modificators/Health Based Damage")]
    public class HealthBasedDamageModificator : Modificator
    {
        [SerializeField] private int baseDamage = 1;
        [Range(0f, 1f), SerializeField] private float ownerMaxHealthPrecentage;
        [Range(0f, 1f), SerializeField] private float targetMaxHealthPrecentage;

        public override void ApplyModification(AttributeSet attributeSet, AttributeSet owner)
        {
            var currentHealth = attributeSet.Health.Get();
            int damage = MathExtensions.Round(
                baseDamage 
                + ownerMaxHealthPrecentage * attributeSet.MaxHealth.Get() 
                + targetMaxHealthPrecentage * owner.MaxHealth.Get());

            currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);
            attributeSet.Health.Set(currentHealth);
        }
    }
}