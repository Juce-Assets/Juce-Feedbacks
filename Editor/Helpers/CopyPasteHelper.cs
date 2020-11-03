using Juce.Utils.Singletons;
using System.Collections.Generic;

namespace Juce.Feedbacks
{
    internal class CopyPasteHelper : Singleton<CopyPasteHelper>
    {
        private Feedback clipboardFeedback;
        private List<Feedback> clipboardAllFeedbacks = new List<Feedback>();

        public bool CanPasteValues(Feedback destination)
        {
            if (clipboardFeedback == null)
            {
                return false;
            }

            if (clipboardFeedback.GetType() != destination.GetType())
            {
                return false;
            }

            return true;
        }

        public bool CanPasteAsNew()
        {
            return clipboardFeedback != null;
        }

        public bool CanPasteAll(FeedbacksPlayerCE destination)
        {
            if (clipboardAllFeedbacks.Count <= 0)
            {
                return false;
            }

            for (int i = 0; i < clipboardAllFeedbacks.Count; ++i)
            {
                Feedback currClipboardFeedback = clipboardAllFeedbacks[i];

                if (currClipboardFeedback == null)
                {
                    continue;
                }

                if (currClipboardFeedback.gameObject == destination.CustomTarget.gameObject)
                {
                    return false;
                }
            }

            return true;
        }

        public void CopyFeedback(Feedback clipboardFeedback)
        {
            this.clipboardFeedback = clipboardFeedback;
        }

        public void CopyAllFeedbacks(IReadOnlyList<Feedback> clipboardAllFeedbacks)
        {
            this.clipboardAllFeedbacks.Clear();
            this.clipboardAllFeedbacks.AddRange(clipboardAllFeedbacks);
        }

        public void PasteFeedbackValues(FeedbacksPlayerCE feedbackPlayer, Feedback destination)
        {
            if (clipboardFeedback == null || destination == null)
            {
                return;
            }

            feedbackPlayer.PasteFeedbackValues(clipboardFeedback, destination);
        }

        public void PasteFeedbackAsNew(FeedbacksPlayerCE feedbackPlayer, int positionIndex)
        {
            if (clipboardFeedback == null)
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

            UndoHelper.Instance.BeginUndo("PasteAll");

            feedbackPlayer.RemoveAllFeedbacks();

            for (int i = 0; i < clipboardAllFeedbacks.Count; ++i)
            {
                feedbackPlayer.PasteFeedbackAsNew(clipboardAllFeedbacks[i]);
            }

            UndoHelper.Instance.EndUndo();
        }
    }
}