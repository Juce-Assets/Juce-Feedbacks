using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sequential Loop Start", "Flow/")]
    public class SequentialLoopStartFlowFeedback : Feedback
    {
        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            if(context.HasLoopStart)
            {
                return;
            }

            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween();

            context.HasLoopStart = true;
        }
    }
}
