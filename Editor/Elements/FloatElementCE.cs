using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(FloatElement))]
    public class FloatElementCE : Editor
    {
        private FloatElement CustomTarget => (FloatElement)target;

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

                        CapValue(startValueProperty);
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

                    CapValue(endValueProperty);

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

        private void CapValue(SerializedProperty property)
        {
            if (property.floatValue > CustomTarget.MaxValue)
            {
                property.floatValue = CustomTarget.MaxValue;
            }

            if (property.floatValue < CustomTarget.MinValue)
            {
                property.floatValue = CustomTarget.MinValue;
            }
        }
    }
}
