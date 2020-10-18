using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndVector2Property))]
    public class StartEndVector2PropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty useStartXProperty = property.FindPropertyRelative("useStartX");
            SerializedProperty useStartYProperty = property.FindPropertyRelative("useStartY");
            SerializedProperty useEndXProperty = property.FindPropertyRelative("useEndX");
            SerializedProperty useEndYProperty = property.FindPropertyRelative("useEndY");
            SerializedProperty startValueXProperty = property.FindPropertyRelative("startValueX");
            SerializedProperty startValueYProperty = property.FindPropertyRelative("startValueY");
            SerializedProperty endValueXProperty = property.FindPropertyRelative("endValueX");
            SerializedProperty endValueYProperty = property.FindPropertyRelative("endValueY");

            EditorGUI.PropertyField(position, useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.LabelField("Start");

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartXProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                        using (new EditorGUI.DisabledScope(!useStartXProperty.boolValue))
                        {
                            EditorGUILayout.LabelField("X");
                            EditorGUILayout.PropertyField(startValueXProperty, GUIContent.none);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartYProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                        using (new EditorGUI.DisabledScope(!useStartYProperty.boolValue))
                        {
                            EditorGUILayout.LabelField("Y");
                            EditorGUILayout.PropertyField(startValueYProperty, GUIContent.none);
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
                    EditorGUILayout.PropertyField(useEndXProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                    using (new EditorGUI.DisabledScope(!useEndXProperty.boolValue))
                    {
                        EditorGUILayout.LabelField("X");
                        EditorGUILayout.PropertyField(endValueXProperty, GUIContent.none);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndYProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                    using (new EditorGUI.DisabledScope(!useEndYProperty.boolValue))
                    {
                        EditorGUILayout.LabelField("Y");
                        EditorGUILayout.PropertyField(endValueYProperty, GUIContent.none);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.EndProperty();
        }
    }
}
