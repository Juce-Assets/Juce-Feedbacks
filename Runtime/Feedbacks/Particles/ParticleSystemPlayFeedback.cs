using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Play", "ParticleSystem/")]
    public class ParticleSystemPlayFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private ParticleSystem target = default;

        [Header("Values")]
        [SerializeField] private bool withChildren = true;

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

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (target == null)
            {
                return;
            }

            sequenceTween.AppendCallback(() =>
            {
                target.Play(withChildren);
            });
        }
    }
}