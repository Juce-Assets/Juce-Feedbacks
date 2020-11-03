using UnityEditor;

namespace Juce.Feedbacks
{
    public class FeedbackEditorData
    {
        public Feedback Feedback { get; }
        public FeedbackTypeEditorData FeedbackTypeEditorData { get; }
        public Editor Editor { get; }

        public FeedbackEditorData(Feedback feedback, FeedbackTypeEditorData feedbackTypeEditorData, Editor editor)
        {
            Feedback = feedback;
            FeedbackTypeEditorData = feedbackTypeEditorData;
            Editor = editor;
        }
    }
}