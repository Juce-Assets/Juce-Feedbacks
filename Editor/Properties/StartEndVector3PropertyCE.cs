using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndVector3Property))]
    public class StartEndVector3PropertyCE : PropertyDrawer
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
            SerializedProperty startValueXProperty = property.FindPropertyRelative("startValueX");
            SerializedProperty startValueYProperty = property.FindPropertyRelative("startValueY");
            SerializedProperty startValueZProperty = property.FindPropertyRelative("startValueZ");
            SerializedProperty endValueXProperty = property.FindPropertyRelative("endValueX");
            SerializedProperty endValueYProperty = property.FindPropertyRelative("endValueY");
            SerializedProperty endValueZProperty = property.FindPropertyRelative("endValueZ");

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

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartZProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                        using (new EditorGUI.DisabledScope(!useStartZProperty.boolValue))
                        {
                            EditorGUILayout.LabelField("Z");
                            EditorGUILayout.PropertyField(startValueZProperty, GUIContent.none);
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

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndZProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                    using (new EditorGUI.DisabledScope(!useEndZProperty.boolValue))
                    {
                        EditorGUILayout.LabelField("Z");
                        EditorGUILayout.PropertyField(endValueZProperty, GUIContent.none);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.EndProperty();
        }
    }
}
