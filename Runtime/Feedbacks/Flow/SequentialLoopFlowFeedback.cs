using Juce.Tween;
using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sequential Loop", "Flow/")]
    public class SequentialLoopFlowFeedback : Feedback
    {
        [SerializeField] [HideInInspector] private LoopElement loop = default;

        public override string GetFeedbackInfo()
        {
            return $"Loops: {loop.Loops}";
        }

        protected override void OnCreate()
        {
            loop = AddElement<LoopElement>("Value");
        }

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            loop.SetLoop(context.CurrentSequence);

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
        }
    }
}
