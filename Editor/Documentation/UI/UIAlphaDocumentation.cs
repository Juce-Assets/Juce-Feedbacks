using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class UIAlphaDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(UIAlphaFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Using a CanvasGroup, can set the alpha value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: GameObject where CanvasGroup is going to be created", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting alpha", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting alpha value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end alpha value to reach", EditorStyles.wordWrappedLabel);
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