using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "GameObject/")]
    public class RotationFeedback : Feedback
    {
        [SerializeField] private float duration = default;

        [Header("Target")]
        [SerializeField] private GameObject target = default;

        [Header("Values")]
        [SerializeField] private CoordinatesSpace space = default;
        [SerializeField] private bool useStartingValue = default;
        [SerializeField] private Vector3 start = default;
        [SerializeField] private Vector3 end = default;

        [Header("Easing")]
        [SerializeField] private bool useCustomEasing = default;
        [SerializeField] private Ease easing = Ease.InOutQuad;
        [SerializeField] private AnimationCurve customEasing = default;

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (useStartingValue)
            {
                switch (space)
                {
                    case CoordinatesSpace.Local:
                        {
                            sequenceTween.Append(target.transform.TweenLocalRotation(start, 0.0f));
                        }
                        break;

                    case CoordinatesSpace.World:
                        {
                            sequenceTween.Append(target.transform.TweenRotation(start, 0.0f));
                        }
                        break;
                }
            }

            switch (space)
            {
                case CoordinatesSpace.Local:
                    {
                        Tween.Tween tween = target.transform.TweenLocalRotation(end, duration);
                        SetEasing(tween);
                        sequenceTween.Append(tween);
                    }
                    break;

                case CoordinatesSpace.World:
                    {
                        Tween.Tween tween = target.transform.TweenRotation(end, duration);
                        SetEasing(tween);
                        sequenceTween.Append(tween);
                    }
                    break;
            }
        }

        private void SetEasing(Tween.Tween tween)
        {
            if (!useCustomEasing)
            {
                tween.SetEase(easing);
            }
            else
            {
                tween.SetEase(customEasing);
            }
        }
    }
}
