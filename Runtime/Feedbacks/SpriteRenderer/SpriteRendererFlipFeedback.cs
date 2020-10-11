using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Flip", "SpriteRenderer/")]
    public class SpriteRendererFlipFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private SpriteRenderer target = default;

        [Header("Values")]
        [SerializeField] private bool flipX = default;
        [SerializeField] private bool flipY = default;

        [SerializeField] [HideInInspector] private TimingElement timing = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target != null)
            {
                errors = "";
                return false;
            }

            errors = "Target is null";

            return true;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.gameObject.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            return "";
        }

        protected override void OnCreate()
        {
            timing = AddElement<TimingElement>("Timing");
            timing.UseDuration = false;
        }

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            if (target == null)
            {
                return;
            }

            sequenceTween.AppendWaitTime(context.CurrentDelay + timing.Delay);

            sequenceTween.AppendCallback(() =>
            {
                target.flipX = flipX;
                target.flipY = flipY;
            });
        }
    }
}
