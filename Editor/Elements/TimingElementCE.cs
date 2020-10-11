using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(TimingElement))]
    public class TimingElementCE : Editor
    {
        private TimingElement CustomTarget => (TimingElement)target;

        private SerializedProperty delayProperty;
        private SerializedProperty useDurationProperty;
        private SerializedProperty durationProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(delayProperty);

            if(delayProperty.floatValue < 0)
            {
                delayProperty.floatValue = 0.0f;
            }

            if (useDurationProperty.boolValue)
            {
                EditorGUILayout.PropertyField(durationProperty);

                if (durationProperty.floatValue < 0)
                {
                    durationProperty.floatValue = 0.0f;
                }
            }

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            delayProperty = serializedObject.FindProperty("delay");
            useDurationProperty = serializedObject.FindProperty("useDuration");
            durationProperty = serializedObject.FindProperty("duration");
        }
    }
}
