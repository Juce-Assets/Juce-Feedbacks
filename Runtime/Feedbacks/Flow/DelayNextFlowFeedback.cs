using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Delay Next", "Flow/")]
    [FeedbackColor(0.0f, 0.4f, 0.5f)]
    public class DelayNextFlowFeedback : Feedback
    {
        [SerializeField] [Min(0.0f)] private float delay = default;

        public override string GetFeedbackInfo()
        {
            return $"Delay: {delay}";
        }

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            context.CurrentDelay += delay;
        }
    }
}
