using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class FeedbackDocumentationWindow : EditorWindow
    {
        private Vector2 scrollPos;

        private readonly List<IFeedbackDocumentation> documentations = new List<IFeedbackDocumentation>();

        private FeedbackTypeEditorData feedbackTypeEditorData;
        private IFeedbackDocumentation currentDocumentation;

        private void OnEnable()
        {
            GatherDocumentations();
        }

        private void OnGUI()
        {
            if (currentDocumentation == null)
            {
                return;
            }

            EditorGUILayout.Space(2);

            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
            style.fontSize = 16;

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            {
                EditorGUILayout.LabelField($"{feedbackTypeEditorData.Path}{feedbackTypeEditorData.Name}", style);

                EditorGUILayout.Space(2);

                currentDocumentation.DrawDocumentation();
            }
            EditorGUILayout.EndScrollView();
        }

        public void Init(FeedbackTypeEditorData feedbackTypeEditorData)
        {
            this.feedbackTypeEditorData = feedbackTypeEditorData;

            currentDocumentation = GetDocumentation(feedbackTypeEditorData.Type);
        }

        private void GatherDocumentations()
        {
            documentations.Clear();

            foreach (Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types = assembly.GetTypes();

                for (int i = 0; i < types.Length; ++i)
                {
                    Type currType = types[i];

                    if (currType.GetInterfaces().Contains(typeof(IFeedbackDocumentation)))
                    {
                        documentations.Add(Activator.CreateInstance(currType) as IFeedbackDocumentation);
                    }
                }
            }
        }

        public IFeedbackDocumentation GetDocumentation(Type type)
        {
            foreach (IFeedbackDocumentation documentation in documentations)
            {
                if (documentation.FeedbackType == type)
                {
                    return documentation;
                }
            }

            return null;
        }
    }
}