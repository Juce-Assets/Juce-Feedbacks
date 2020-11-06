using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class ImageColorDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(ImageColorFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target Image to a certain color", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Image component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting color", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting color value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end color value to reach", EditorStyles.wordWrappedLabel);
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