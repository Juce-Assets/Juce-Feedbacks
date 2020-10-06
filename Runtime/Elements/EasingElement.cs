using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class EasingElement : Element
    {
        [SerializeField] [HideInInspector] private bool useAnimationCurve = default;
        [SerializeField] [HideInInspector] private Ease easing = Tween.Ease.InOutQuad;
        [SerializeField] [HideInInspector] private AnimationCurve animationCurveEasing = default;

        public bool UseAnimationCurve => useAnimationCurve;
        public Ease Easing => easing;

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
