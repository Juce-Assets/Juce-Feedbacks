using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class ImageSpriteDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(ImageSpriteFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target Image sprite", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Image component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Sprite: sprite to set", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}