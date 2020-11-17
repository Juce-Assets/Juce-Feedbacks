using Juce.Utils;
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
        public FeedbacksPlayer CustomTarget => (FeedbacksPlayer)target;

        private readonly List<FeedbackTypeEditorData> cachedfeedbackTypes = new List<FeedbackTypeEditorData>();
        private readonly Dictionary<Feedback, FeedbackEditorData> cachedEditorFeedback = new Dictionary<Feedback, FeedbackEditorData>();

        private readonly List<Feedback> toRepare = new List<Feedback>();

        private readonly DragHelper dragHelper = new DragHelper();

        private List<string> feedbacksInfo = new List<string>();

        private SerializedProperty feedbacksProperty;

        private bool developerMode;

        private void OnEnable()
        {
            GatherProperties();

            CacheFeedbackTypes();

            TryRepareFeedbacks();

            SetStartingFeedbacksVisibility();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            base.DrawDefaultInspector();

            DrawFeedbacksSection();

            DrawAddAndCopyPasteSection();

            DrawControlsSection();

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

        private void DrawFeedbacksSection()
        {
            Event e = Event.current;

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Feedbacks", EditorStyles.boldLabel);

            TryRepareFeedbacks();

            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                Feedback currFeedback = feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue as Feedback;

                if (currFeedback == null)
                {
                    continue;
                }

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

        private void DrawAddAndCopyPasteSection()
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

                EditorGUI.BeginDisabledGroup(!CopyPasteHelper.Instance.CanPasteAll(this));
                {
                    if (GUILayout.Button("Paste All"))
                    {
                        UndoHelper.Instance.BeginUndo("PasteAll");
                        CopyPasteHelper.Instance.PasteAllFeedbacks(this);
                        UndoHelper.Instance.EndUndo();
                    }
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawControlsSection()
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

        private void DrawFeedback(FeedbackEditorData feedbackEditorData, int index, Event e)
        {
            Feedback feedback = feedbackEditorData.Feedback;

            if (feedback == null)
            {
                return;
            }

            FeedbackTypeEditorData feedbackTypeEditorData = feedbackEditorData.FeedbackTypeEditorData;

            bool expanded = feedback.Expanded;
            bool enabled = !feedback.Disabled;

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
                feedback.Disabled = !enabled;

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
                        EditorGUILayout.LabelField(infoString, EditorStyles.wordWrappedLabel);
                    }
                }

                if (expanded)
                {
                    EditorGUILayout.Space(2);
                }

                DrawFeedbackProgress(feedback);

                if (expanded)
                {
                    EditorGUILayout.Space(2);

                    Styling.DrawSplitter(1, -4, 4);

                    EditorGUILayout.Space(5);

                    feedbackEditorData.Editor.OnInspectorGUI();
                }

                if (!JuceConfiguration.Instance.DeveloperMode)
                {
                    feedback.hideFlags |= HideFlags.HideInInspector;
                }
                else
                {
                    feedback.hideFlags &= ~HideFlags.HideInInspector;
                }
            }
        }

        private void DrawFeedbackProgress(Feedback feedback)
        {
            Rect progressRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            progressRect.x -= 3;
            progressRect.width += 6;
            progressRect.height = 2.0f;

            if (feedback.ExecuteResult != null)
            {
                if (feedback.ExecuteResult.DelayTween != null && feedback.ExecuteResult.DelayTween.IsPlaying)
                {
                    progressRect.width *= feedback.ExecuteResult.DelayTween.GetProgress();
                    EditorGUI.DrawRect(progressRect, Color.gray);
                }
                else if (feedback.ExecuteResult.ProgresTween != null)
                {
                    if (feedback.ExecuteResult.ProgresTween.IsCompleted)
                    {
                        EditorGUI.DrawRect(progressRect, Styling.ProgressComplete);
                    }
                    else
                    {
                        progressRect.width *= feedback.ExecuteResult.ProgresTween.GetProgress();
                        EditorGUI.DrawRect(progressRect, Color.white);
                    }
                }
                else
                {
                    EditorGUI.DrawRect(progressRect, Color.clear);
                }
            }
            else
            {
                EditorGUI.DrawRect(progressRect, Color.clear);
            }
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

            menu.AddItem(new GUIContent("Remove"), false,
                () =>
                {
                    UndoHelper.Instance.BeginUndo("Remove");
                    RemoveFeedback(feedback);
                    UndoHelper.Instance.EndUndo();
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Copy"), false, () => CopyPasteHelper.Instance.CopyFeedback(feedback));

            if (CopyPasteHelper.Instance.CanPasteValues(feedback))
            {
                menu.AddItem(new GUIContent("Paste values"), false, () =>
                {
                    UndoHelper.Instance.BeginUndo("PasteValues");
                    CopyPasteHelper.Instance.PasteFeedbackValues(this, feedback);
                    UndoHelper.Instance.EndUndo();
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste values"), false);
            }

            if (CopyPasteHelper.Instance.CanPasteAsNew())
            {
                menu.AddItem(new GUIContent("Paste As New"), false, () =>
                {
                    int feedbackIndex = GetFeedbackIndex(feedback);

                    UndoHelper.Instance.BeginUndo("PasteAsNew");
                    CopyPasteHelper.Instance.PasteFeedbackAsNew(this, feedbackIndex + 1);
                    UndoHelper.Instance.EndUndo();
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

        public Feedback AddFeedback(Type type)
        {
            FeedbackTypeEditorData feedbackTypeEditorData = GetFeedbackEditorDataByType(type);

            return AddFeedback(feedbackTypeEditorData);
        }

        public Feedback AddFeedback(FeedbackTypeEditorData feedbackTypeEditorData)
        {
            if (feedbackTypeEditorData == null)
            {
                Debug.LogError($"{nameof(FeedbackTypeEditorData)} was null on {nameof(AddFeedback)} at {nameof(FeedbacksPlayerCE)}");
                return null;
            }

            Feedback newFeedback = CustomTarget.gameObject.AddComponent(feedbackTypeEditorData.Type) as Feedback;

            if (newFeedback == null)
            {
                Debug.LogError($"Could not create {nameof(Feedback)} instance, " +
                    $"{nameof(feedbackTypeEditorData.Type)} does not inherit from {nameof(Feedback)}");
            }

            Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(AddFeedback)}");
            Undo.RegisterCreatedObjectUndo(newFeedback, $"{nameof(AddFeedback)}");

            int index = feedbacksProperty.arraySize;

            feedbacksProperty.arraySize++;
            feedbacksProperty.GetArrayElementAtIndex(index).objectReferenceValue = newFeedback;

            feedbacksProperty.serializedObject.ApplyModifiedProperties();

            return newFeedback;
        }

        public void PasteFeedbackValues(Feedback origin, Feedback destination)
        {
            if (origin == null)
            {
                Debug.LogError($"Origin {nameof(Feedback)} was null on {nameof(PasteFeedbackValues)} at {nameof(FeedbacksPlayerCE)}");
                return;
            }

            if (destination == null)
            {
                Debug.LogError($"Destination {nameof(Feedback)} was null on {nameof(PasteFeedbackValues)} at {nameof(FeedbacksPlayerCE)}");
                return;
            }

            if (origin.GetType() != destination.GetType())
            {
                return;
            }

            EditorUtility.CopySerialized(origin, destination);
        }

        public void PasteFeedbackAsNew(Feedback feedback)
        {
            PasteFeedbackAsNew(feedback, feedbacksProperty.arraySize);
        }

        public void PasteFeedbackAsNew(Feedback origin, int index)
        {
            if (origin == null)
            {
                Debug.LogError($"Origin {nameof(Feedback)} was null on {nameof(PasteFeedbackAsNew)} at {nameof(FeedbacksPlayerCE)}");
                return;
            }

            Feedback feedbackCopy = CustomTarget.gameObject.AddComponent(origin.GetType()) as Feedback;

            Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(PasteFeedbackAsNew)}");
            Undo.RegisterCreatedObjectUndo(feedbackCopy, $"{nameof(PasteFeedbackAsNew)}");

            EditorUtility.CopySerialized(origin, feedbackCopy);

            feedbacksProperty.InsertArrayElementAtIndex(index);
            feedbacksProperty.GetArrayElementAtIndex(index).objectReferenceValue = feedbackCopy;

            feedbacksProperty.serializedObject.ApplyModifiedProperties();
        }

        public void RemoveFeedback(Feedback feedback, bool destroyComponent = true)
        {
            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                if (feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue == feedback)
                {
                    RemoveFeedbackEditor(feedback);

                    Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(RemoveFeedback)}");

                    if (feedback != null && destroyComponent)
                    {
                        Undo.DestroyObjectImmediate(feedback);
                    }

                    feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue = null;
                    feedbacksProperty.DeleteArrayElementAtIndex(i);

                    feedbacksProperty.serializedObject.ApplyModifiedProperties();

                    break;
                }
            }
        }

        public void RemoveAllFeedbacks()
        {
            cachedEditorFeedback.Clear();

            Undo.RegisterCompleteObjectUndo(CustomTarget, $"{nameof(RemoveAllFeedbacks)}");

            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                Undo.DestroyObjectImmediate(feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue);
            }

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

        private void FeedbacksSetExpanded(bool set)
        {
            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                Feedback currFeedback = feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue as Feedback;

                currFeedback.Expanded = set;
            }
        }

        private void TryRepareFeedbacks()
        {
            toRepare.Clear();

            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                toRepare.Add((Feedback)feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue);
            }

            for (int i = 0; i < toRepare.Count; ++i)
            {
                Feedback currFeedback = toRepare[i];

                if (currFeedback == null)
                {
                    Debug.LogError($"{nameof(Feedback)} is null and could not be repared");

                    continue;
                }

                bool needsToBeRecreated = currFeedback.gameObject == null;

                if (needsToBeRecreated)
                {
                    PasteFeedbackAsNew(currFeedback);
                    RemoveFeedback(currFeedback);

                    continue;
                }

                bool needsToBeReplaced = currFeedback.gameObject != CustomTarget.gameObject;

                if (needsToBeReplaced)
                {
                    PasteFeedbackAsNew(currFeedback);
                    RemoveFeedback(currFeedback, false);

                    continue;
                }
            }
        }

        private FeedbackEditorData GetOrCreateFeedbackeEditor(Feedback feedback)
        {
            cachedEditorFeedback.TryGetValue(feedback, out FeedbackEditorData feedbackEditorData);

            if (feedbackEditorData != null)
            {
                return feedbackEditorData;
            }

            FeedbackTypeEditorData feedbackTypeEditorData = GetFeedbackEditorDataByType(feedback.GetType());
            Editor feedbackEditor = Editor.CreateEditor(feedback);

            FeedbackEditorData newFeedbackEditorData = new FeedbackEditorData(feedback, feedbackTypeEditorData, feedbackEditor);

            cachedEditorFeedback.Add(feedback, newFeedbackEditorData);

            return newFeedbackEditorData;
        }

        private void RemoveFeedbackEditor(Feedback feedback)
        {
            if (feedback == null)
            {
                return;
            }

            cachedEditorFeedback.Remove(feedback);
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

        private void SetStartingFeedbacksVisibility()
        {
            Feedback[] feedbacks = CustomTarget.GetComponents<Feedback>();

            for (int i = 0; i < feedbacks.Length; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if (!JuceConfiguration.Instance.DeveloperMode)
                {
                    currFeedback.hideFlags |= HideFlags.HideInInspector;
                }
                else
                {
                    currFeedback.hideFlags &= ~HideFlags.HideInInspector;
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
    }
}