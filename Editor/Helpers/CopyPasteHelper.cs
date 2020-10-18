using System;
using Juce.Utils.Singletons;

namespace Juce.Feedbacks
{
    internal class CopyPasteHelper : Singleton<CopyPasteHelper>
    {
        private Feedback clipboardFeedback;

        public bool CanPaste => clipboardFeedback != null;

        public void CopyFeedback(Feedback clipboardFeedback)
        {
            this.clipboardFeedback = clipboardFeedback;
        }

        public void PasteFeedbackAsNew(FeedbacksPlayerCE feedbackPlayer)
        {
            if(clipboardFeedback == null)
            {
                return;
            }

            feedbackPlayer.PasteFeedbackAsNew(clipboardFeedback);
        }
    }
}
