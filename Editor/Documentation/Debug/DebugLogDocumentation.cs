using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class DebugLogDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(DebugLogFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Prints a log to the Unity console", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Log: string to print on the console", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}