using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(Vector3Element))]
    public class Vector3ElementCE : Editor
    {
        private Vector3Element CustomTarget => (Vector3Element)target;

        private SerializedProperty useStartValueProperty;
        private SerializedProperty useStartXProperty;
        private SerializedProperty useStartYProperty;
        private SerializedProperty useStartZProperty;
        private SerializedProperty useEndXProperty;
        private SerializedProperty useEndYProperty;
        private SerializedProperty useEndZProperty;
        private SerializedProperty startValueXProperty;
        private SerializedProperty startValueYProperty;
        private SerializedProperty startValueZProperty;
        private SerializedProperty endValueXProperty;
        private SerializedProperty endValueYProperty;
        private SerializedProperty endValueZProperty;

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
            useStartZProperty = serializedObject.FindProperty("useStartZ");
            useEndXProperty = serializedObject.FindProperty("useEndX");
            useEndYProperty = serializedObject.FindProperty("useEndY");
            useEndZProperty = serializedObject.FindProperty("useEndZ");
            startValueXProperty = serializedObject.FindProperty("startValueX");
            startValueYProperty = serializedObject.FindProperty("startValueY");
            startValueZProperty = serializedObject.FindProperty("startValueZ");
            endValueXProperty = serializedObject.FindProperty("endValueX");
            endValueYProperty = serializedObject.FindProperty("endValueY");
            endValueZProperty = serializedObject.FindProperty("endValueZ");
        }
    }
}
