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
            AddElement<BoolElement>(0, "Value");

            TimingElement timingElement = AddElement<TimingElement>(1, "Timing");
            timingElement.UseDuration = false;
        }

        protected override void OnLink()
        {
            value = GetElement<BoolElement>(0);
            timing = GetElement<TimingElement>(1);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            Tween.Tween delayTween = null;

            if (timing.Delay > 0)
            {
                delayTween = new WaitTimeTween(timing.Delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() => target.SetActive(value.Value));

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}
