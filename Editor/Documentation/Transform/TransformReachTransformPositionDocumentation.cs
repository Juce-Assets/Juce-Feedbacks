using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class TransformReachTransformPositionDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TransformReachTransformPositionFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Moves the target Transform to match the world position of another Transform", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Transform component that is going to be moved", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Coordinates Space: use local or world position to move the transform", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Use Starting Value: enables the starting Transform", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting Transform position value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end Transform position value to reach", EditorStyles.wordWrappedLabel);
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