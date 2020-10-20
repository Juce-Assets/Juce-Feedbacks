using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(Feedback))]
    public class FeedbackCE : Editor
    {
        private Feedback CustomTarget => (Feedback)target;

        private SerializedProperty userDataProperty;
        private SerializedProperty delayProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(delayProperty);

            base.DrawDefaultInspector();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            userDataProperty = serializedObject.FindProperty("userData");
            delayProperty = serializedObject.FindProperty("delay");
        }
    }
}
