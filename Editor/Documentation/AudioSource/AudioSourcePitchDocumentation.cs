using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class AudioSourcePitchDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(AudioSourcePitchFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target AudioSource pitch value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: AudioSource component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting pitch", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting pitch value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end pitch value to reach", EditorStyles.wordWrappedLabel);
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