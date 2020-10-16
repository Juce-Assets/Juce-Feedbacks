using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Stop", "ParticleSystem/")]
    public class ParticleSystemStopFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private ParticleSystem target = default;

        [Header("Values")]
        [SerializeField] private bool withChildren = true;
        [SerializeField] private ParticleSystemStopBehavior stopBehavior = default;

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
            string info = "";

            return info;
        }

        protected override void OnCreate()
        {
            TimingElement timingElement = AddElement<TimingElement>(0, "Timing");
            timingElement.UseDuration = false;
        }

        protected override void OnLink()
        {
            timing = GetElement<TimingElement>(0);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if (target == null)
            {
                return null;
            }

            Tween.Tween delayTween = null;

            if (timing.Delay > 0)
            {
                delayTween = new WaitTimeTween(timing.Delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() =>
            {
                target.Stop(withChildren, stopBehavior);
            });

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}