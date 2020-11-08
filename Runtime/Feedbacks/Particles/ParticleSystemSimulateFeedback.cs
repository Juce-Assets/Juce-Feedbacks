using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Simulate", "ParticleSystem/")]
    public class ParticleSystemSimulateFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private ParticleSystem target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private float time = default;

        [SerializeField] private bool withChildren = true;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public ParticleSystem Target { get => target; set => target = value; }
        public bool WithChildren { get => withChildren; set => withChildren = value; }
        public float Delay { get => delay; set => delay = Mathf.Max(0, value); }

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target == null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            errors = string.Empty;
            return false;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.gameObject.name : string.Empty;
        }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            infoList.Add($"Simulate: {time}s");
            InfoUtils.GetTimingInfo(ref infoList, delay);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if (target == null)
            {
                return null;
            }

            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() =>
            {
                target.Simulate(time, withChildren);
            });

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}