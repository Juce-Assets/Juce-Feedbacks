using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class Collider2DSetEnabledDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(Collider2DSetEnabledFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Enables or disables the target Collider2D", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Collider2D that is going to be enabled/disabled", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Set Enabled: enables or disables the Collider2D", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}