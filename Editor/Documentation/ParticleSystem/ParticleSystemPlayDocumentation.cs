using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class ParticleSystemPlayDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(ParticleSystemPlayFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Plays the target ParticleSystem", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: ParticleSystem that is going to be played", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- With Children: play all child ParticleSystem aswell", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}