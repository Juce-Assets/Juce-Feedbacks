using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sequential Wait", "Flow/")]
    [FeedbackColor(0.0f, 0.4f, 0.5f)]
    public class SequentialWaitFlowFeedback : Feedback
    {
        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween(); 
        }
    }
}
