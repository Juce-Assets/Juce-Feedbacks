using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class GraphicMaterialColorDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(GraphicMaterialColorFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Changes the target Graphic color property to a certain value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Graphic and color property that is going to be affected", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Use Starting Value: enables the starting color value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting color value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end color value to reach", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
                GenericsDocumentation.DurationDocumentation();
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.EasingDocumentation();
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.LoopDocumentation();
            }
        }
    }
}