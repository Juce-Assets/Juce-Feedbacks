using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Sprite", "Image/")]
    public class ImageSpriteFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private Image target = default;

        [SerializeField] [HideInInspector] private SpriteElement value = default;
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
            return value.Value != null ? value.Value.name : string.Empty;
        }

        protected override void OnCreate()
        {
            value = AddElement<SpriteElement>("Values");

            timing = AddElement<TimingElement>("Timing");
            timing.UseDuration = false;
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if(target == null)
            {
                return;
            }

            sequenceTween.AppendWaitTime(timing.Delay);

            sequenceTween.AppendCallback(() =>
            {
                target.sprite = value.Value;
            });
        }
    }
}
