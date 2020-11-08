#if JUCE_TEXT_MESH_PRO_EXTENSIONS

using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class TextMeshProTextDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TextMeshProTextFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target TextMeshProUGUI text", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: TextMeshProUGUI component that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Value: text o set", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}

#endif