using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    [CustomPropertyDrawer(typeof(Attribute), true)]
    public class AttributeDrawer : PropertyDrawer
    {
        private const string attributeValue = "attribute";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var attributeProperty = property.FindPropertyRelative(attributeValue);

            VisualElement attributeField;

            switch (attributeProperty.propertyType)
            {
                case SerializedPropertyType.Integer:
                    attributeField = new IntegerField(property.displayName);
                    ((IntegerField)attributeField).BindProperty(attributeProperty);
                    break;
                case SerializedPropertyType.Float:
                    attributeField = new FloatField(property.displayName);
                    ((FloatField)attributeField).BindProperty(attributeProperty);
                    break;
                case SerializedPropertyType.Vector2:
                    attributeField = new Vector2Field(property.displayName);
                    ((Vector2Field)attributeField).BindProperty(attributeProperty);
                    break;
                default:
                    Debug.LogError(attributeProperty.propertyType.ToString() + " is not supported in property drawer!");
                    return root;
            }

            root.Add(attributeField);

            return root;
        }
    }
}
