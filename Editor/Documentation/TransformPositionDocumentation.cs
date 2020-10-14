using System;
using UnityEditor;
using Juce.Feedbacks;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class TransformPositionDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(TransformPositionFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Moves the target's position to the given value.", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: Transform component", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Coordinates Space: Local/World, mapping Transform.localPosition or Transform.position", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Use Starting Value: Enables the starting position", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- Start: (If enabled) Position that is going to be set at the moment the feedback starts", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- End: Position to reach at the end of the time", EditorStyles.wordWrappedLabel);
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
    }
}
