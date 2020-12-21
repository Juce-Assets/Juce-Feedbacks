using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class FeedbacksPlayerKillAndResetDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(FeedbacksPlayerKillAndResetFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Instantly kills and resets the FeedbacksPlayer target values, to the ones at" +
                " the beginning, when it started playing", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: FeedbacksPlayer that is going to be killed and reset", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}