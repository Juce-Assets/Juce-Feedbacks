using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Play", "AudioSource/")]
    public class AudioSourcePlayFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private AudioSource target = default;

        [SerializeField] [HideInInspector] private AudioClipElement value = default;
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
            value = AddElement<AudioClipElement>("Values");

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
                if (!value.OneShot)
                {
                    target.clip = value.Value;

                    target.Play();
                }
                else
                {
                    target.PlayOneShot(value.Value);
                }
            });
        }
    }
}
