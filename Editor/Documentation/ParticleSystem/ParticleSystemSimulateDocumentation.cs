using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class ParticleSystemSimulateDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(ParticleSystemSimulateFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Fast-forwards the ParticleSystem by simulating particles over the given period of time, then pauses it.", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: ParticleSystem that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Time: time period in seconds to advance the ParticleSystem simulation by", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- With Children: play all child ParticleSystem aswell", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}