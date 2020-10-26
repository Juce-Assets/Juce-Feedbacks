using System;
using System.Collections.Generic;
using Juce.Utils.Singletons;

namespace Juce.Feedbacks
{
    internal class CopyPasteHelper : Singleton<CopyPasteHelper>
    {
        private Feedback clipboardFeedback;
        private List<Feedback> clipboardAllFeedbacks = new List<Feedback>();

        public bool CanPaste => clipboardFeedback != null;

        public void CopyFeedback(Feedback clipboardFeedback)
        {
            this.clipboardFeedback = clipboardFeedback;
        }

        public void CopyAllFeedbacks(IReadOnlyList<Feedback> clipboardAllFeedbacks)
        {
            this.clipboardAllFeedbacks.Clear();
            this.clipboardAllFeedbacks.AddRange(clipboardAllFeedbacks);
        }

        public void PasteFeedbackAsNew(FeedbacksPlayerCE feedbackPlayer, int positionIndex)
        {
            if(clipboardFeedback == null)
            {
                return;
            }

            feedbackPlayer.PasteFeedbackAsNew(clipboardFeedback, positionIndex);
        }

        public void PasteAllFeedbacks(FeedbacksPlayerCE feedbackPlayer)
        {
            if (clipboardAllFeedbacks == null)
            {
                return;
            }

            feedbackPlayer.RemoveAllFeedbacks();

            for (int i = 0; i < clipboardAllFeedbacks.Count; ++i)
            {
                feedbackPlayer.PasteFeedbackAsNew(clipboardAllFeedbacks[i]);
            }
        }
    }
}
