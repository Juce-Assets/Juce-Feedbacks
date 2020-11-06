using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class FeedbacksPlayerCompleteDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(FeedbacksPlayerCompleteFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Instantly completes the target FeedbacksPlayer", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: FeedbacksPlayer that is going to be completed", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}