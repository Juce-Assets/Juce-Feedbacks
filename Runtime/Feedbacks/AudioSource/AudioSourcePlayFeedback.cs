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
            AddElement<AudioClipElement>(0, "Values");
            TimingElement timingElement = AddElement<TimingElement>(1, "Timing");
            timingElement.UseDuration = false;
        }

        protected override void OnLink()
        {
            value = GetElement<AudioClipElement>(0);
            timing = GetElement<TimingElement>(1);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if(target == null)
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

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}
