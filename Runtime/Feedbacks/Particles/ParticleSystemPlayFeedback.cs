using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Play", "ParticleSystem/")]
    public class ParticleSystemPlayFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private ParticleSystem target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private bool withChildren = true;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target == null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            errors = "";

            return true;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.gameObject.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            string info = "";

            return info;
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
                target.Play(withChildren);
            });

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}