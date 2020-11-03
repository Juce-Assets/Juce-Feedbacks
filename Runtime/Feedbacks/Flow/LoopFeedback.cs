using Juce.Tween;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Loop", "Flow/")]
    [FeedbackColor(0.5f, 0.3f, 0.1f)]
    public class LoopFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public LoopProperty Looping => looping;

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            LoopUtils.SetLoop(context.CurrentSequence, looping);

            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween();

            return new ExecuteResult();
        }
    }
}