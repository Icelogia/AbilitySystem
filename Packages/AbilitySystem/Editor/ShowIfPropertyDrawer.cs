using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfPropertyDrawer : PropertyDrawer
{
    private ShowIfAttribute showIf;
    SerializedProperty comparedField;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShowMe(property) && showIf.disablingType == ShowIfAttribute.DisablingType.DontDraw)
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
        else
        {
            if (property.propertyType == SerializedPropertyType.Generic)
            {
                int numChildren = 0;
                float totalHeight = 0.0f;

                IEnumerator children = property.GetEnumerator();
                HashSet<SerializedProperty> drawnprops = new HashSet<SerializedProperty>();

                while (children.MoveNext())
                {
                    SerializedProperty child = children.Current as SerializedProperty;
                    if (drawnprops.Contains(child))
                    {
                        continue;
                    }
                    drawnprops.Add(child);

                    GUIContent childLabel = new GUIContent(child.displayName);

                    totalHeight += EditorGUI.GetPropertyHeight(child, childLabel) + EditorGUIUtility.standardVerticalSpacing;
                    numChildren++;
                }

                // Remove extra space at end, (we only want spaces between items)
                totalHeight -= EditorGUIUtility.standardVerticalSpacing;

                return totalHeight;
            }

            return EditorGUI.GetPropertyHeight(property, label);
        }
    }

    private bool ShowMe(SerializedProperty property)
    {
        showIf = attribute as ShowIfAttribute;
        // Replace propertyname to the value from the parameter
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, showIf.comparedPropertyName) : showIf.comparedPropertyName;
        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        // get the value & compare based on types
        switch (comparedField.type)
        { // Possible extend cases to support your own type
            case "bool":
                return comparedField.boolValue.Equals(showIf.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)showIf.comparedValue);
            default:
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                return true;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (ShowMe(property))
        {
            // A Generic type means a custom class...
            if (property.propertyType == SerializedPropertyType.Generic)
            {
                IEnumerator children = property.GetEnumerator();

                Rect offsetPosition = position;
                HashSet<SerializedProperty> drawnprops = new HashSet<SerializedProperty>();

                while (children.MoveNext())
                {
                    SerializedProperty child = children.Current as SerializedProperty;
                    if (drawnprops.Contains(child))
                    {
                        continue;
                    }

                    GUIContent childLabel = new GUIContent(child.displayName);

                    float childHeight = EditorGUI.GetPropertyHeight(child, childLabel);
                    offsetPosition.height = childHeight;

                    EditorGUI.PropertyField(offsetPosition, child, childLabel);

                    offsetPosition.y += childHeight + EditorGUIUtility.standardVerticalSpacing;
                    drawnprops.Add(child);
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }

        } //...check if the disabling type is read only. If it is, draw it disabled
        else if (showIf.disablingType == ShowIfAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}