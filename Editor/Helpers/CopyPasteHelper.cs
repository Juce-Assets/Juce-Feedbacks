using System;
using Juce.Utils.Singletons;

namespace Juce.Feedbacks
{
    internal class CopyPasteHelper : Singleton<CopyPasteHelper>
    {
        private Feedback clipboardFeedback;

        public void CopyFeedback(Feedback clipboardFeedback)
        {
            this.clipboardFeedback = clipboardFeedback;
        }

        public void PasteFeedback(FeedbacksPlayerCE feedbackPlayer)
        {
            if(clipboardFeedback == null)
            {
                return;
            }

            feedbackPlayer.PasteFeedback(clipboardFeedback);
        }
    }
}
