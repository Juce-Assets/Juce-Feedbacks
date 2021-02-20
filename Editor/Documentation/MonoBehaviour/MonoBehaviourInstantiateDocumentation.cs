using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class MonoBehaviourInstantiateDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(MonoBehaviourInstantiateFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Instantiates the target MonoBehaviour", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Target: MonoBehaviour that is going to be instantiated", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("- Parent: Parent transform to set to the instantiated MonoBehaviour. Can be left to null", EditorStyles.wordWrappedLabel);
                GUILayout.Label("- World Position Stays: When you assign a parent MonoBehaviour, true will position the " +
                    "new object directly in world space. False will set the MonoBehaviour’s position relative to its new parent", EditorStyles.wordWrappedLabel);
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}