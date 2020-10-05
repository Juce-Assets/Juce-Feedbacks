using System;

namespace Juce.Feedbacks
{
    public class FeedbackDescription : Attribute
    {
        public string Description { get; }

        public FeedbackDescription(string description)
        {
            Description = description;
        }
    }
}