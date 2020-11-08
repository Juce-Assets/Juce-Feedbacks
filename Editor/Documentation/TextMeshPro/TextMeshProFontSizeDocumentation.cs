#if JUCE_TEXT_MESH_PRO_EXTENSIONS

using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class TextMeshProFontSizeDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TextMeshProFontSizeFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target TextMeshProUGUI font size to a certain value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: TextMeshProUGUI component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting font size", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting font size value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end font size value to reach", EditorStyles.wordWrappedLabel);
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

#endif