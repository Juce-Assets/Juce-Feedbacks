using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("New Sequence", "Flow/")]
    [FeedbackColor(0.0f, 0.4f, 0.5f)]
    public class NewSequenceFeedback : Feedback
    {
        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween();

            return new ExecuteResult();
        }
    }
}
