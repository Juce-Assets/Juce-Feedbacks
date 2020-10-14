using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Scale", "Transform/")]
    public class TransformScaleFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private Transform target = default;

        [SerializeField] [HideInInspector] private Vector3Element value = default;
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
                info += $" | Start: x:{value.StartValueX} y:{value.StartValueY} z: {value.StartValueZ}";
            }

            info += $" | End: x:{value.EndValueX} y:{value.EndValueY} z: {value.EndValueZ}";

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
            value = AddElement<Vector3Element>("Values");
            timing = AddElement<TimingElement>("Timing");
            loop = AddElement<LoopElement>("Loop");
            easing = AddElement<EasingElement>("Easing");
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
                    startSequence.Join(target.TweenLocalScaleX(value.StartValueX, 0.0f));
                }

                if (value.UseStartY)
                {
                    startSequence.Join(target.TweenLocalScaleY(value.StartValueY, 0.0f));
                }

                if (value.UseStartZ)
                {
                    startSequence.Join(target.TweenLocalScaleZ(value.StartValueZ, 0.0f));
                }

                sequenceTween.Append(startSequence);
            }

            SequenceTween endSequence = new SequenceTween();

            if (value.UseEndX)
            {
                endSequence.Join(target.TweenLocalScaleX(value.EndValueX, timing.Duration));
            }

            if (value.UseEndY)
            {
                endSequence.Join(target.TweenLocalScaleY(value.EndValueY, timing.Duration));
            }

            if (value.UseEndZ)
            {
                endSequence.Join(target.TweenLocalScaleZ(value.EndValueZ, timing.Duration));
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
