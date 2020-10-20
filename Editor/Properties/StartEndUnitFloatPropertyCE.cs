using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndUnitFloatProperty))]
    public class StartEndUnitFloatPropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty startValueProperty = property.FindPropertyRelative("startValue");
            SerializedProperty endValueProperty = property.FindPropertyRelative("endValue");

            EditorGUI.PropertyField(position, useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Start");
                        EditorGUILayout.PropertyField(startValueProperty, GUIContent.none);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("End");
                    EditorGUILayout.PropertyField(endValueProperty, GUIContent.none);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.EndProperty();
        }
    }
}
