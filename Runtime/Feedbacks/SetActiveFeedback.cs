using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "GameObject/")]
    public class SetActiveFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private GameObject target = default;

        [Header("Values")]
        [SerializeField] private bool setActive = default;

        public override void OnExectue(SequenceTween sequenceTween)
        {
            sequenceTween.AppendCallback(() => target.SetActive(target));
        }
    }
}
