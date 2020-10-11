using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Delay Next", "Flow/")]
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
