using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class EasingElement : FeedbackElement
    {
        [SerializeField] [HideInInspector] private bool useAnimationCurve = default;
        [SerializeField] [HideInInspector] private Ease easing = Tween.Ease.InOutQuad;
        [SerializeField] [HideInInspector] private AnimationCurve animationCurveEasing = default;

        public void SetEasing(Tween.Tween tween)
        {
            if (!useAnimationCurve)
            {
                tween.SetEase(easing);
            }
            else
            {
                tween.SetEase(animationCurveEasing);
            }
        }
    }
}
