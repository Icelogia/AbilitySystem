using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem
{
    using Attributes;

    public interface IAbilityHolder
    {
        public AttributeSet GetAttributeSet();
        public Vector3 GetTargetPosition();
    }
}