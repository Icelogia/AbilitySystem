using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities.Indicators
{
    using Input;

    public class DirectionalIndicator : MonoBehaviour
    {
        private void Update()
        {
            var aimPosition = InputManager.Instance.GetMouseAim(transform.position);
            transform.LookAt(aimPosition);
        }
    }
}