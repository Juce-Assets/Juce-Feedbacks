using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(Vector2Element))]
    public class Vector2ElementCE : Editor
    {
        private Vector2Element CustomTarget => (Vector2Element)target;

        private SerializedProperty useStartValueProperty;
        private SerializedProperty useStartXProperty;
        private SerializedProperty useStartYProperty;
        private SerializedProperty useEndXProperty;
        private SerializedProperty useEndYProperty;
        private SerializedProperty startValueXProperty;
        private SerializedProperty startValueYProperty;
        private SerializedProperty endValueXProperty;
        private SerializedProperty endValueYProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(useStartValueProperty);

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

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            useStartValueProperty = serializedObject.FindProperty("useStartValue");
            useStartXProperty = serializedObject.FindProperty("useStartX");
            useStartYProperty = serializedObject.FindProperty("useStartY");
            useEndXProperty = serializedObject.FindProperty("useEndX");
            useEndYProperty = serializedObject.FindProperty("useEndY");
            startValueXProperty = serializedObject.FindProperty("startValueX");
            startValueYProperty = serializedObject.FindProperty("startValueY");
            endValueXProperty = serializedObject.FindProperty("endValueX");
            endValueYProperty = serializedObject.FindProperty("endValueY");
        }
    }
}
