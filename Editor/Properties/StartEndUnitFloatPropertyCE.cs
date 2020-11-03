using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndUnitFloatProperty))]
    public class StartEndUnitFloatPropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 2;

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");

            if (useStartValueProperty.boolValue)
            {
                ++elementsCount;
            }

            return layoutHelper.GetHeightOfElements(elementsCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty startValueProperty = property.FindPropertyRelative("startValue");
            SerializedProperty endValueProperty = property.FindPropertyRelative("endValue");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), startValueProperty, new GUIContent("Start"));
            }

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), endValueProperty, new GUIContent("End"));

            EditorGUI.EndProperty();
        }
    }
}