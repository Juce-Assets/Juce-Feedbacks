using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class EventTriggerDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(EventTriggerFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Sends an event in the form of a string through the FeedbacksPlayer OnEventTrigger callback", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- EventTrigger: string that is going to be sent with the event", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}