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
        [Tooltip("Modifier applies to both Int and Float attirubte values")]
        [SerializeField] private float modifierValue;
        [SerializeField] private Vector2 modifierValueVector2;

        public override void ApplyModification(AttributeSet attributeSet, AttributeSet owner)
        {
            Attribute attribute = attributeSet.Attributes[attributeType];

            switch(attribute)
            {
                case IntAttribute intAttribute:
                    ApplyIntModification(intAttribute, owner);
                    break;
                case FloatAttribute floatAttribute:
                    ApplyFloatModification(floatAttribute, owner);
                    break;
                case Vector2Attribute vector2Attribute:
                    ApplyVecotr2Modification(vector2Attribute, owner);
                    break;
            }
        }

        public void ApplyIntModification(IntAttribute attribute, AttributeSet owner)
        {
            int newValue = attribute.Get();

            switch (operation)
            {
                case Operation.Add:
                    newValue += MathExtensions.Round(modifierValue);
                    break;
                case Operation.Multiply:
                    newValue *= MathExtensions.Round(modifierValue);
                    break;
            }

            attribute.Set(newValue);
        }

        public void ApplyFloatModification(FloatAttribute attribute, AttributeSet owner)
        {
            float newValue = attribute.Get();

            switch (operation)
            {
                case Operation.Add:
                    newValue += MathExtensions.Round(modifierValue);
                    break;
                case Operation.Multiply:
                    newValue *= MathExtensions.Round(modifierValue);
                    break;
            }

            attribute.Set(newValue);
        }

        public void ApplyVecotr2Modification(Vector2Attribute attribute, AttributeSet owner)
        {
            Vector2 newValue = attribute.Get();

            switch (operation)
            {
                case Operation.Add:
                    newValue += modifierValueVector2;
                    break;
                case Operation.Multiply:
                    newValue *= modifierValueVector2;
                    break;
            }

            attribute.Set(newValue);
        }
    }
}