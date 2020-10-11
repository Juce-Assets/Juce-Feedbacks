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
            coordinatesSpace = AddElement<CoordinatesSpaceElement>("Space");
            value = AddElement<Vector3Element>("Values");
            timing = AddElement<TimingElement>("Timing");
            loop = AddElement<LoopElement>("Loop");
            easing = AddElement<EasingElement>("Easing");
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            sequenceTween.AppendWaitTime(timing.Delay);

            if (value.UseStartValue)
            {
                SequenceTween startSequence = new SequenceTween();

                switch (coordinatesSpace.CoordinatesSpace)
                {
                    case CoordinatesSpace.Local:
                        {
                            if (value.UseStartX)
                            {
                                startSequence.Join(target.TweenLocalRotationX(value.StartValueX, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startSequence.Join(target.TweenLocalRotationY(value.StartValueY, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startSequence.Join(target.TweenLocalRotationZ(value.StartValueZ, 0.0f));
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
                            endSequence.Join(target.TweenLocalRotationX(value.EndValueX, timing.Duration));
                        }

                        if (value.UseEndY)
                        {
                            endSequence.Join(target.TweenLocalRotationY(value.EndValueY, timing.Duration));
                        }

                        if (value.UseEndZ)
                        {
                            endSequence.Join(target.TweenLocalRotationZ(value.EndValueZ, timing.Duration));
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

            easing.SetEasing(endSequence);

            sequenceTween.Append(endSequence);

            loop.SetLoop(sequenceTween);
        }
    }
}
