using Juce.Tween;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sequential Loop", "Flow/")]
    [FeedbackColor(0.0f, 0.4f, 0.5f)]
    public class LoopFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public LoopProperty Looping => looping;

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            LoopUtils.SetLoop(context.CurrentSequence, looping);

            context.MainSequence.Join(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween();

            return new ExecuteResult();
        }
    }
}
