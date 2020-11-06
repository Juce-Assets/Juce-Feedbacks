using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class FeedbacksPlayerKillDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(FeedbacksPlayerKillFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Instantly kills the target FeedbacksPlayer", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: FeedbacksPlayer that is going to be killed", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}