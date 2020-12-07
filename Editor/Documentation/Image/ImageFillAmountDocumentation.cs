using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class ImageFillAmountDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(ImageFillAmountFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target Image fill amount value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Image component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting fill amount", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting fill amount value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end fill amount value to reach", EditorStyles.wordWrappedLabel);
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