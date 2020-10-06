using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "GameObject/")]
    public class PositionFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private GameObject target = default;

        [SerializeField] [HideInInspector] private DurationElement duration = default;
        [SerializeField] [HideInInspector] private LoopElement loop = default;
        [SerializeField] [HideInInspector] private CoordinatesSpaceElement coordinatesSpace;
        [SerializeField] [HideInInspector] private Vector3Element value = default;
        [SerializeField] [HideInInspector] private EasingElement easing = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if(target != null)
            {
                errors = "";
                return false;
            }

            errors = "Target is null";

            return true;
        }

        public override string GetFeedbackInfo()
        {
            string info = $"{duration.Duration}s";

            if(value.UseStartPosition)
            {
                info += $" | Start: x:{value.StartPositionX} y:{value.StartPositionY} z: {value.StartPositionZ}";
            }

            info += $" | End: x:{value.EndPositionX} y:{value.EndPositionY} z: {value.EndPositionZ}";

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
            duration = AddElement<DurationElement>("Timing");
            loop = AddElement<LoopElement>("Loop");
            coordinatesSpace = AddElement<CoordinatesSpaceElement>("Space");
            value = AddElement<Vector3Element>("Value");
            easing = AddElement<EasingElement>("Easing");
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (value.UseStartPosition)
            {
                SequenceTween startPositionSequence = new SequenceTween();

                switch (coordinatesSpace.CoordinatesSpace)
                {
                    case CoordinatesSpace.Local:
                        {
                            if(value.UseStartX)
                            {
                                startPositionSequence.Join(target.transform.TweenLocalPositionX(value.StartPositionX, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startPositionSequence.Join(target.transform.TweenLocalPositionY(value.StartPositionY, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startPositionSequence.Join(target.transform.TweenLocalPositionZ(value.StartPositionZ, 0.0f));
                            }
                        }
                        break;

                    case CoordinatesSpace.World:
                        {
                            if (value.UseStartX)
                            {
                                startPositionSequence.Join(target.transform.TweenPositionX(value.StartPositionX, 0.0f));
                            }

                            if (value.UseStartY)
                            {
                                startPositionSequence.Join(target.transform.TweenPositionY(value.StartPositionY, 0.0f));
                            }

                            if (value.UseStartZ)
                            {
                                startPositionSequence.Join(target.transform.TweenPositionZ(value.StartPositionZ, 0.0f));
                            }
                        }
                        break;
                }

                sequenceTween.Append(startPositionSequence);
            }

            SequenceTween endPositionSequence = new SequenceTween();

            switch (coordinatesSpace.CoordinatesSpace)
            {
                case CoordinatesSpace.Local:
                    {
                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenLocalPositionX(value.EndPositionX, duration.Duration));
                        }

                        if (value.UseEndY)
                        {
                            endPositionSequence.Join(target.transform.TweenLocalPositionY(value.EndPositionY, duration.Duration));
                        }

                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenLocalPositionZ(value.EndPositionZ, duration.Duration));
                        }
                    }
                    break;

                case CoordinatesSpace.World:
                    {
                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenPositionX(value.EndPositionX, duration.Duration));
                        }

                        if (value.UseEndY)
                        {
                            endPositionSequence.Join(target.transform.TweenPositionY(value.EndPositionY, duration.Duration));
                        }

                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenPositionZ(value.EndPositionZ, duration.Duration));
                        }
                    }
                    break;
            }

            easing.SetEasing(endPositionSequence);

            sequenceTween.Append(endPositionSequence);

            loop.SetLoop(sequenceTween);
        }
    }
}
