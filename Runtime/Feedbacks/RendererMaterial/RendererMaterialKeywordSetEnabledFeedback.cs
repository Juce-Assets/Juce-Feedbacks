using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Keyword Set Enabled", "Renderer Material/")]
    public class RendererMaterialKeywordSetEnabledFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] [HideInInspector] private RendererMaterialPropertyElement target = default;

        [SerializeField] [HideInInspector] private BoolElement value = default;

        [SerializeField] [HideInInspector] private TimingElement timing = default;

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
            return $"Value: {value.Value}";
        }

        protected override void OnCreate()
        {
            target = AddElement<RendererMaterialPropertyElement>("Target");
            target.MaterialPropertyType = MaterialPropertyType.All;

            value = AddElement<BoolElement>("Enabled");

            timing = AddElement<TimingElement>("Timing");
            timing.UseDuration = false;
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (target.Renderer == null)
            {
                return;
            }

            Material material = target.Renderer.materials[target.MaterialIndex];

            bool hasProperty = material.HasProperty(target.Property);

            if (!hasProperty)
            {
                Debug.Log("");
                return;
            }

            sequenceTween.AppendWaitTime(timing.Delay);

            sequenceTween.AppendCallback(() =>
            {
                if (value.Value)
                {
                    material.EnableKeyword(target.Property);
                }
                else
                {
                    material.DisableKeyword(target.Property);
                }
            });
        }
    }
}
