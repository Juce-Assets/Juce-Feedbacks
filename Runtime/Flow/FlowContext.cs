using Juce.Tween;

namespace Juce.Feedbacks
{
    public class FlowContext
    {
        public SequenceTween MainSequence { get; } = new SequenceTween();
        public SequenceTween CurrentSequence { get; set; } = new SequenceTween();
    }
}