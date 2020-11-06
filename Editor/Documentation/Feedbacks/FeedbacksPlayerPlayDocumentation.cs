using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class FeedbacksPlayerPlayDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(FeedbacksPlayerPlayFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Play the target FeedbacksPlayer", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: FeedbacksPlayer that is going to be played", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}