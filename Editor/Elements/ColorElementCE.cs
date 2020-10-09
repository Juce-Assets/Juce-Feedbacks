using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(ColorElement))]
    public class ColorElementCE : Editor
    {
        private ColorElement CustomTarget => (ColorElement)target;

        private SerializedProperty useStartValueProperty;
        private SerializedProperty startValueProperty;
        private SerializedProperty endValueProperty;

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

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            useStartValueProperty = serializedObject.FindProperty("useStartValue");
            startValueProperty = serializedObject.FindProperty("startValue");
            endValueProperty = serializedObject.FindProperty("endValue");
        }
    }
}
