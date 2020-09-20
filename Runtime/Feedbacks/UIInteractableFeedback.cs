using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Position", "GameObject/")]
    public class UIInteractableFeedback : Feedback
    {
        [SerializeField] private float duration = default;

        [Header("Target")]
        [SerializeField] private GameObject target = default;

        [Header("Values")]
        [SerializeField] private bool interactable = default;
        [SerializeField] private bool blocksRaycasts = default;

        public override void OnExectue(SequenceTween sequenceTween)
        {
            CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = target.AddComponent<CanvasGroup>();
            }

            sequenceTween.AppendCallback(() =>
            {
                canvasGroup.interactable = interactable;
                canvasGroup.blocksRaycasts = blocksRaycasts;
            });
        }
    }
}