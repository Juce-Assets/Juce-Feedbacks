using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class RectTransformAnchorMinMaxDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(RectTransformAnchorMinMaxFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target RectTransform anchorMin and anchorMax property to a certain value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: RectTransform that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Values (Min)", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Use Starting Value: enables the starting value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting anchorMin value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end anchorMin value to reach", EditorStyles.wordWrappedLabel);
            }


            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Values (Max)", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Use Starting Value: enables the starting value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting anchorMax value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end anchorMax value to reach", EditorStyles.wordWrappedLabel);
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