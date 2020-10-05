using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(Vector3Element))]
    public class Vector3ElementCE : Editor
    {
        //[SerializeField] [HideInInspector] private bool useStartPosition = default;
        //[SerializeField] [HideInInspector] private bool useX = default;
        //[SerializeField] [HideInInspector] private bool useY = default;
        //[SerializeField] [HideInInspector] private bool useZ = default;
        //[SerializeField] [HideInInspector] private Vector3 startPosition = default;
        //[SerializeField] [HideInInspector] private Vector3 endPosition = default;

        private Vector3Element CustomTarget => (Vector3Element)target;

        private SerializedProperty useStartPositionProperty;
        private SerializedProperty useStartXProperty;
        private SerializedProperty useStartYProperty;
        private SerializedProperty useStartZProperty;
        private SerializedProperty useEndXProperty;
        private SerializedProperty useEndYProperty;
        private SerializedProperty useEndZProperty;
        private SerializedProperty startPositionXProperty;
        private SerializedProperty startPositionYProperty;
        private SerializedProperty startPositionZProperty;
        private SerializedProperty endPositionXProperty;
        private SerializedProperty endPositionYProperty;
        private SerializedProperty endPositionZProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(useStartPositionProperty);

            if (useStartPositionProperty.boolValue)
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
                            EditorGUILayout.PropertyField(startPositionXProperty, GUIContent.none);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartYProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                        using (new EditorGUI.DisabledScope(!useStartYProperty.boolValue))
                        {
                            EditorGUILayout.LabelField("Y");
                            EditorGUILayout.PropertyField(startPositionYProperty, GUIContent.none);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(useStartZProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                        using (new EditorGUI.DisabledScope(!useStartZProperty.boolValue))
                        {
                            EditorGUILayout.LabelField("Z");
                            EditorGUILayout.PropertyField(startPositionZProperty, GUIContent.none);
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
                        EditorGUILayout.PropertyField(endPositionXProperty, GUIContent.none);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndYProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                    using (new EditorGUI.DisabledScope(!useEndYProperty.boolValue))
                    {
                        EditorGUILayout.LabelField("Y");
                        EditorGUILayout.PropertyField(endPositionYProperty, GUIContent.none);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(useEndZProperty, new GUIContent(""), GUILayout.MaxWidth(15));

                    using (new EditorGUI.DisabledScope(!useEndZProperty.boolValue))
                    {
                        EditorGUILayout.LabelField("Z");
                        EditorGUILayout.PropertyField(endPositionZProperty, GUIContent.none);
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
            useStartPositionProperty = serializedObject.FindProperty("useStartPosition");
            useStartXProperty = serializedObject.FindProperty("useStartX");
            useStartYProperty = serializedObject.FindProperty("useStartY");
            useStartZProperty = serializedObject.FindProperty("useStartZ");
            useEndXProperty = serializedObject.FindProperty("useEndX");
            useEndYProperty = serializedObject.FindProperty("useEndY");
            useEndZProperty = serializedObject.FindProperty("useEndZ");
            startPositionXProperty = serializedObject.FindProperty("startPositionX");
            startPositionYProperty = serializedObject.FindProperty("startPositionY");
            startPositionZProperty = serializedObject.FindProperty("startPositionZ");
            endPositionXProperty = serializedObject.FindProperty("endPositionX");
            endPositionYProperty = serializedObject.FindProperty("endPositionY");
            endPositionZProperty = serializedObject.FindProperty("endPositionZ");
        }
    }
}
