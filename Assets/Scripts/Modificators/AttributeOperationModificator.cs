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

        public override void ApplyModification(AttributeSet attributeSet, float multiplier = 1)
        {
            Attribute attribute = attributeSet.Attributes[attributeType];

            switch(attribute)
            {
                case IntAttribute intAttribute:
                    ApplyIntModification(intAttribute, multiplier);
                    break;
                case FloatAttribute floatAttribute:
                    ApplyFloatModification(floatAttribute, multiplier);
                    break;
                case Vector2Attribute vector2Attribute:
                    ApplyVecotr2Modification(vector2Attribute, multiplier);
                    break;
            }
        }

        public void ApplyIntModification(IntAttribute attribute, float multiplier)
        {
            int newValue = attribute.Get();

            switch (operation)
            {
                case Operation.Add:
                    newValue += MathExtensions.Round(modifierValue * multiplier);
                    break;
                case Operation.Multiply:
                    newValue *= MathExtensions.Round(modifierValue * multiplier);
                    break;
            }

            attribute.Set(newValue);
        }

        public void ApplyFloatModification(FloatAttribute attribute, float multiplier)
        {
            float newValue = attribute.Get();

            switch (operation)
            {
                case Operation.Add:
                    newValue += MathExtensions.Round(modifierValue * multiplier);
                    break;
                case Operation.Multiply:
                    newValue *= MathExtensions.Round(modifierValue * multiplier);
                    break;
            }

            attribute.Set(newValue);
        }

        public void ApplyVecotr2Modification(Vector2Attribute attribute, float multiplier)
        {
            Vector2 newValue = attribute.Get();

            switch (operation)
            {
                case Operation.Add:
                    newValue += modifierValueVector2 * multiplier;
                    break;
                case Operation.Multiply:
                    newValue *= modifierValueVector2 * multiplier;
                    break;
            }

            attribute.Set(newValue);
        }
    }
}