using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("SizeDelta", "RectTransform/")]
    public class RectTransformSizeDeltaFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private RectTransform target = default;

        [SerializeField] [HideInInspector] private Vector2Element value = default;
        [SerializeField] [HideInInspector] private TimingElement timing = default;
        [SerializeField] [HideInInspector] private LoopElement loop = default;
        [SerializeField] [HideInInspector] private EasingElement easing = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target != null)
            {
                errors = "";
                return false;
            }

            errors = "Target is null";

            return true;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.gameObject.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            string info = $"{timing.Duration}s";

            if (value.UseStartValue)
            {
                info += $" | Start: x:{value.StartValueX} y:{value.StartValueY}";
            }

            info += $" | End: x:{value.EndValueX} y:{value.EndValueY}";

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

        protected override void OnCreate()
        {
            AddElement<Vector2Element>(0, "Values");
            AddElement<TimingElement>(1, "Timing");
            AddElement<LoopElement>(2, "Loop");
            AddElement<EasingElement>(3, "Easing");
        }

        protected override void OnLink()
        {
            value = GetElement<Vector2Element>(0);
            timing = GetElement<TimingElement>(1);
            loop = GetElement<LoopElement>(2);
            easing = GetElement<EasingElement>(3);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            Tween.Tween delayTween = null;

            if (timing.Delay > 0)
            {
                delayTween = new WaitTimeTween(timing.Delay);
                sequenceTween.Append(delayTween);
            }

            if (value.UseStartValue)
            {
                SequenceTween startSequence = new SequenceTween();

                if (value.UseStartX)
                {
                    startSequence.Join(target.TweenSizeDeltaX(value.StartValueX, 0.0f));
                }

                if (value.UseStartY)
                {
                    startSequence.Join(target.TweenSizeDeltaY(value.StartValueY, 0.0f));
                }

                sequenceTween.Append(startSequence);
            }

            SequenceTween endSequence = new SequenceTween();

            if (value.UseEndX)
            {
                endSequence.Join(target.TweenSizeDeltaX(value.EndValueX, timing.Duration));
            }

            if (value.UseEndY)
            {
                endSequence.Join(target.TweenSizeDeltaY(value.EndValueY, timing.Duration));
            }

            Tween.Tween progressTween = endSequence;

            easing.SetEasing(endSequence);

            sequenceTween.Append(endSequence);

            loop.SetLoop(sequenceTween);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}
