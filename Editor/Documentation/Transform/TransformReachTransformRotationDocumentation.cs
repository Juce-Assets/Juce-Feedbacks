using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class TransformReachTransformRotationDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TransformReachTransformRotationFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Rotates the target Transform to match the world rotation of another Transform", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Transform component that is going to be rotated", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting Transform", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting Transform rotation value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end Transform rotation value to reach", EditorStyles.wordWrappedLabel);
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