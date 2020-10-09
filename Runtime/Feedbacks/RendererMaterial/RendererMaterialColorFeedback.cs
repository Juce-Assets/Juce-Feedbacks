using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Color", "Renderer Material/")]
    public class RendererMaterialColorFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] [HideInInspector] private RendererMaterialPropertyElement target = default;
        [SerializeField] [HideInInspector] private DurationElement duration = default;
        [SerializeField] [HideInInspector] private LoopElement loop = default;
        [SerializeField] [HideInInspector] private ColorElement value = default;
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

        public override string GetFeedbackInfo()
        {
            string info = $"{duration.Duration}s";

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
            target = AddElement<RendererMaterialPropertyElement>("Target");
            target.MaterialPropertyType = MaterialPropertyType.Color;

            duration = AddElement<DurationElement>("Timing");
            loop = AddElement<LoopElement>("Loop");

            value = AddElement<ColorElement>("Values");

            easing = AddElement<EasingElement>("Easing");
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if(target.Renderer == null)
            {
                return;
            }

            Material material = target.Renderer.materials[target.MaterialIndex];

            bool hasProperty = material.HasProperty(target.Property);

            if(!hasProperty)
            {
                Debug.Log("");
                return;
            }

            if (value.UseStartValue)
            {
                sequenceTween.Append(material.TweenColor(value.StartValue, target.Property, 0.0f));
            }

            sequenceTween.Append(material.TweenColor(value.EndValue, target.Property, duration.Duration));

            easing.SetEasing(sequenceTween);

            loop.SetLoop(sequenceTween);
        }
    }
}
