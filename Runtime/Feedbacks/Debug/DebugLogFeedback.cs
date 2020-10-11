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

        public override void OnExectue(SequenceTween sequenceTween)
        {
            sequenceTween.AppendCallback(() => Debug.Log(log));
        }
    }
}
