using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sequential Loop Start", "Flow/")]
    [FeedbackColor(0.0f, 0.4f, 0.5f)]
    public class SequentialLoopStartFlowFeedback : Feedback
    {
        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if(context.HasLoopStart)
            {
                return null;
            }

            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween();

            context.HasLoopStart = true;

            return new ExecuteResult();
        }
    }
}
