using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("New Sequence", "Flow/")]
    public class NewSequenceFlowFeedback : Feedback
    {
        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween(); 
        }
    }
}
