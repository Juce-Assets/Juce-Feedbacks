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
            RendererMaterialPropertyElement rendererMaterialPropertyElement = AddElement<RendererMaterialPropertyElement>(0, "Target");
            rendererMaterialPropertyElement.MaterialPropertyType = MaterialPropertyType.All;

            AddElement<BoolElement>(1, "Values");

            TimingElement timingElement = AddElement<TimingElement>(2, "Timing");
            timingElement.UseDuration = false;
        }

        protected override void OnLink()
        {
            target = GetElement<RendererMaterialPropertyElement>(0);
            value = GetElement<BoolElement>(1);
            timing = GetElement<TimingElement>(2);
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

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}
