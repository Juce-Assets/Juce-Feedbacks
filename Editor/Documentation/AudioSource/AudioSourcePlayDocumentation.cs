using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class AudioSourcePlayDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(AudioSourcePlayFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Plays an AudioSource", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: AudioSource component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Audio Clip: clip to be played", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- One Shot: does not cancel clips that are already being played if enabled", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.LoopDocumentation();
            }
        }
    }
}