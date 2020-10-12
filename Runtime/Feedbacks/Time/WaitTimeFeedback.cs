using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Wait", "Time/")]
    [FeedbackColor(0.1f, 0.5f, 0.3f)]
    public class WaitTimeFeedback : Feedback
    {
        [Header("Values")]
        [SerializeField] private float time = default;

        public override string GetFeedbackInfo()
        {
            if (time <= 0.0f)
            {
                return string.Empty;
            }

            return $"Time: { time }";
        }

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            sequenceTween.AppendWaitTime(time);
        }
    }
}
