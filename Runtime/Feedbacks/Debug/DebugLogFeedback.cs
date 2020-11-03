using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

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

        public string Log { get => log; set => log = value; }
        public float Delay { get => delay; set => delay = Mathf.Max(0, delay); }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            InfoUtils.GetTimingInfo(ref infoList, delay);

            if (!string.IsNullOrEmpty(log))
            {
                infoList.Add(log);
            }
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