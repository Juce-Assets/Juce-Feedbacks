using System;

namespace Juce.Feedbacks
{
    public class FeedbackIdentifier : Attribute
    {
        public string Name { get; }
        public string Path { get; }

        public FeedbackIdentifier(string name, string path = "")
        {
            Name = name;
            Path = path;
        }
    }
}