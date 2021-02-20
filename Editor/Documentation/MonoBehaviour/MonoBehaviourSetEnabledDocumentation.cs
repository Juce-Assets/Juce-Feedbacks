using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class MonoBehaviourSetEnabledDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(MonoBehaviourSetEnabledFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Enables or disables the target MonoBehaviour", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: MonoBehaviour that is going to be enabled/disabled", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Set Enabled: enables or disables the MonoBehaviour", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}