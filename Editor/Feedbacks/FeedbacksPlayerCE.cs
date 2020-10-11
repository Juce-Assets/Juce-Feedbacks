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

        private readonly List<FeedbackTypeEditorData> feedbackTypes = new List<FeedbackTypeEditorData>();
        private readonly List<FeedbackEditorData> cachedEditorFeedback = new List<FeedbackEditorData>();

        private readonly DragHelper dragHelper = new DragHelper();

        private SerializedProperty feedbacksProperty;

        private SerializedProperty loopModeProperty;
        private SerializedProperty loopResetModeProperty;
        private SerializedProperty loopsProperty;

        private void OnEnable()
        {
            GatherProperties();

            GatherFeedbackTypes();

            ChacheAllFeedbacksEditor();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            base.DrawDefaultInspector();

            DrawLoopProperties();

            DrawFeedbacksEditors();

            DrawAddFeedbackInspector();

            DrawFeedbackPlayerControls();

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget.gameObject);
            }
        }

        private void GatherProperties()
        {
            feedbacksProperty = serializedObject.FindProperty("feedbacks");

            loopModeProperty = serializedObject.FindProperty("loopMode");
            loopResetModeProperty = serializedObject.FindProperty("loopResetMode");
            loopsProperty = serializedObject.FindProperty("loops");
        }

        private Feedback AddFeedback(Type type)
        {
            FeedbackTypeEditorData editorData = GetFeedbackEditorDataByType(type);

            return AddFeedback(editorData);
        }

        private Feedback AddFeedback(FeedbackTypeEditorData feedbackTypeEditorData)
        {
            Feedback newFeedback = ScriptableObject.CreateInstance(feedbackTypeEditorData.Type) as Feedback;

            if (newFeedback == null)
            {
                Debug.LogError($"Could not create {nameof(Feedback)} instance, {nameof(feedbackTypeEditorData.Type)} does not inherit from {nameof(Feedback)}");
            }

            CacheFeedbackEditor(newFeedback);

            CustomTarget.AddFeedback(newFeedback);

            newFeedback.Init();

            return newFeedback;
        }

        private void RemoveFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new Exception();
            }

            RemoveCacheFeedbackEditor(feedback);

            CustomTarget.RemoveFeedback(feedback);
        }

        private void ReorderFeedback(int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                return;
            }

            FeedbackEditorData item = cachedEditorFeedback[startIndex];
            cachedEditorFeedback.RemoveAt(startIndex);
            cachedEditorFeedback.Insert(endIndex, item);

            CustomTarget.ReorderFeedback(startIndex, endIndex);
        }

        private void ChacheAllFeedbacksEditor()
        {
            for (int i = 0; i < feedbacksProperty.arraySize; ++i)
            {
                Feedback currFeedback = (Feedback)feedbacksProperty.GetArrayElementAtIndex(i).objectReferenceValue;

                CacheFeedbackEditor(currFeedback);
            }
        }

        private void CacheFeedbackEditor(Feedback feedback)
        {
            RemoveCacheFeedbackEditor(feedback);

            if (feedback == null)
            {
                return;
            }

            FeedbackTypeEditorData feedbackTypeEditorData = GetFeedbackEditorDataByType(feedback.GetType());

            Editor currEditor = Editor.CreateEditor(feedback);

            FeedbackEditorData feedbackEditorData = new FeedbackEditorData(feedback, feedbackTypeEditorData, currEditor);

            cachedEditorFeedback.Add(feedbackEditorData);
        }

        private void RemoveCacheFeedbackEditor(Feedback feedback)
        {
            for (int i = 0; i < cachedEditorFeedback.Count; ++i)
            {
                if (cachedEditorFeedback[i].Feedback == feedback)
                {
                    cachedEditorFeedback.RemoveAt(i);

                    break;
                }
            }
        }

        private void GatherFeedbackTypes()
        {
            feedbackTypes.Clear();

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

                        FeedbackDescription description = currType.GetCustomAttribute(typeof(FeedbackDescription)) as FeedbackDescription;

                        FeedbackTypeEditorData data;

                        if (description != null)
                        {
                            data = new FeedbackTypeEditorData(currType, identifier.Name, identifier.Path, description.Description);
                        }
                        else
                        {
                            data = new FeedbackTypeEditorData(currType, identifier.Name, identifier.Path);
                        }

                        feedbackTypes.Add(data);
                    }
                }
            }
        }

        private FeedbackTypeEditorData GetFeedbackEditorDataByType(Type type)
        {
            for (int i = 0; i < feedbackTypes.Count; ++i)
            {
                FeedbackTypeEditorData currFeedbackEditorData = feedbackTypes[i];

                if (currFeedbackEditorData.Type == type)
                {
                    return currFeedbackEditorData;
                }
            }

            return null;
        }

        private void DrawLoopProperties()
        {
            EditorGUILayout.PropertyField(loopModeProperty);

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes || (LoopMode)loopModeProperty.enumValueIndex == LoopMode.UntilManuallyStoped)
            {
                EditorGUILayout.PropertyField(loopResetModeProperty);
            }

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes)
            {
                EditorGUILayout.PropertyField(loopsProperty);
            }
        }

        private void DrawProgress(Tween.Tween tween)
        {
            if (tween != null && tween.IsPlaying)
            {
                Rect progressRect = GUILayoutUtility.GetRect(4f, 2f);
                progressRect.width *= tween.GetProgress();
                EditorGUI.DrawRect(progressRect, Color.white);

                Repaint();
            }
        }

        private void DrawFeedbacksEditors()
        {
            Event e = Event.current;

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Feedbacks", EditorStyles.boldLabel);

            for (int i = 0; i < cachedEditorFeedback.Count; ++i)
            {
                FeedbackEditorData currFeedback = cachedEditorFeedback[i];

                FeedbackTypeEditorData feedbackTypeEditorData = currFeedback.FeedbackTypeEditorData;

                bool expanded = currFeedback.Feedback.Expanded;
                bool enabled = currFeedback.Feedback.Enabled;

                if (!string.IsNullOrEmpty(feedbackTypeEditorData.Description))
                {
                    EditorGUILayout.HelpBox(feedbackTypeEditorData.Description, MessageType.None);

                    EditorGUILayout.Space(2);
                }

                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    string name = $"{feedbackTypeEditorData.Path.Replace("/", " ")}{feedbackTypeEditorData.Name}";

                    string targetInfo = currFeedback.Feedback.GetFeedbackTargetInfo();

                    if (!string.IsNullOrEmpty(targetInfo))
                    {
                        name += $" [{targetInfo}]";
                    }

                    Rect headerRect = Styling.DrawHeader(ref expanded, ref enabled, name, () => ShowFeedbackContextMenu(currFeedback.Feedback));

                    dragHelper.CheckDraggingItem(e, headerRect, Styling.ReorderRect, i);

                    currFeedback.Feedback.Expanded = expanded;
                    currFeedback.Feedback.Enabled = enabled;

                    string errors;
                    bool hasErrors = currFeedback.Feedback.GetFeedbackErrors(out errors);

                    if (hasErrors)
                    {
                        GUIStyle s = new GUIStyle(EditorStyles.label);
                        s.normal.textColor = Color.red;

                        EditorGUILayout.LabelField($"Warning: {errors}", s);
                    }

                    if (!string.IsNullOrEmpty(currFeedback.Feedback.UserData))
                    {
                        EditorGUILayout.LabelField($"{currFeedback.Feedback.UserData}");
                    }

                    if (!expanded)
                    {
                        DrawProgress(currFeedback.Feedback.FeedbackSequence);

                        string feedbackInfoString = currFeedback.Feedback.GetFeedbackInfo();

                        if (!string.IsNullOrEmpty(feedbackInfoString))
                        {
                            EditorGUILayout.LabelField(feedbackInfoString);
                        }
                    }
                    else
                    {
                        EditorGUILayout.Space(2);

                        Styling.DrawSplitter(1, -4, 4);

                        currFeedback.Editor.OnInspectorGUI();

                        foreach (Element element in currFeedback.Feedback.Elements)
                        {
                            EditorGUILayout.Space(2);

                            Editor elementEditor = Editor.CreateEditor(element);

                            EditorGUILayout.LabelField(element.ElementName, EditorStyles.boldLabel);

                            elementEditor.OnInspectorGUI();

                            DestroyImmediate(elementEditor);
                        }
                    }
                }

                // Check if we start dragging this feedback
                //draggingHelper.CheckDraggingItem(e, headerRect, FeedbacksPlayerStyling.ReorderRect, i);
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

        private void DrawAddFeedbackInspector()
        {
            EditorGUILayout.Space(8);

            Styling.DrawSplitter(2.0f);

            EditorGUILayout.Space(2);

            if (GUILayout.Button("Add feedback"))
            {
                ShowFeedbacksMenu();
            }
        }

        private void ShowFeedbacksMenu()
        {
            GenericMenu menu = new GenericMenu();

            for (int i = 0; i < feedbackTypes.Count; ++i)
            {
                FeedbackTypeEditorData currFeedbackEditorData = feedbackTypes[i];

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

            menu.ShowAsContext();
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
