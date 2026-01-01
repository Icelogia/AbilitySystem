using UnityEngine;

namespace ShatteredIceStudio.Entities
{
    using UI;
    using AbilitySystem.Attributes;

    public class TargetController : MonoBehaviour
    {
        [SerializeField] private AttributeSet attributeSet;

        private void OnEnable()
        {
            attributeSet.Health.OnAttributeChanged += SetHPBar;
        }

        private void OnDisable()
        {
            attributeSet.Health.OnAttributeChanged -= SetHPBar;
        }

        private void SetHPBar(int oldHp, int newHp)
        {
            float normalizedHP = (float)newHp / attributeSet.MaxHealth.Get();
            HUDManager.Instance.TargetHPBar.SetSize(normalizedHP);
        }
    }
}
