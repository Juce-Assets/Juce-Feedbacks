using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class StartLoopDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(StartLoopFeedback);

        public void DrawDocumentation()
        { 
            GUILayout.Label("Marks the start of a loop for LoopFeedback. It's not necessary for the LoopFeedback to work, " +
                "but it can be usefull in some situations", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.LoopDocumentation();
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
