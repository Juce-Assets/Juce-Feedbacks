using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("End Loop", "Flow/")]
    public class EndLoopFlowFeedback : Feedback
    {
        [SerializeField] [Min(0.0f)] private int loops = default;

        public override string GetFeedbackInfo()
        {
            return $"Loops: {loops}";
        }

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            //context.CurrentLoop.SetLoops(loops, ResetMode.Restart);

            //context.MainSequence.Append(context.CurrentLoop);

            //context.CurrentLoop = new SequenceTween();
        }
    }
}
