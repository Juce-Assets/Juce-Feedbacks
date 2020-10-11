using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Stop", "AudioSource/")]
    public class AudioSourceStopFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private AudioSource target = default;

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
            timing = AddElement<TimingElement>("Timing");
            timing.UseDuration = false;
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (target == null)
            {
                return;
            }

            sequenceTween.AppendWaitTime(timing.Delay);

            sequenceTween.AppendCallback(() =>
            {
                target.Stop();
            });
        }
    }
}
