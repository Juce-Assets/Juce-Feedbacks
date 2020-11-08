using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Trigger", "Event/")]
    public class EventTriggerFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private string eventTrigger = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public string EventTrigger { get => eventTrigger; set => eventTrigger = value; }
        public float Delay { get => delay; set => delay = Mathf.Max(0, value); }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            infoList.Add($"Event: {eventTrigger}");
            InfoUtils.GetTimingInfo(ref infoList, delay);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if (string.IsNullOrEmpty(eventTrigger))
            {
                return null;
            }

            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            ExecuteResult result = new ExecuteResult();

            sequenceTween.AppendCallback(() =>
            {
                context.TriggerEvent(eventTrigger);
            });

            result.DelayTween = delayTween;

            return result;
        }
    }
}