using System;
using UnityEditor;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(LoopElement))]
    public class LoopElementCE : Editor
    {
        private LoopElement CustomTarget => (LoopElement)target;

        private SerializedProperty loopModeProperty;
        private SerializedProperty loopResetModeProperty;
        private SerializedProperty loopsProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(loopModeProperty);

            if((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes || (LoopMode)loopModeProperty.enumValueIndex == LoopMode.UntilManuallyStoped)
            {
                EditorGUILayout.PropertyField(loopResetModeProperty);
            }

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes)
            {
                EditorGUILayout.PropertyField(loopsProperty);
            }

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            loopModeProperty = serializedObject.FindProperty("loopMode");
            loopResetModeProperty = serializedObject.FindProperty("loopResetMode");
            loopsProperty = serializedObject.FindProperty("loops");
        }
    }
}
