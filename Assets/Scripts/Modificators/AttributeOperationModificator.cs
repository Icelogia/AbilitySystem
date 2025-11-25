using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Modificators
{
    using Attributes;

    [CreateAssetMenu(fileName = "Attribute Operation Modificator", menuName = "Ability System/Modificators/Attribute Operation", order = 1)]
    public class AttributeOperationModificator : Modificator
    {
        public enum Operation
        {
            Add,
            Multiply,
        }

        [SerializeField] private Operation operation;
        [SerializeField] private AttributeType attributeType;
        [SerializeField] private float modifierValue;

        public override void ApplyModification(AttributeSet attributeSet, float multiplier = 1)
        {
            Attribute attribute = attributeSet.Attributes[attributeType];
            float newValue = attribute.GetAttribute();

            switch (operation)
            {
                case Operation.Add:
                    newValue += modifierValue * multiplier;
                    break;
                case Operation.Multiply:
                    newValue *= modifierValue * multiplier;
                    break;
            }

            attribute.SetAttribute(newValue);
        }
    }
}