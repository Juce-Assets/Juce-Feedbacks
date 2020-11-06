using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class AudioSourceVolumeDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(AudioSourceVolumeFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target AudioSource volume value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: AudioSource component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting volume", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting volume value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end volume value to reach", EditorStyles.wordWrappedLabel);
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