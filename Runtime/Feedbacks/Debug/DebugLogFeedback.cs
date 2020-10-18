using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Log", "Debug/")]
    [FeedbackColor(0.5f, 0.4f, 0.1f)]
    public class DebugLogFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private string log = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public override string GetFeedbackInfo()
        {
            if(string.IsNullOrEmpty(log))
            {
                return string.Empty;
            }

            return $"Log: { log }";
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() => Debug.Log(log));

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}
