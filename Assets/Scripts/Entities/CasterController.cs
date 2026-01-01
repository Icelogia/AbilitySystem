using UnityEngine;

namespace ShatteredIceStudio.Entities
{
    using Input;
    using AbilitySystem;
    using AbilitySystem.Abilities;
    using AbilitySystem.Attributes;

    public class CasterController : MonoBehaviour, IAbilityHolder
    {
        [SerializeField] private AbilityController ability;
        [SerializeField] private AttributeSet attributeSet;

        private void OnEnable()
        {
            ability.SetAbilityHolder(this);
            InputManager.Instance.OnCastStart += ability.Aim;
            InputManager.Instance.OnCastEnd += ability.Cast;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnCastStart -= ability.Aim;
            InputManager.Instance.OnCastEnd -= ability.Cast;
        }

        public Vector3 GetTargetPosition()
        {
            return InputManager.Instance.GetMouseAim(transform.position);
        }

        public AttributeSet GetAttributeSet()
        {
            return attributeSet;
        }
    }
}