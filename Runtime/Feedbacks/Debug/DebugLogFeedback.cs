using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Log", "Debug/")]
    [FeedbackColor(0.5f, 0.4f, 0.1f)]
    public class DebugLogFeedback : Feedback
    {
        [Header("Values")]
        [SerializeField] private string log = default;

        [SerializeField] [HideInInspector] private TimingElement timing = default;

        public override string GetFeedbackInfo()
        {
            if(string.IsNullOrEmpty(log))
            {
                return string.Empty;
            }

            return $"Log: { log }";
        }

        protected override void OnCreate()
        {
            timing = AddElement<TimingElement>("Timing");
            timing.UseDuration = false;
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            Tween.Tween delayTween = new WaitTimeTween(timing.Delay);
            sequenceTween.Append(delayTween);

            sequenceTween.AppendCallback(() => Debug.Log(log));

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}
