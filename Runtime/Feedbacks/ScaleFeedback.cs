using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "GameObject/")]
    public class ScaleFeedback : Feedback
    {
        [SerializeField] private float duration = default;

        [Header("Target")]
        [SerializeField] private GameObject target = default;

        [Header("Values")]
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
                sequenceTween.Append(target.transform.TweenLocalScale(start, 0.0f));
            }

            Tween.Tween tween = target.transform.TweenLocalScale(end, duration);
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
