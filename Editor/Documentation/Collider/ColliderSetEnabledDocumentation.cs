using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class ColliderSetEnabledDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(ColliderSetEnabledFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Enables or disables the target Collider", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Collider that is going to be enabled/disabled", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Set Enabled: enables or disables the Collider", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}