using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class TransformScaleDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TransformScaleFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Scales the target to the given value.", EditorStyles.wordWrappedLabel);
            GUILayout.Label("You can only change the localScale of a Transform", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Transform component", EditorStyles.wordWrappedLabel);
                RotationModeDocumentation();
                GUILayout.Label("- Use Starting Value: Enables the starting scale", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (If enabled) Scale that is going to be set at the moment the feedback starts", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: Scale to reach at the end of the time", EditorStyles.wordWrappedLabel);
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
