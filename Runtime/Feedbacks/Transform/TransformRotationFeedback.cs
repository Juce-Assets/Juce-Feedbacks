using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Rotation", "Transform/")]
    public class TransformRotationFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private Transform target = default;

        [SerializeField] private RotationMode rotationMode = default;

        [SerializeField] [HideInInspector] private CoordinatesSpaceElement coordinatesSpace = default;
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
            AddElement<CoordinatesSpaceElement>(0, "Space");
            AddElement<Vector3Element>(1, "Values");
            AddElement<TimingElement>(2, "Timing");
            AddElement<LoopElement>(3, "Loop");
            AddElement<EasingElement>(4, "Easing");
        }

        protected override void OnLink()
        {
            coordinatesSpace = GetElement<CoordinatesSpaceElement>(0);
            value = GetElement<Vector3Element>(1);
            timing = GetElement<TimingElement>(2);
            loop = GetElement<LoopElement>(3);
            easing = GetElement<EasingElement>(4);
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

                switch (coordinatesSpace.CoordinatesSpace)
                {
                    case CoordinatesSpace.Local:
                        {
                            if (value.UseStartX)
                            {
                                startSequence.Join(target.TweenLocalRotationX(value.StartValueX, 0.0f, rotationMode));
                            }

                            if (value.UseStartY)
                            {
                                startSequence.Join(target.TweenLocalRotationY(value.StartValueY, 0.0f, rotationMode));
                            }

                            if (value.UseStartZ)
                            {
                                startSequence.Join(target.TweenLocalRotationZ(value.StartValueZ, 0.0f, rotationMode));
                            }
                        }
                        break;

                    case CoordinatesSpace.World:
                        {
                            if (value.UseStartX)
                            {
                                startSequence.Join(target.TweenRotationX(value.StartValueX, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startSequence.Join(target.TweenRotationY(value.StartValueY, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startSequence.Join(target.TweenRotationZ(value.StartValueZ, 0.0f));
                            }
                        }
                        break;
                }

                sequenceTween.Append(startSequence);
            }

            SequenceTween endSequence = new SequenceTween();

            switch (coordinatesSpace.CoordinatesSpace)
            {
                case CoordinatesSpace.Local:
                    {
                        if (value.UseEndX)
                        {
                            endSequence.Join(target.TweenLocalRotationX(value.EndValueX, timing.Duration, rotationMode));
                        }

                        if (value.UseEndY)
                        {
                            endSequence.Join(target.TweenLocalRotationY(value.EndValueY, timing.Duration, rotationMode));
                        }

                        if (value.UseEndZ)
                        {
                            endSequence.Join(target.TweenLocalRotationZ(value.EndValueZ, timing.Duration, rotationMode));
                        }
                    }
                    break;

                case CoordinatesSpace.World:
                    {
                        if (value.UseEndX)
                        {
                            endSequence.Join(target.TweenRotationX(value.EndValueX, timing.Duration));
                        }

                        if (value.UseEndY)
                        {
                            endSequence.Join(target.TweenRotationY(value.EndValueY, timing.Duration));
                        }

                        if (value.UseEndZ)
                        {
                            endSequence.Join(target.TweenRotationZ(value.EndValueZ, timing.Duration));
                        }
                    }
                    break;
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
