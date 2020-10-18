using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{ 
    [CustomPropertyDrawer(typeof(StartEndColorProperty))]
    public class StartEndColorPropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty useStartColorProperty = property.FindPropertyRelative("useStartColor");
            SerializedProperty useStartAlphaProperty = property.FindPropertyRelative("useStartAlpha");
            SerializedProperty useEndColorProperty = property.FindPropertyRelative("useEndColor");
            SerializedProperty useEndAlphaProperty = property.FindPropertyRelative("useEndAlpha");
            SerializedProperty startColorProperty = property.FindPropertyRelative("startColor");
            SerializedProperty startAlphaProperty = property.FindPropertyRelative("startAlpha");
            SerializedProperty endColorProperty = property.FindPropertyRelative("endColor");
            SerializedProperty endAlphaProperty = property.FindPropertyRelative("endAlpha");

            EditorGUI.PropertyField(position, useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.LabelField("Start");

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartColorProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                        using (new EditorGUI.DisabledScope(!useStartColorProperty.boolValue))
                        {
                            EditorGUILayout.LabelField("Color");
                            EditorGUILayout.PropertyField(startColorProperty, GUIContent.none);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartAlphaProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                        using (new EditorGUI.DisabledScope(!useStartAlphaProperty.boolValue))
                        {
                            EditorGUILayout.LabelField("Alpha");
                            EditorGUILayout.PropertyField(startAlphaProperty, GUIContent.none);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("End");

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndColorProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                    using (new EditorGUI.DisabledScope(!useEndColorProperty.boolValue))
                    {
                        EditorGUILayout.LabelField("Color");
                        EditorGUILayout.PropertyField(endColorProperty, GUIContent.none);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndAlphaProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                    using (new EditorGUI.DisabledScope(!useEndAlphaProperty.boolValue))
                    {
                        EditorGUILayout.LabelField("Alpha");
                        EditorGUILayout.PropertyField(endAlphaProperty, GUIContent.none);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.EndProperty();
        }
    }
}
