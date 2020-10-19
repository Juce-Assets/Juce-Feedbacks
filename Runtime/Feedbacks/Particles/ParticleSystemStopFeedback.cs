using System;
using UnityEngine;
using Juce.Tween;
using System.Collections.Generic;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Stop", "ParticleSystem/")]
    public class ParticleSystemStopFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private ParticleSystem target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private bool withChildren = true;
        [SerializeField] private ParticleSystemStopBehavior stopBehavior = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;
        [SerializeField] [Min(0)] private float duration = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target == null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            errors = "";
            return false;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.gameObject.name : string.Empty;
        }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            InfoUtils.GetTimingInfo(ref infoList, delay, duration);
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
                target.Stop(withChildren, stopBehavior);
            });

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}