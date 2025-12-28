using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities.Indicators
{

    public class DirectionalIndicator : MonoBehaviour
    {
        public void SetDirection(Vector3 direction)
        {
            transform.LookAt(transform.position + direction);
        }
    }
}