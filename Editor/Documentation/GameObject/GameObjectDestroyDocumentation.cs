using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class GameObjectDestroyDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(GameObjectDestroyFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Destroys the target GameObject", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: GameObject that is going to be destroyed", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}