using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class EasingProperty
    {
        [SerializeField] [HideInInspector] private bool useAnimationCurve = default;
        [SerializeField] [HideInInspector] private Ease easing = Tween.Ease.InOutQuad;
        [SerializeField] [HideInInspector] private AnimationCurve animationCurveEasing = default;

        public bool UseAnimationCurve => useAnimationCurve;
        public Ease Easing => easing;
        public AnimationCurve AnimationCurveEasing => animationCurveEasing;
    }
}
