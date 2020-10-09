using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Color Alpha", "Graphic Material/")]
    public class GraphicMaterialColorAlphaFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] [HideInInspector] private GraphicMaterialPropertyElement target = default;
        [SerializeField] [HideInInspector] private DurationElement duration = default;
        [SerializeField] [HideInInspector] private LoopElement loop = default;
        [SerializeField] [HideInInspector] private FloatElement value = default;
        [SerializeField] [HideInInspector] private EasingElement easing = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target.Graphic != null)
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
            target = AddElement<GraphicMaterialPropertyElement>("Target");
            target.MaterialPropertyType = MaterialPropertyType.Color;

            duration = AddElement<DurationElement>("Timing");
            loop = AddElement<LoopElement>("Loop");

            value = AddElement<FloatElement>("Values");
            value.MinValue = 0.0f;
            value.MaxValue = 1.0f;

            easing = AddElement<EasingElement>("Easing");
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (target.Graphic == null)
            {
                return;
            }

            if (target.InstantiateMaterial)
            {
                GraphicMaterialInstance materialInstance = target.Graphic.gameObject.GetComponent<GraphicMaterialInstance>();

                if (materialInstance == null)
                {
                    materialInstance = target.Graphic.gameObject.AddComponent<GraphicMaterialInstance>();

                    materialInstance.Init(target.Graphic);
                }
            }

            Material material = target.Graphic.materialForRendering;

            bool hasProperty = material.HasProperty(target.Property);

            if (!hasProperty)
            {
                Debug.Log("");
                return;
            }

            if (value.UseStartValue)
            {
                sequenceTween.Append(material.TweenColorAlpha(value.StartValue, target.Property, 0.0f));
            }

            sequenceTween.Append(material.TweenColorAlpha(value.EndValue, target.Property, duration.Duration));

            easing.SetEasing(sequenceTween);

            loop.SetLoop(sequenceTween);
        }
    }
}
