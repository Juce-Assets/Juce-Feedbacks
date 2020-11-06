using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class UIInteractableDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(UIInteractableFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Using a CanvasGroup, can set interactable and blocks raycast value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: GameObject where CanvasGroup is going to be created", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Interactable: Sets CanvasGroup interactable property", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- BlocksRaycast: Sets CanvasGroup BlocksRaycasts property", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}