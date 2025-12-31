using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Modificators
{
    using Attributes;

    public abstract class Modificator : ScriptableObject
    {
        public virtual void ApplyModification(AttributeSet attributeSet, AttributeSet effectorOwner)
        {

        }
    }
}