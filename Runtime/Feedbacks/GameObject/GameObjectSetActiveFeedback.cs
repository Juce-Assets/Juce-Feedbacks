using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("SetActive", "GameObject/")]
    public class GameObjectSetActiveFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private GameObject target = default;

        [SerializeField] [HideInInspector] private BoolElement value = default;
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
            return target != null ? target.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            return $"Value: {value.Value}";
        }

        protected override void OnCreate()
        {
            value = AddElement<BoolElement>("Value");
            timing = AddElement<TimingElement>("Timing");
        }

        public override void OnExectue(FlowContext context, SequenceTween sequenceTween)
        {
            sequenceTween.AppendWaitTime(context.CurrentDelay + timing.Delay);

            sequenceTween.AppendCallback(() => target.SetActive(value.Value));
        }
    }
}
