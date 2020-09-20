using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "GameObject/")]
    public class UIAlphaFeedback : Feedback
    {
        [SerializeField] private float duration = default;

        [Header("Target")]
        [SerializeField] private GameObject target = default;

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
            CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();

            if(canvasGroup == null)
            {
                canvasGroup = target.AddComponent<CanvasGroup>();
            }

            if (useStartingValue)
            {
                sequenceTween.Append(canvasGroup.TweenAlpha(start, 0.0f));
            }

            Tween.Tween tween = canvasGroup.TweenAlpha(end, duration);
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