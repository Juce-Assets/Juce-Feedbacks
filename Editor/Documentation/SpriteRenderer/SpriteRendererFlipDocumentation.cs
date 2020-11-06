using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class SpriteRendererFlipDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(SpriteRendererFlipFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target SpriteRenderer flipX and flipY values", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: SpriteRenderer component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- FlipX: enable or disable flip x value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- FlipY: enable or disable flip y value", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}