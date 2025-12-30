using System.Collections.Generic;
using ShatteredIceStudio.AbilitySystem.Effectors;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    [CustomEditor(typeof(AttributeSet), true), CanEditMultipleObjects]
    public class AttributeSetEditor : Editor
    {
        private const string debugging = "Debugging";
        private const string activeEffectors = "Active Effectors";
        private const string historyEffectors = "Effectors History";

        private bool showActiveEffectors = false;
        private bool showHistoryEffectors = false;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            AttributeSet attributeSet = (AttributeSet)target;

            var debugLabel = new Label(debugging) { style = { unityFontStyleAndWeight = FontStyle.Bold } };
            var debuggingContainer = CreateDebuggingContainer();
            var activeFoldout = CreateEffectorContextFoldOut(attributeSet.ActiveEffectors, activeEffectors, showActiveEffectors);
            var historyFoldout = CreateEffectorContextFoldOut(attributeSet.EffectorsHistory, historyEffectors, showHistoryEffectors);

            debuggingContainer.Add(activeFoldout);
            debuggingContainer.Add(historyFoldout);

            root.Add(debugLabel);
            root.Add(debuggingContainer);

            return root; ;
        }

        private VisualElement CreateDebuggingContainer()
        {
            var debuggingContainer = new VisualElement();
            debuggingContainer.style.paddingTop = 5;
            debuggingContainer.style.paddingBottom = 5;

            debuggingContainer.style.backgroundColor = new Color(0f, 0f, 0f, 0.15f);

            debuggingContainer.style.marginTop = 5;
            debuggingContainer.style.marginBottom = 5;

            debuggingContainer.style.borderTopLeftRadius = 5;
            debuggingContainer.style.borderTopRightRadius = 5;
            debuggingContainer.style.borderBottomLeftRadius = 5;
            debuggingContainer.style.borderBottomRightRadius = 5;

            Color borderColor = Color.white;
            borderColor.a = 0.5f;

            debuggingContainer.style.borderBottomColor = borderColor;
            debuggingContainer.style.borderTopColor = borderColor;
            debuggingContainer.style.borderLeftColor = borderColor;
            debuggingContainer.style.borderRightColor = borderColor;

            debuggingContainer.style.borderBottomWidth = 1;
            debuggingContainer.style.borderTopWidth = 1;
            debuggingContainer.style.borderLeftWidth = 1;
            debuggingContainer.style.borderRightWidth = 1;

            return debuggingContainer;
        }

        private VisualElement CreateEffectorContextFoldOut(List<EffectorContext> effectors, string text, bool value)
        {
            VisualElement foldoutContainer = new VisualElement();
            foldoutContainer.style.paddingRight = 15;
            foldoutContainer.style.paddingLeft = 15;

            var foldout = new Foldout() { text = text, value = value };
            foldout.style.flexGrow = 0;
            foldout.style.alignSelf = Align.FlexStart;


            foreach (var context in effectors)
            {
                foldout.Add(CreateEffectorContextElement(context));
            }

            foldoutContainer.Add(foldout);
            return foldoutContainer;
        }

        private VisualElement CreateEffectorContextElement(EffectorContext context)
        {
            var element = new VisualElement();
            element.style.flexDirection = FlexDirection.Row;
            element.style.marginBottom = 2;

            // Effector field
            var effectorField = new ObjectField()
            {
                objectType = typeof(Effector),
                value = context.Effector,
                allowSceneObjects = false,
                style = { flexGrow = 1 }
            };
            effectorField.SetEnabled(false); // read-only
            
            // Owner field
            var ownerField = new ObjectField()
            {
                objectType = typeof(GameObject),
                value = context.Owner != null ? context.Owner.gameObject : null,
                allowSceneObjects = true,
                style = { flexGrow = 1 }
            };
            ownerField.SetEnabled(false); // read-only

            element.Add(effectorField);
            element.Add(ownerField);

            return element;
        }
    }
}

