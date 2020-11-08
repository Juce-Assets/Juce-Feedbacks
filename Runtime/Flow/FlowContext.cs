using Juce.Tween;
using System;

namespace Juce.Feedbacks
{
    public class FlowContext
    {
        private readonly Action<string> triggerEvent;

        public FlowContext(Action<string> triggerEvent)
        {
            this.triggerEvent = triggerEvent;
        }

        public SequenceTween MainSequence { get; } = new SequenceTween();
        public SequenceTween CurrentSequence { get; set; } = new SequenceTween();

        public void TriggerEvent(string eventTrigger)
        {
            triggerEvent?.Invoke(eventTrigger);
        }
    }
}