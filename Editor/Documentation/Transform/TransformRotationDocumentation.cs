using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class TransformRotationDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TransformRotationFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Rotates the target Transform to a certain value", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Transform component that is going to be rotated", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                RotationModeDocumentation();
                GUILayout.Label("- Coordinates Space: use local or world rotation to rotate the transform", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Use Starting Value: enables the starting rotation", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (if enabled) starting rotation value", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: end rotation value to reach", EditorStyles.wordWrappedLabel);
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

        private void RotationModeDocumentation()
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- RotationMode:", EditorStyles.wordWrappedLabel);
                GUILayout.Label("   Fast: The rotation will take the shortest route and will not rotate more than 360", EditorStyles.wordWrappedLabel);
                GUILayout.Label("   Fast Beyond 360: The rotation will go beyond 360°", EditorStyles.wordWrappedLabel);
            }
        }
    }
}