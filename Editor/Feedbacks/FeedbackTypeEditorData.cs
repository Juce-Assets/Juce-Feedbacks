using System;

namespace Juce.Feedbacks
{
    public class FeedbackTypeEditorData
    {
        public Type Type { get; }
        public string Name { get; }
        public string Path { get; }
        public string Description { get; }

        public FeedbackTypeEditorData(Type type, string name, string path, string description)
        {
            Type = type;
            Name = name;
            Path = path;
            Description = description;
        }

        public FeedbackTypeEditorData(Type type, string name, string path)
        {
            Type = type;
            Name = name;
            Path = path;
        }
    }
}