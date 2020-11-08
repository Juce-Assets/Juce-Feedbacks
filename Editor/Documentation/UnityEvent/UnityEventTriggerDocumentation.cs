using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class UnityEventTriggerDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(UnityEventTriggerFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Invokes the UnityEvents defined", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- EventTrigger: UnityEvents that will be invoked", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}