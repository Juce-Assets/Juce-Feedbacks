using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndTransformVector3Property))]
    public class StartEndTransformVector3PropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty useStartXProperty = property.FindPropertyRelative("useStartX");
            SerializedProperty useStartYProperty = property.FindPropertyRelative("useStartY");
            SerializedProperty useStartZProperty = property.FindPropertyRelative("useStartZ");
            SerializedProperty useEndXProperty = property.FindPropertyRelative("useEndX");
            SerializedProperty useEndYProperty = property.FindPropertyRelative("useEndY");
            SerializedProperty useEndZProperty = property.FindPropertyRelative("useEndZ");
            SerializedProperty startValueProperty = property.FindPropertyRelative("startValue");
            SerializedProperty endValueProperty = property.FindPropertyRelative("endValue");

            EditorGUI.PropertyField(position, useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.LabelField("Start");

                    EditorGUILayout.PropertyField(startValueProperty);

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartXProperty, new GUIContent("X"));
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartYProperty, new GUIContent("Y"));
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartZProperty, new GUIContent("Z"));
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("End");

                EditorGUILayout.PropertyField(endValueProperty);

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndXProperty, new GUIContent("X"));
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndYProperty, new GUIContent("Y"));
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndZProperty, new GUIContent("Z"));
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.EndProperty();
        }
    }
}
