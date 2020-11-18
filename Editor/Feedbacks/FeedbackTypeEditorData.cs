using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class FeedbackTypeEditorData
    {
        public Type Type { get; }
        public string Name { get; }
        public string Path { get; }
        public string FullName { get; }
        public Color Color { get; }

        public FeedbackTypeEditorData(Type type, string name, string path, string fullName, Color color)
        {
            Type = type;
            Name = name;
            Path = path;
            FullName = fullName;
            Color = color;
        }
    }
}