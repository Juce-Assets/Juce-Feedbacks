using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Wait", "Time/")]
    [FeedbackColor(0.1f, 0.5f, 0.3f)]
    public class WaitTimeFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;
        [SerializeField] [Min(0)] private float duration = default;

        public override string GetFeedbackInfo()
        {
            if (duration <= 0.0f)
            {
                return string.Empty;
            }

            return $"Time: { duration }";
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            Tween.Tween progressTween = new WaitTimeTween(duration);
            sequenceTween.Append(progressTween);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}
