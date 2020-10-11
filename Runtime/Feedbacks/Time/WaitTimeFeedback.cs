using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Wait", "Time/")]
    public class WaitTimeFeedback : Feedback
    {
        [Header("Values")]
        [SerializeField] private float time = default;

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            sequenceTween.AppendWaitTime(time);
        }
    }
}
