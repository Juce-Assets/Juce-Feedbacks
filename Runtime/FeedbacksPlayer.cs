using System;
using System.Collections.Generic;
using UnityEngine;
using Juce.Tween;
using UnityEngine.UIElements;

namespace Juce.Feedbacks
{
    public class FeedbacksPlayer : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private List<Feedback> feedbacks = new List<Feedback>();

        [SerializeField] private bool executeOnAwake = default;
        [SerializeField] [Min(0)] private float delay = default;

        [SerializeField] [HideInInspector] private LoopMode loopMode = LoopMode.Disabled;
        [SerializeField] [HideInInspector] private ResetMode loopResetMode = ResetMode.Restart;
        [SerializeField] [HideInInspector] [Min(0)] private int loops = default;

        private SequenceTween currMainSequence;

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

        public void ReorderFeedback(int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                return;
            }

            Feedback item = feedbacks[startIndex];

            feedbacks.RemoveAt(startIndex);

            feedbacks.Insert(endIndex, item);
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
            Kill();

            FlowContext context = new FlowContext();

            if(delay > 0)
            {
                context.MainSequence.AppendWaitTime(delay);
            }

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if(!currFeedback.Enabled)
                {
                    continue;
                }

                Tween.SequenceTween sequenceTween = new Tween.SequenceTween();

                ExecuteResult executeResult = currFeedback.OnExecute(context, sequenceTween);

                if(executeResult == null)
                {
                    continue;
                }

                context.CurrentSequence.Join(sequenceTween);

                currFeedback.ExecuteResult = executeResult;
            }

            context.MainSequence.Append(context.CurrentSequence);

            switch (loopMode)
            {
                case LoopMode.XTimes:
                    {
                        context.MainSequence.SetLoops(loops, loopResetMode);
                    }
                    break;

                case LoopMode.UntilManuallyStoped:
                    {
                        context.MainSequence.SetLoops(int.MaxValue, loopResetMode);
                    }
                    break;
            }

            context.MainSequence.Play();

            currMainSequence = context.MainSequence;
        }

        public void Complete()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Complete();
        }

        public void Kill()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Kill();
        }

        public void Restart()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Restart();
        }
    }
}
