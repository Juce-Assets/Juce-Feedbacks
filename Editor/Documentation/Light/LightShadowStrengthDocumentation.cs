using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class LightShadowStrengthDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(LightShadowStrengthFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target Light shadow strength", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Light component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting shadow strength", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting shadow strength value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end shadow strength value to reach", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
                GenericsDocumentation.DurationDocumentation();
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.EasingDocumentation();
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.LoopDocumentation();
            }
        }
    }
}