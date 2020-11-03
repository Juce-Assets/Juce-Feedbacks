using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Wait", "Time/")]
    [FeedbackColor(0.1f, 0.5f, 0.3f)]
    public class WaitTimeFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float duration = 1.0f;

        public float Duration { get => duration; set => duration = Mathf.Max(0, value); }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            InfoUtils.GetTimingInfo(ref infoList, duration);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            Tween.Tween progressTween = new WaitTimeTween(duration);
            sequenceTween.Append(progressTween);

            ExecuteResult result = new ExecuteResult();
            result.ProgresTween = progressTween;

            return result;
        }
    }
}