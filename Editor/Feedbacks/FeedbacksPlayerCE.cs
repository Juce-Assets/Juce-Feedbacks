using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(FeedbacksPlayer))]
    public class FeedbacksPlayerCE : Editor
    {
        private FeedbacksPlayer CustomTarget => (FeedbacksPlayer)target;

        private readonly List<FeedbackTypeEditorData> cachedfeedbackTypes = new List<FeedbackTypeEditorData>();
        private readonly Dictionary<Feedback, FeedbackEditorData> cachedEditorFeedback = new Dictionary<Feedback, FeedbackEditorData>();

        private readonly DragHelper dragHelper = new DragHelper();

        private List<string> feedbacksInfo = new List<string>();

        private SerializedProperty feedbacksProperty;

        private void OnEnable()
        {
            GatherProperties();

            CacheFeedbackTypes();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            base.DrawDefaultInspector();

            DrawAllFeedbacks();

            DrawBottomInspector();

            DrawFeedbackPlayerControls();

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget.gameObject);
            }

            if (Application.isPlaying)
            {
                Repaint();
            }
        }

        private void GatherProperties()
        {
            feedbacksProperty = serializedObject.FindProperty("feedbacks");
        }

        public Feedback AddFeedback(Type type)
        {
            FeedbackTypeEditorData feedbackTypeEditorData = GetFeedbackEditorDataByType(type);

            return AddFeedback(feedbackTypeEditorData);
        }

        public Feedback AddFeedback(FeedbackTypeEditorData feedbackTypeEditorData)
        {
            Feedback newFeedback = ScriptableObject.CreateInstance(feedbackTypeEditorData.Type) as Feedback;

            if (newFeedback == null)
            {
                Debug.LogError($"Could not create {nameof(Feedback)} instance, " +
                    $"{nameof(feedbackTypeEditorData.Type)} does not inherit from {nameof(Feedback)}");
            }

            Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(AddFeedback)}");

            int index = feedbacksProperty.arraySize;

            feedbacksProperty.arraySize++;
            feedbacksProperty.GetArrayElementAtIndex(index).objectReferenceValue = newFeedback;

            feedbacksProperty.serializedObject.ApplyModifiedProperties();

            return newFeedback;
        }

        public void PasteFeedbackAsNew(Feedback feedback)
        {
            PasteFeedbackAsNew(feedback, feedbacksProperty.arraySize);
        }

        public void PasteFeedbackAsNew(Feedback feedback, int index)
        {
            Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(PasteFeedbackAsNew)}");

            Feedback feedbackCopy = Instantiate(feedback) as Feedback;

            feedbacksProperty.InsertArrayElementAtIndex(index);
            feedbacksProperty.GetArrayElementAtIndex(index).objectReferenceValue = feedbackCopy;

            feedbacksProperty.serializedObject.ApplyModifiedProperties();
        }

        public void RemoveFeedback(Feedback feedback)
        {
            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                if(feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue == feedback)
                {
                    Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(RemoveFeedback)}");

                    feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue = null;
                    feedbacksProperty.DeleteArrayElementAtIndex(i);

                    feedbacksProperty.serializedObject.ApplyModifiedProperties();

                    break;
                }
            }
        }

        public void RemoveAllFeedbacks()
        {
            Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(RemoveAllFeedbacks)}");

            feedbacksProperty.ClearArray();

            feedbacksProperty.serializedObject.ApplyModifiedProperties();
        }

        private void ReorderFeedback(int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                return;
            }

            Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(ReorderFeedback)}");

            Feedback feedbackToReorder = feedbacksProperty.GetArrayElementAtIndex(startIndex).objectReferenceValue as Feedback;

            feedbacksProperty.GetArrayElementAtIndex(startIndex).objectReferenceValue = null;
            feedbacksProperty.DeleteArrayElementAtIndex(startIndex);

            feedbacksProperty.InsertArrayElementAtIndex(endIndex);
            feedbacksProperty.GetArrayElementAtIndex(endIndex).objectReferenceValue = feedbackToReorder;

            feedbacksProperty.serializedObject.ApplyModifiedProperties();
        }

        public int GetFeedbackIndex(Feedback feedback)
        {
            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                if (feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue == feedback)
                {
                    return i;
                }
            }

            return 0;
        }

        private FeedbackEditorData GetOrCreateFeedbackeEditor(Feedback feedback)
        {
            cachedEditorFeedback.TryGetValue(feedback, out FeedbackEditorData feedbackEditorData);

            if(feedbackEditorData != null)
            {
                return feedbackEditorData;
            }

            FeedbackTypeEditorData feedbackTypeEditorData = GetFeedbackEditorDataByType(feedback.GetType());
            Editor feedbackEditor = Editor.CreateEditor(feedback);

            FeedbackEditorData newFeedbackEditorData = new FeedbackEditorData(feedback, feedbackTypeEditorData, feedbackEditor);

            cachedEditorFeedback.Add(feedback, newFeedbackEditorData);

            return newFeedbackEditorData;
        }

        private void FeedbacksSetExpanded(bool set)
        {
            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                Feedback currFeedback = feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue as Feedback;

                currFeedback.Expanded = set;
            }
        }

        private void CacheFeedbackTypes()
        {
            cachedfeedbackTypes.Clear();

            foreach (Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types = assembly.GetTypes();

                for (int i = 0; i < types.Length; ++i)
                {
                    Type currType = types[i];

                    if (currType.IsSubclassOf(typeof(Feedback)))
                    {
                        FeedbackIdentifier identifier = currType.GetCustomAttribute(typeof(FeedbackIdentifier)) as FeedbackIdentifier;

                        if (identifier == null)
                        {
                            continue;
                        }

                        FeedbackColor colorAttribute = currType.GetCustomAttribute(typeof(FeedbackColor)) as FeedbackColor;
                        Color color = colorAttribute != null ? colorAttribute.Color : new Color(0.0f, 0.0f, 0.0f, 0.0f);

                        string fullName = $"{identifier.Path.Replace("/", " ")}{identifier.Name}";

                        FeedbackTypeEditorData data = new FeedbackTypeEditorData(currType, identifier.Name, identifier.Path, fullName, color);

                        cachedfeedbackTypes.Add(data);
                    }
                }
            }
        }

        private FeedbackTypeEditorData GetFeedbackEditorDataByType(Type type)
        {
            for (int i = 0; i < cachedfeedbackTypes.Count; ++i)
            {
                FeedbackTypeEditorData currFeedbackEditorData = cachedfeedbackTypes[i];

                if (currFeedbackEditorData.Type == type)
                {
                    return currFeedbackEditorData;
                }
            }

            return null;
        }

        private void DrawProgress(Feedback feedback)
        {
            Rect progressRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            progressRect.x -= 3;
            progressRect.width += 6;
            progressRect.height = 2.0f;

            if (feedback.ExecuteResult != null)
            {
                if(feedback.ExecuteResult.DelayTween != null && feedback.ExecuteResult.DelayTween.IsPlaying)
                {
                    progressRect.width *= feedback.ExecuteResult.DelayTween.GetProgress();
                    EditorGUI.DrawRect(progressRect, Color.gray);
                }
                else if (feedback.ExecuteResult.ProgresTween != null)
                {
                    if (feedback.ExecuteResult.ProgresTween.IsCompleted)
                    {
                        EditorGUI.DrawRect(progressRect, Color.green);
                    }
                    else
                    {
                        progressRect.width *= feedback.ExecuteResult.ProgresTween.GetProgress();
                        EditorGUI.DrawRect(progressRect, Color.white);
                    }
                }
                else
                {
                    EditorGUI.DrawRect(progressRect, new Color(0.0f, 0.0f, 0.0f, 0.0f));
                }
            }
            else
            {
                EditorGUI.DrawRect(progressRect, new Color(0.0f, 0.0f, 0.0f, 0.0f));
            }
        }

        private void DrawAllFeedbacks()
        {
            Event e = Event.current;

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Feedbacks", EditorStyles.boldLabel);

            for(int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                Feedback currFeedback = feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue as Feedback;

                FeedbackEditorData feedbackEditorData = GetOrCreateFeedbackeEditor(currFeedback);

                DrawFeedback(feedbackEditorData, i, e);
            }

            // Finish dragging
            int startIndex;
            int endIndex;
            bool dragged = dragHelper.ResolveDragging(e, out startIndex, out endIndex);

            if (dragged)
            {
                ReorderFeedback(startIndex, endIndex);
            }
        }

        private void DrawFeedback(FeedbackEditorData feedbackEditorData, int index, Event e)
        {
            Feedback feedback = feedbackEditorData.Feedback;
            FeedbackTypeEditorData feedbackTypeEditorData = feedbackEditorData.FeedbackTypeEditorData;

            bool expanded = feedback.Expanded;
            bool enabled = feedback.Enabled;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                string name = feedbackTypeEditorData.FullName;

                string targetInfo = feedback.GetFeedbackTargetInfo();

                if (!string.IsNullOrEmpty(targetInfo))
                {
                    name += $" [{targetInfo}]";
                }

                Rect headerRect = Styling.DrawHeader(ref expanded, ref enabled, name, feedbackTypeEditorData.Color,
                    () => ShowFeedbackContextMenu(feedback));

                dragHelper.CheckDraggingItem(e, headerRect, Styling.ReorderRect, index);

                feedback.Expanded = expanded;
                feedback.Enabled = enabled;

                string errors;
                bool hasErrors = feedback.GetFeedbackErrors(out errors);

                if (hasErrors || !string.IsNullOrEmpty(feedback.UserData))
                {
                    EditorGUILayout.Space(2);
                }

                if (hasErrors)
                {
                    GUIStyle s = new GUIStyle(EditorStyles.label);
                    s.normal.textColor = new Color(0.8f, 0.2f, 0.2f);

                    EditorGUILayout.LabelField($"Warning: {errors}", s);
                }

                if (!string.IsNullOrEmpty(feedback.UserData))
                {
                    GUILayout.Label($"{feedback.UserData}", EditorStyles.wordWrappedLabel);
                }

                if (!expanded)
                {
                    feedbacksInfo.Clear();
                    feedback.GetFeedbackInfo(ref feedbacksInfo);

                    string infoString = InfoUtils.FormatInfo(ref feedbacksInfo);

                    if (!string.IsNullOrEmpty(infoString))
                    {
                        EditorGUILayout.LabelField(infoString);
                    }
                }

                if (expanded)
                {
                    EditorGUILayout.Space(2);
                }

                DrawProgress(feedback);

                if (expanded)
                {
                    EditorGUILayout.Space(2);

                    Styling.DrawSplitter(1, -4, 4);

                    EditorGUILayout.Space(5);

                    feedbackEditorData.Editor.OnInspectorGUI();
                }
            }
        }

        private void DrawBottomInspector()
        {
            EditorGUILayout.Space(4);

            Styling.DrawSplitter(2.0f);

            EditorGUILayout.Space(2);

            if (GUILayout.Button("Add feedback"))
            {
                ShowFeedbacksMenu();
            }

            EditorGUILayout.Space(4);

            Styling.DrawSplitter(1.0f);

            EditorGUILayout.Space(4);

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Copy All"))
                {
                    CopyPasteHelper.Instance.CopyAllFeedbacks(CustomTarget.Feedbacks);
                }

                EditorGUI.BeginDisabledGroup(!CopyPasteHelper.Instance.CanPasteAll);
                {
                    if (GUILayout.Button("Paste All"))
                    {
                        CopyPasteHelper.Instance.PasteAllFeedbacks(this);
                    }
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void ShowFeedbacksMenu()
        {
            GenericMenu menu = new GenericMenu();

            for (int i = 0; i < cachedfeedbackTypes.Count; ++i)
            {
                FeedbackTypeEditorData currFeedbackEditorData = cachedfeedbackTypes[i];

                menu.AddItem(new GUIContent($"{currFeedbackEditorData.Path}{currFeedbackEditorData.Name}"),
                    false, OnFeedbacksMenuPressed, currFeedbackEditorData);
            }

            menu.ShowAsContext();
        }

        private void OnFeedbacksMenuPressed(object userData)
        {
            FeedbackTypeEditorData feedbackEditorData = userData as FeedbackTypeEditorData;

            if (feedbackEditorData == null)
            {
                return;
            }

            AddFeedback(feedbackEditorData);
        }

        private void ShowFeedbackContextMenu(Feedback feedback)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Remove"), false, () => RemoveFeedback(feedback));

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Copy"), false, () => CopyPasteHelper.Instance.CopyFeedback(feedback));

            if (CopyPasteHelper.Instance.CanPaste)
            {
                menu.AddItem(new GUIContent("Paste As New"), false, () =>
                {
                    int feedbackIndex = GetFeedbackIndex(feedback);

                    CopyPasteHelper.Instance.PasteFeedbackAsNew(this, feedbackIndex + 1);
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste As New"), false);
            }
            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Expand All"), false, () => FeedbacksSetExpanded(true));
            menu.AddItem(new GUIContent("Collapse All"), false, () => FeedbacksSetExpanded(false));

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Documentation"), false, () => ShowFeedbackDocumentation(feedback));


            menu.ShowAsContext();
        }

        private void ShowFeedbackDocumentation(Feedback feedback)
        {
            FeedbackTypeEditorData feedbackTypeEditorData = GetFeedbackEditorDataByType(feedback.GetType());

            FeedbackDocumentationWindow window = EditorWindow.GetWindow<FeedbackDocumentationWindow>("Feedbacks documentation");

            window.Init(feedbackTypeEditorData);

            window.Show();
        }

        private void DrawFeedbackPlayerControls()
        {
            EditorGUILayout.Space(8);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Play"))
                        {
                            CustomTarget.Play();
                        }

                        if (GUILayout.Button("Complete"))
                        {
                            CustomTarget.Complete();
                        }

                        if (GUILayout.Button("Kill"))
                        {
                            CustomTarget.Kill();
                        }

                        if (GUILayout.Button("Restart"))
                        {
                            CustomTarget.Restart();
                        }
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }
    }
}
