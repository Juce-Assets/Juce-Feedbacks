using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class FeedbackTypeEditorData
    {
        public Type Type { get; }
        public string Name { get; }
        public string Path { get; }
        public string Description { get; }
        public Color Color { get; }

        public FeedbackTypeEditorData(Type type, string name, string path, string description, Color color)
        {
            Type = type;
            Name = name;
            Path = path;
            Description = description;
            Color = color;
        }
    }
}