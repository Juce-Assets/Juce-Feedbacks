using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class RendererMaterialKeywordSetEnabledDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(RendererMaterialKeywordSetEnabledFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Enables or disables the target RenderMaterial keyword", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: RenderMaterial and keyword that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Set Enabled: enables or disables the keyword", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}