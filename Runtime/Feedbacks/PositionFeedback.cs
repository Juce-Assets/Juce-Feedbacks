using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "GameObject/")]
    public class PositionFeedback : Feedback
    {
        [SerializeField] private float duration = default;

        [Header("Target")]
        [SerializeField] private GameObject target = default;

        [Header("Values")]
        [SerializeField] private CoordinatesSpace space = default;

        [SerializeField] [HideInInspector] private Vector3Element value = default;
        [SerializeField] [HideInInspector] private EasingElement easing = default;

        public override void OnCreate()
        {
            value = AddElement<Vector3Element>("Value");
            easing = AddElement<EasingElement>("Easing");
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (value.UseStartPosition)
            {
                SequenceTween startPositionSequence = new SequenceTween();

                switch (space)
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

            switch (space)
            {
                case CoordinatesSpace.Local:
                    {
                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenLocalPositionX(value.EndPositionX, duration));
                        }

                        if (value.UseEndY)
                        {
                            endPositionSequence.Join(target.transform.TweenLocalPositionY(value.EndPositionY, duration));
                        }

                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenLocalPositionZ(value.EndPositionZ, duration));
                        }
                    }
                    break;

                case CoordinatesSpace.World:
                    {
                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenPositionX(value.EndPositionX, duration));
                        }

                        if (value.UseEndY)
                        {
                            endPositionSequence.Join(target.transform.TweenPositionY(value.EndPositionY, duration));
                        }

                        if (value.UseEndX)
                        {
                            endPositionSequence.Join(target.transform.TweenPositionZ(value.EndPositionZ, duration));
                        }
                    }
                    break;
            }

            easing.SetEasing(endPositionSequence);

            sequenceTween.Append(endPositionSequence);
        }
    }
}
