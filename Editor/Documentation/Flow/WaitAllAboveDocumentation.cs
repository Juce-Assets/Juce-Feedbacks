using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class WaitAllAboveDocumentation : IFeedbackDocumentation
    {
        public Type FeedbackType => typeof(WaitAllAboveFeedback);

        public void DrawDocumentation()
        {
            GUILayout.Label("Normally, all feedbacks play at the same time. If you add this feedback, " +
                "it will wait for all the feedbacks above to finish, before starting with the " +
                "feedbacks that are under it", EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space(2);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GenericsDocumentation.DelayDocumentation();
            }
        }
    }
}
