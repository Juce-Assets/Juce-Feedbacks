using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Start Loop", "Flow/")]
    [FeedbackColor(0.5f, 0.3f, 0.1f)]
    public class StartLoopFeedback : Feedback
    {
        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            context.MainSequence.Join(context.CurrentSequence);

            context.CurrentSequence = new SequenceTween();

            return new ExecuteResult();
        }
    }
}