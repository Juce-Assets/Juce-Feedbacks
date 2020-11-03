using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Reach Transform Rotation", "Transform/")]
    public class TransformReachTransformRotationFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private Transform target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private RotationMode rotationMode = default;

        [SerializeField] private StartEndTransformVector3Property value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public Transform Target { get => target; set => target = value; }
        public RotationMode RotationMode { get => rotationMode; set => rotationMode = value; }
        public StartEndTransformVector3Property Value => value;
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

            if (value.UseStartValue && value.StartValue == null)
            {
                errors = $"Start value {nameof(Transform)} is null";
                return true;
            }

            if (value.EndValue == null)
            {
                errors = $"End value {nameof(Transform)} is null";
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
            InfoUtils.GetStartEndTransformPropertyInfo(ref infoList, value);
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
                if (value.StartValue == null)
                {
                    return null;
                }

                SequenceTween startSequence = new SequenceTween();

                if (value.UseStartX)
                {
                    startSequence.Join(target.TweenRotationX(value.StartValue.eulerAngles.x, 0.0f, rotationMode));
                }

                if (value.UseStartY)
                {
                    startSequence.Join(target.TweenRotationY(value.StartValue.eulerAngles.y, 0.0f, rotationMode));
                }

                if (value.UseStartZ)
                {
                    startSequence.Join(target.TweenRotationZ(value.StartValue.eulerAngles.z, 0.0f, rotationMode));
                }

                sequenceTween.Append(startSequence);
            }

            if (value.EndValue == null)
            {
                return null;
            }

            SequenceTween endSequence = new SequenceTween();

            if (value.UseEndX)
            {
                endSequence.Join(target.TweenRotationX(value.EndValue.eulerAngles.x, duration, rotationMode));
            }

            if (value.UseEndY)
            {
                endSequence.Join(target.TweenRotationY(value.EndValue.eulerAngles.y, duration, rotationMode));
            }

            if (value.UseEndZ)
            {
                endSequence.Join(target.TweenRotationZ(value.EndValue.eulerAngles.z, duration, rotationMode));
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