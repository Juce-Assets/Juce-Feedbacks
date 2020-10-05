using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(EasingElement))]
    public class EasingElementCE : Editor
    {
        private EasingElement CustomTarget => (EasingElement)target;

        private SerializedProperty useAnimationCurveProperty;
        private SerializedProperty easingProperty;
        private SerializedProperty animationCurveEasingProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(useAnimationCurveProperty);

            if (!useAnimationCurveProperty.boolValue)
            {
                EditorGUILayout.PropertyField(easingProperty, new GUIContent("Ease"));
            }
            else
            {
                EditorGUILayout.PropertyField(animationCurveEasingProperty, new GUIContent("Ease"));
            }

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            useAnimationCurveProperty = serializedObject.FindProperty("useAnimationCurve");
            easingProperty = serializedObject.FindProperty("easing");
            animationCurveEasingProperty = serializedObject.FindProperty("animationCurveEasing");
        }
    }
}
