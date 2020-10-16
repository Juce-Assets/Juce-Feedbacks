using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Float", "Renderer Material/")]
    public class RendererMaterialFloatFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] [HideInInspector] private RendererMaterialPropertyElement target = default;
        [SerializeField] [HideInInspector] private FloatElement value = default;
        [SerializeField] [HideInInspector] private TimingElement timing = default;
        [SerializeField] [HideInInspector] private LoopElement loop = default;
        [SerializeField] [HideInInspector] private EasingElement easing = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target.Renderer != null)
            {
                errors = "";
                return false;
            }

            errors = "Target is null";

            return true;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target.Renderer != null ? target.Renderer.gameObject.name : string.Empty;
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
            RendererMaterialPropertyElement rendererMaterialPropertyElement = AddElement<RendererMaterialPropertyElement>(0, "Target");
            rendererMaterialPropertyElement.MaterialPropertyType = MaterialPropertyType.Float;

            AddElement<FloatElement>(1, "Values");

            AddElement<TimingElement>(2, "Timing");
            AddElement<LoopElement>(3, "Loop");

            AddElement<EasingElement>(4, "Easing");
        }

        protected override void OnLink()
        {
            target = GetElement<RendererMaterialPropertyElement>(0);
            value = GetElement<FloatElement>(1);
            timing = GetElement<TimingElement>(2);
            loop = GetElement<LoopElement>(3);
            easing = GetElement<EasingElement>(4);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if (target.Renderer == null)
            {
                return null;
            }

            Material material = target.Renderer.materials[target.MaterialIndex];

            bool hasProperty = material.HasProperty(target.Property);

            if (!hasProperty)
            {
                Debug.Log("");
                return null;
            }

            Tween.Tween delayTween = null;

            if (timing.Delay > 0)
            {
                delayTween = new WaitTimeTween(timing.Delay);
                sequenceTween.Append(delayTween);
            }

            if (value.UseStartValue)
            {
                sequenceTween.Append(material.TweenFloat(value.StartValue, target.Property, 0.0f));
            }

            Tween.Tween progressTween = material.TweenFloat(value.EndValue, target.Property, timing.Duration);
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
