using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class AudioSourceStopDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(AudioSourceStopFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Stops an AudioSource", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: AudioSource component that is going to be affected", EditorStyles.wordWrappedLabel);
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