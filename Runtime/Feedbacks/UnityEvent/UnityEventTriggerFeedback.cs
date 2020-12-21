using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class UnityEventTriggerFeedbackEvent : UnityEvent { }

    [FeedbackIdentifier("Trigger", "Untiy Event/")]
    public class UnityEventTriggerFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private UnityEventTriggerFeedbackEvent eventTrigger = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public UnityEventTriggerFeedbackEvent EventTrigger { get => eventTrigger; set => eventTrigger = value; }
        public float Delay { get => delay; set => delay = Mathf.Max(0, value); }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            if (eventTrigger != null)
            {
                for (int i = 0; i < eventTrigger.GetPersistentEventCount(); ++i)
                {
                    infoList.Add($"{i + 1}: {eventTrigger.GetPersistentTarget(i)} [{eventTrigger.GetPersistentMethodName(i)}]");
                }
            }

            InfoUtils.GetTimingInfo(ref infoList, delay);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if (eventTrigger == null)
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
                eventTrigger?.Invoke();
            });

            result.DelayTween = delayTween;

            return result;
        }
    }
}