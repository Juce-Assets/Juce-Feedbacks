using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sequential Loop", "Flow/")]
    [FeedbackColor(0.0f, 0.4f, 0.5f)]
    public class SequentialLoopFlowFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty loop = default;

        public override string GetFeedbackInfo()
        {
            return $"Loops: {loop.Loops}";
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            LoopUtils.SetLoop(context.CurrentSequence, loop);

            if (!context.HasLoopStart)
            {
                context.MainSequence.Append(context.CurrentSequence);
            }
            else
            {
                context.HasLoopStart = false;

                context.MainSequence.Join(context.CurrentSequence);
            }

            context.CurrentSequence = new SequenceTween();

            return new ExecuteResult();
        }
    }
}
