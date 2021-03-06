﻿using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "Transform/")]
    public class TransformPositionFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private Transform target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private CoordinatesSpace coordinatesSpace = default;

        [SerializeField] private StartEndVector3Property value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public Transform Target { get => target; set => target = value; }
        public CoordinatesSpace CoordinatesSpace { get => coordinatesSpace; set => coordinatesSpace = value; }
        public StartEndVector3Property Value => value;
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
            InfoUtils.GetStartEndVector3PropertyInfo(ref infoList, value);
            InfoUtils.GetCoordinatesSpaceInfo(ref infoList, coordinatesSpace);
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

                switch (coordinatesSpace)
                {
                    case CoordinatesSpace.Local:
                        {
                            if (value.UseStartX)
                            {
                                startSequence.Join(target.TweenLocalPositionX(value.StartValueX, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startSequence.Join(target.TweenLocalPositionY(value.StartValueY, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startSequence.Join(target.TweenLocalPositionZ(value.StartValueZ, 0.0f));
                            }
                        }
                        break;

                    case CoordinatesSpace.World:
                        {
                            if (value.UseStartX)
                            {
                                startSequence.Join(target.TweenPositionX(value.StartValueX, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startSequence.Join(target.TweenPositionY(value.StartValueY, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startSequence.Join(target.TweenPositionZ(value.StartValueZ, 0.0f));
                            }
                        }
                        break;
                }

                sequenceTween.Append(startSequence);
            }

            SequenceTween endSequence = new SequenceTween();

            switch (coordinatesSpace)
            {
                case CoordinatesSpace.Local:
                    {
                        if (value.UseEndX)
                        {
                            endSequence.Join(target.TweenLocalPositionX(value.EndValueX, duration));
                        }

                        if (value.UseEndY)
                        {
                            endSequence.Join(target.TweenLocalPositionY(value.EndValueY, duration));
                        }

                        if (value.UseEndZ)
                        {
                            endSequence.Join(target.TweenLocalPositionZ(value.EndValueZ, duration));
                        }
                    }
                    break;

                case CoordinatesSpace.World:
                    {
                        if (value.UseEndX)
                        {
                            endSequence.Join(target.TweenPositionX(value.EndValueX, duration));
                        }

                        if (value.UseEndY)
                        {
                            endSequence.Join(target.TweenPositionY(value.EndValueY, duration));
                        }

                        if (value.UseEndZ)
                        {
                            endSequence.Join(target.TweenPositionZ(value.EndValueZ, duration));
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