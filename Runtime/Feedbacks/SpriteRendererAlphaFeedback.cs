using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class SpriteRendererAlphaFeedback : Feedback
    {
        [SerializeField] private float duration = default;

        [Header("Target")]
        [SerializeField] private SpriteRenderer sriteRenderer = default;

        [Header("Values")]
        [SerializeField] private bool useStartingValue = default;
        [SerializeField] private float start = default;
        [SerializeField] private float end = default;

        [Header("Easing")]
        [SerializeField] private bool useCustomEasing = default;
        [SerializeField] private Ease easing = Ease.InOutQuad;
        [SerializeField] private AnimationCurve customEasing = default;

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (useStartingValue)
            {
                sequenceTween.Append(sriteRenderer.TweenColorAlpha(start, 0.0f));
            }

            Tween.Tween tween = sriteRenderer.TweenColorAlpha(end, duration);
            SetEasing(tween);
            sequenceTween.Append(tween);
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