using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class MonoBehaviourDestroyDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(MonoBehaviourDestroyFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Destroys the target MonoBehaviour", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: MonoBehaviour that is going to be destroyed", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}