using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Anchor Min-Max", "RectTransform/")]
    public class RectTransformAnchorMinMaxFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private RectTransform target = default;

        [Header(FeedbackSectionsUtils.ValuesSection + " (Min)")]
        [SerializeField] private StartEndVector2Property minValue = default;


        [Header(FeedbackSectionsUtils.ValuesSection + " (Max)")]
        [SerializeField] private StartEndVector2Property maxValue = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public RectTransform Target { get => target; set => target = value; }
        public StartEndVector2Property MinValue => minValue;
        public StartEndVector2Property MaxValue => maxValue;
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
            InfoUtils.GetStartEndVector2PropertyInfo(ref infoList, minValue, "Min");
            InfoUtils.GetStartEndVector2PropertyInfo(ref infoList, maxValue, "Max");
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

            SequenceTween startSequence = new SequenceTween();

            if (minValue.UseStartValue)
            {
                if (minValue.UseStartX)
                {
                    startSequence.Join(target.TweenAnchorMinX(minValue.StartValueX, 0.0f));
                }

                if (minValue.UseStartY)
                {
                    startSequence.Join(target.TweenAnchorMinY(minValue.StartValueY, 0.0f));
                }
            }

            if (maxValue.UseStartValue)
            {
                if (maxValue.UseStartX)
                {
                    startSequence.Join(target.TweenAnchorMaxX(maxValue.StartValueX, 0.0f));
                }

                if (maxValue.UseStartY)
                {
                    startSequence.Join(target.TweenAnchorMaxY(maxValue.StartValueY, 0.0f));
                }
            }

            if(minValue.UseStartValue || maxValue.UseStartValue)
            {
                sequenceTween.Append(startSequence);
            }

            SequenceTween endSequence = new SequenceTween();

            if (minValue.UseEndX)
            {
                endSequence.Join(target.TweenAnchorMinX(minValue.EndValueX, duration));
            }

            if (minValue.UseEndY)
            {
                endSequence.Join(target.TweenAnchorMinY(minValue.EndValueY, duration));
            }

            if (maxValue.UseEndX)
            {
                endSequence.Join(target.TweenAnchorMaxX(maxValue.EndValueX, duration));
            }

            if (maxValue.UseEndY)
            {
                endSequence.Join(target.TweenAnchorMaxY(maxValue.EndValueY, duration));
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