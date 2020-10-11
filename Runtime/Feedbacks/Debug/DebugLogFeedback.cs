using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Log", "Debug/")]
    public class DebugLogFeedback : Feedback
    {
        [Header("Values")]
        [SerializeField] private string log = default;

        [SerializeField] [HideInInspector] private TimingElement timing = default;

        protected override void OnCreate()
        {
            timing = AddElement<TimingElement>("Timing");
            timing.UseDuration = false;
        }

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            sequenceTween.AppendWaitTime(context.CurrentDelay + timing.Delay);

            sequenceTween.AppendCallback(() => Debug.Log(log));
        }
    }
}
