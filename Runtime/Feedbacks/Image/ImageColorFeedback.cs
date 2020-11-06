using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Color", "Image/")]
    public class ImageColorFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private Image target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private StartEndColorProperty value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public Image Target { get => target; set => target = value; }
        public StartEndColorProperty Value => value;
        public float Delay { get => delay; set => delay = Mathf.Max(0, value); }
        public float Duration { get => duration; set => duration = Mathf.Max(0, value); }
        public EasingProperty Easing => easing;
        public LoopProperty Looping => looping;

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
            InfoUtils.GetTimingInfo(ref infoList, delay, duration);
            InfoUtils.GetStartEndColorPropertyInfo(ref infoList, value);
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

            if (value.UseStartValue)
            {
                SequenceTween startSequence = new SequenceTween();

                if (value.UseStartColor)
                {
                    startSequence.Join(target.TweenColorNoAlpha(value.StartColor, 0.0f));
                }

                if (value.UseStartAlpha)
                {
                    sequenceTween.Join(target.TweenColorAlpha(value.StartAlpha, 0.0f));
                }

                sequenceTween.Append(startSequence);
            }

            SequenceTween endSequence = new SequenceTween();

            if (value.UseEndColor)
            {
                endSequence.Join(target.TweenColorNoAlpha(value.EndColor, duration));
            }

            if (value.UseEndAlpha)
            {
                endSequence.Join(target.TweenColorAlpha(value.EndAlpha, duration));
            }

            Tween.Tween progressTween = endSequence;

            sequenceTween.Append(endSequence);

            EasingUtils.SetEasing(sequenceTween, easing);
            LoopUtils.SetLoop(sequenceTween, looping);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}