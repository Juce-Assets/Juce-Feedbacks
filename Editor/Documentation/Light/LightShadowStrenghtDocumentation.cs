using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class LightShadowStrenghtDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(LightShadowStrenghtFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target Light shadow strenght", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Light component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting shadow strenght", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting shadow strenght value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end shadow strenght value to reach", EditorStyles.wordWrappedLabel);
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