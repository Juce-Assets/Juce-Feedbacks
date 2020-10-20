using System;

namespace Juce.Feedbacks
{
    public interface IFeedbackDocumentation
    {
        Type FeedbackType { get; }
        void DrawDocumentation();
    }
}
