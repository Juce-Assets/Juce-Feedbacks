using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class TransformRotationDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TransformRotationFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Rotates the target to the given value.", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Transform component", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Coordinates Space: Local/World, mapping Transform.localRotation or Transform.rotation", EditorStyles.wordWrappedLabel);
                RotationModeDocumentation();
                GUILayout.Label("- Use Starting Value: Enables the starting rotation", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (If enabled) Rotation that is going to be set at the moment the feedback starts", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: Rotation to reach at the end of the time", EditorStyles.wordWrappedLabel);
                GenericsDocumentation.DelayDocumentation();
                GenericsDocumentation.DurationDocumentation();
                GenericsDocumentation.LoopDocumentation();
                GenericsDocumentation.EasingDocumentation();
            }

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Sequencing:");
                GenericsDocumentation.SameTimeSequencingDocumentation();
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
