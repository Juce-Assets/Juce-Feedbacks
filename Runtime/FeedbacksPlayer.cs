using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class FeedbacksPlayer : MonoBehaviour
    {
        [SerializeField] private bool executeOnAwake = default;

        private readonly List<Feedback> feedbacks = new List<Feedback>();

        private void Awake()
        {
            FindFeedbacks();
        }

        private void Start()
        {
            TryExecuteOnAwake();
        }

        private void FindFeedbacks()
        {
            gameObject.GetComponents<Feedback>(feedbacks);
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

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                Tween.SequenceTween sequenceTween = new Tween.SequenceTween();

                currFeedback.OnExectue(sequenceTween);

                mainSequenceTween.Join(sequenceTween);
            }

            mainSequenceTween.Play();
        }
    }
}
