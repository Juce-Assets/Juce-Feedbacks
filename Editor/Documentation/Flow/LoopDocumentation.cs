using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class LoopDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(LoopFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Loops feedbacks that are over this feedback. If there is a LoopStartFeedback (not mandatory) " +
                "on top, it will only loop the feedbacks that are in between the two. If there is a WaitAllAbove, " +
                "it will only loop the feedbacks that are in between the two.", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.LoopDocumentation();
            }
        }
    }
}