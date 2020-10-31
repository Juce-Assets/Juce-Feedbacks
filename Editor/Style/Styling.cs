using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class Styling
    {
        public static readonly GUIStyle SmallTickbox = new GUIStyle("ShurikenToggle");

        static readonly Color splitterDark = new Color(0.12f, 0.12f, 0.12f, 1.333f);
        static readonly Color splitterLight = new Color(0.6f, 0.6f, 0.6f, 1.333f);
        public static Color Splitter { get { return EditorGUIUtility.isProSkin ? splitterDark : splitterLight; } }

        static readonly Color headerBackgroundDark = new Color(0.1f, 0.1f, 0.1f, 0.2f);
        static readonly Color headerBackgroundLight = new Color(1f, 1f, 1f, 0.4f);
        public static Color HeaderBackground { get { return EditorGUIUtility.isProSkin ? headerBackgroundDark : headerBackgroundLight; } }

        static readonly Color reorderRectDark = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        static readonly Color reorderRectLight = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        public static Color ReorderRect { get { return EditorGUIUtility.isProSkin ? reorderRectDark : reorderRectLight; } }

        static readonly Color reorderDark = new Color(1f, 1f, 1f, 0.2f);
        static readonly Color reorderLight = new Color(0.1f, 0.1f, 0.1f, 0.2f);
        public static Color Reorder { get { return EditorGUIUtility.isProSkin ? reorderDark : reorderLight; } }

        public static Color ProgressComplete { get; } = new Color(0.3f, 0.9f, 0.5f);

        public static void DrawSplitter(float height = 1.0f, float leftOffset = 0.0f, float rightOffset = 0.0f)
        {
            Rect rect = GUILayoutUtility.GetRect(1f, height);

            rect.x += leftOffset;
            rect.width += (-leftOffset) + rightOffset;

            EditorGUI.DrawRect(rect, Splitter);
        }

        public static Rect DrawHeader(ref bool expanded, ref bool activeField, string title, Color color, Action showGenericMenu)
        {
            Event e = Event.current;

            Rect backgroundRect = GUILayoutUtility.GetRect(4f, 17f);

            float offset = 24f;

            Rect reorderRect = backgroundRect;
            reorderRect.xMin = offset;
            reorderRect.y += 5f;
            reorderRect.width = 9f;
            reorderRect.height = 9f;

            Rect labelRect = backgroundRect;
            labelRect.xMin = offset + 48f;
            labelRect.xMax -= 20f;

            Rect foldoutRect = backgroundRect;
            foldoutRect.y += 2f;
            foldoutRect.xMin = offset + 13f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            Rect toggleRect = backgroundRect;
            toggleRect.x += 16f;
            toggleRect.xMin = offset + 29f;
            toggleRect.y += 2f;
            toggleRect.width = 13f;
            toggleRect.height = 13f;

            Rect menuRect = new Rect(labelRect.xMax + 4f, labelRect.y - 5f, 16, 20);

            // Background rect should be full-width
            backgroundRect.xMin -= 3f;
            backgroundRect.yMin -= 2f;
            backgroundRect.width += 3f;
            backgroundRect.height += 2f;

            EditorGUI.DrawRect(backgroundRect, color);

            // Foldout
            expanded = GUI.Toggle(foldoutRect, expanded, GUIContent.none, EditorStyles.foldout);

            // Title
            using (new EditorGUI.DisabledScope(!activeField))
            {
                EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);
            }

            // Active checkbox
            activeField = GUI.Toggle(toggleRect, activeField, GUIContent.none, SmallTickbox);

            for (int i = 0; i < 3; i++)
            {
                Rect r = reorderRect;
                r.height = 1;
                r.y = reorderRect.y + reorderRect.height * (i / 3.0f);
                EditorGUI.DrawRect(r, Reorder);
            }

            EditorGUI.LabelField(menuRect, "...", EditorStyles.boldLabel);

            // Handle events
            if (e.type == EventType.MouseDown)
            {
                if (menuRect.Contains(e.mousePosition))
                {
                    showGenericMenu?.Invoke();
                    e.Use();
                }
            }

            if (e.type == EventType.MouseDown && labelRect.Contains(e.mousePosition) && e.button == 0)
            {
                expanded = !expanded;
                e.Use();
            }

            return backgroundRect;
        }
    }
}