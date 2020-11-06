using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class StartLoopDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(StartLoopFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Marks the start of a loop for LoopFeedback. It's not necessary " +
                "for the LoopFeedback to work, but it can be useful in some situations", EditorStyles.wordWrappedLabel);
        }
    }
}