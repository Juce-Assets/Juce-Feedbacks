using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal static class MenuItems
    {
        [MenuItem("Tools/Juce/Feedbacks/Create Feedbacks Player", false, 1)]
        private static void CreateFeedbackPlayer()
        {
            GameObject newFeedbackPlayer = new GameObject("FeedbacksPlayer");
            newFeedbackPlayer.AddComponent<FeedbacksPlayer>();
        }

        [MenuItem("Tools/Juce/Feedbacks/🗎 Documentation", false, 50)]
        private static void Documentation()
        {
            Application.OpenURL("https://github.com/Juce-Assets/Juce-Feedbacks/wiki");
        }

        [MenuItem("Tools/Juce/Feedbacks/🐞 Report Bug", false, 51)]
        private static void ReportBug()
        {
            Application.OpenURL("https://github.com/Juce-Assets/Juce-Feedbacks/issues/new?assignees=&labels=bug&template=bug_report.md");
        }

        [MenuItem("Tools/Juce/Feedbacks/🖋 Send Feedback", false, 52)]
        private static void SendFeedback()
        {
            Application.OpenURL("https://github.com/Juce-Assets/Juce-Feedbacks/issues/new?assignees=&labels=enhancement&template=feature_request.md");
        }

        [MenuItem("Tools/Juce/Feedbacks/📆 Changelog", false, 53)]
        private static void Changelog()
        {
            Application.OpenURL("https://github.com/Juce-Assets/Juce-Feedbacks/issues/new?assignees=&labels=enhancement&template=feature_request.md");
        }
    }
}