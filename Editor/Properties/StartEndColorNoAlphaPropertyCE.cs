using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndColorNoAlphaProperty))]
    public class StartEndColorNoAlphaPropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty startColorProperty = property.FindPropertyRelative("startColor");
            SerializedProperty endColorProperty = property.FindPropertyRelative("endColor");

            EditorGUI.PropertyField(position, useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Start Color");
                        EditorGUILayout.PropertyField(startColorProperty, GUIContent.none);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("End Color");
                    EditorGUILayout.PropertyField(endColorProperty, GUIContent.none);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.EndProperty();
        }
    }
}
