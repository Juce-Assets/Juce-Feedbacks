using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Interactable", "UI/")]
    public class UIInteractableFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private GameObject target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private bool interactable = default;
        [SerializeField] private bool blocksRaycasts = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target == null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            errors = "";

            return true;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            return $"Interactable: {interactable} | Blocks raycast: {blocksRaycasts}";
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            CanvasGroup canvasGroup = target.GetOrAddComponent<CanvasGroup>();

            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() =>
            {
                canvasGroup.interactable = interactable;
                canvasGroup.blocksRaycasts = blocksRaycasts;
            });

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}