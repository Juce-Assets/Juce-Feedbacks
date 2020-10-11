using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sequential Wait", "Flow/")]
    public class SequentialWaitFlowFeedback : Feedback
    {
        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween(); 
        }
    }
}
