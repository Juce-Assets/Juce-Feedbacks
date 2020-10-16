using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Shadow Strenght", "Light/")]
    public class LightShadowStrenghtFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] private Light target = default;

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
            string info = $"{timing.Duration}s";

            if (value.UseStartValue)
            {
                info += $" | Start: {value.StartValue} ";
            }

            info += $" | End: {value.EndValue} ";

            if (!easing.UseAnimationCurve)
            {
                info += $" | Ease: {easing.Easing}";
            }
            else
            {
                info += $" | Ease: Curve";
            }

            return info;
        }

        protected override void OnCreate()
        {
            FloatElement floatElement = AddElement<FloatElement>(0, "Values");
            floatElement.MinValue = 0.0f;

            AddElement<TimingElement>(1, "Timing");
            AddElement<LoopElement>(2, "Loop");

            AddElement<EasingElement>(3, "Easing");
        }

        protected override void OnLink()
        {
            value = GetElement<FloatElement>(0);

            timing = GetElement<TimingElement>(1);
            loop = GetElement<LoopElement>(2);

            easing = GetElement<EasingElement>(3);
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
                sequenceTween.Append(target.TweenShadowStrenght(value.StartValue, 0.0f));
            }

            Tween.Tween progressTween = target.TweenShadowStrenght(value.EndValue, timing.Duration);
            sequenceTween.Append(target.TweenShadowStrenght(value.EndValue, timing.Duration));

            easing.SetEasing(sequenceTween);

            loop.SetLoop(sequenceTween);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}
