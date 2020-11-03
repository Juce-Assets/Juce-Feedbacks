using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Reach Transform Position", "Transform/")]
    public class TransformReachTransformPositionFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private Transform target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private CoordinatesSpace coordinatesSpace = default;

        [SerializeField] private StartEndTransformVector3Property value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public Transform Target { get => target; set => target = value; }
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

                Vector3 startLocalValueToReach = target.transform.localPosition + (value.StartValue.position - target.transform.position);

                switch (coordinatesSpace)
                {
                    case CoordinatesSpace.Local:
                        {
                            if (value.UseStartX)
                            {
                                startSequence.Join(target.TweenLocalPositionX(startLocalValueToReach.x, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startSequence.Join(target.TweenLocalPositionY(startLocalValueToReach.y, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startSequence.Join(target.TweenLocalPositionZ(startLocalValueToReach.z, 0.0f));
                            }
                        }
                        break;

                    case CoordinatesSpace.World:
                        {
                            if (value.UseStartX)
                            {
                                startSequence.Join(target.TweenPositionX(value.StartValue.position.x, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startSequence.Join(target.TweenPositionY(value.StartValue.position.y, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startSequence.Join(target.TweenPositionZ(value.StartValue.position.z, 0.0f));
                            }
                        }
                        break;
                }

                sequenceTween.Append(startSequence);
            }

            if (value.EndValue == null)
            {
                return null;
            }

            SequenceTween endSequence = new SequenceTween();

            Vector3 endLocalValueToReach = target.transform.localPosition + (value.EndValue.position - target.transform.position);

            switch (coordinatesSpace)
            {
                case CoordinatesSpace.Local:
                    {
                        if (value.UseEndX)
                        {
                            endSequence.Join(target.TweenLocalPositionX(endLocalValueToReach.x, duration));
                        }

                        if (value.UseEndY)
                        {
                            endSequence.Join(target.TweenLocalPositionY(endLocalValueToReach.y, duration));
                        }

                        if (value.UseEndZ)
                        {
                            endSequence.Join(target.TweenLocalPositionZ(endLocalValueToReach.z, duration));
                        }
                    }
                    break;

                case CoordinatesSpace.World:
                    {
                        if (value.UseEndX)
                        {
                            endSequence.Join(target.TweenPositionX(value.EndValue.position.x, duration));
                        }

                        if (value.UseEndY)
                        {
                            endSequence.Join(target.TweenPositionY(value.EndValue.position.y, duration));
                        }

                        if (value.UseEndZ)
                        {
                            endSequence.Join(target.TweenPositionZ(value.EndValue.position.z, duration));
                        }
                    }
                    break;
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