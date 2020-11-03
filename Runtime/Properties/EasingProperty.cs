using Juce.Tween;
using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class EasingProperty
    {
        [SerializeField] [HideInInspector] private bool useAnimationCurve = default;
        [SerializeField] [HideInInspector] private Ease easing = Tween.Ease.InOutQuad;
        [SerializeField] [HideInInspector] private AnimationCurve animationCurveEasing = default;

        public bool UseAnimationCurve { get => useAnimationCurve; set => useAnimationCurve = value; }
        public Ease Easing { get => easing; set => easing = value; }
        public AnimationCurve AnimationCurveEasing { get => animationCurveEasing; set => animationCurveEasing = value; }
    }
}