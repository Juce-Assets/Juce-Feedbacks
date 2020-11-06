using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class GameObjectInstantiateDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(GameObjectInstantiateFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Instantiates the target GameObject", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: GameObject that is going to be instantiated", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Parent: Parent transform to set to the instantiated GameObject. Can be left to null", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- World Position Stays: When you assign a parent GameObject, true will position the " +
                    "new object directly in world space. False will set the GameObject’s position relative to its new parent", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}