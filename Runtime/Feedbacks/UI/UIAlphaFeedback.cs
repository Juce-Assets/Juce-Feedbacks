using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Alpha", "UI/")]
    public class UIAlphaFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private GameObject target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private StartEndUnitFloatProperty value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;
        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty loop = default;

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
            return target != null ? target.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            string info = $"{duration}s";

            if (value.UseStartValue)
            {
                info += $" | Start: {value.StartValue} ";
            }

            info += $" | End: {value.EndValue} ";

            if (!easing.UseAnimationCurve)
            {
                info += $" | Ease: {easing.Easing}";
            }
            else
            {
                info += $" | Ease: Curve";
            }

            return info;
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            CanvasGroup canvasGroup = target.GetOrAddComponent<CanvasGroup>();

            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            if (value.UseStartValue)
            {
                sequenceTween.Append(canvasGroup.TweenAlpha(value.StartValue, 0.0f));
            }

            Tween.Tween progressTween = canvasGroup.TweenAlpha(value.EndValue, duration);
            sequenceTween.Append(progressTween);

            EasingUtils.SetEasing(sequenceTween, easing);
            LoopUtils.SetLoop(sequenceTween, loop);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}
