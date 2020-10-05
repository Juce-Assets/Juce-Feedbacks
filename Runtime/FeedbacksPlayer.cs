using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class FeedbacksPlayer : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private List<Feedback> feedbacks = new List<Feedback>();

        [SerializeField] private bool executeOnAwake = default;
        [SerializeField] [Min(0)] private float delay = default;

        private void Start()
        {
            TryExecuteOnAwake();
        }

        public void AddFeedback(Feedback feedback)
        {
            feedbacks.Add(feedback);
        }

        public void RemoveFeedback(Feedback feedback)
        {
            feedbacks.Remove(feedback);
        }

        private void TryExecuteOnAwake()
        {
            if(!executeOnAwake)
            {
                return;
            }

            Play();
        }

        public void Play()
        {
            Tween.SequenceTween mainSequenceTween = new Tween.SequenceTween();

            if(delay > 0)
            {
                mainSequenceTween.AppendWaitTime(delay);
            }

            Tween.SequenceTween feedbacksSequenceTween = new Tween.SequenceTween();

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if(!currFeedback.Enabled)
                {
                    continue;
                }

                Tween.SequenceTween sequenceTween = new Tween.SequenceTween();

                if (currFeedback.Delay > 0)
                {
                    sequenceTween.AppendWaitTime(currFeedback.Delay);
                }

                currFeedback.OnExectue(sequenceTween);

                feedbacksSequenceTween.Join(sequenceTween);
            }

            mainSequenceTween.Append(feedbacksSequenceTween);

            mainSequenceTween.Play();
        }
    }
}
