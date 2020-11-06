using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class TransformReachTransformScaleDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TransformReachTransformScaleFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Scales the target Transform to match the scale of another Transform", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Transform component that is going to be scaled", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting Transform", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting Transform scale value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end Transform scale value to reach", EditorStyles.wordWrappedLabel);
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