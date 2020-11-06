using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class GameObjectSetActiveDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(GameObjectSetActiveFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Enables or disables the target GameObject", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: GameObject that is going to be enabled/disabled", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Set Active: enables or disables the GameObject", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}