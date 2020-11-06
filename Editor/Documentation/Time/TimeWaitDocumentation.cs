using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class WaitTimeFeedbackDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(WaitTimeFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("On the current sequence, waits a given ammount of seconds.", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DurationDocumentation();
            }
        }
    }
}