using UnityEngine;

namespace ShatteredIceStudio.Entities
{
    using Input;
    using AbilitySystem.Abilities;
    using ShatteredIceStudio.AbilitySystem;

    public class CasterController : MonoBehaviour, IAbilityHolder
    {
        [SerializeField] private AbilityController ability;

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
    }
}