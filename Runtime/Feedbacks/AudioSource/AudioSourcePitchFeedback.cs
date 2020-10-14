﻿using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Pitch", "AudioSource/")]
    public class AudioSourcePitchFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private AudioSource target = default;

        [SerializeField] [HideInInspector] private FloatElement value = default;
        [SerializeField] [HideInInspector] private TimingElement timing = default;
        [SerializeField] [HideInInspector] private LoopElement loop = default;
        [SerializeField] [HideInInspector] private EasingElement easing = default;

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
            string info = $"{timing.Delay} {timing.Duration}s";

            if (value.UseStartValue)
            {
                info += $" | Start: {value.StartValue} ";
            }

            info += $" | End: {value.EndValue} ";

            return info;
        }

        protected override void OnCreate()
        {
            value = AddElement<FloatElement>("Values");

            timing = AddElement<TimingElement>("Timing");
            loop = AddElement<LoopElement>("Loop");

            easing = AddElement<EasingElement>("Easing");
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            Tween.Tween delayTween = null;

            if (timing.Delay > 0)
            {
                delayTween = new WaitTimeTween(timing.Delay);
                sequenceTween.Append(delayTween);
            }

            if (value.UseStartValue)
            {
                sequenceTween.Append(target.TweenPitch(value.StartValue, 0.0f));
            }

            Tween.Tween progressTween = target.TweenPitch(value.EndValue, timing.Duration);
            sequenceTween.Append(progressTween);

            easing.SetEasing(sequenceTween);

            loop.SetLoop(sequenceTween);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}