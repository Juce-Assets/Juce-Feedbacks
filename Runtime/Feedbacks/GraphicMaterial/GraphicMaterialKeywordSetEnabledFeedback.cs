using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Keyword Set Enabled", "Graphic Material/")]
    public class GraphicMaterialKeywordSetEnabledFeedback : Feedback
    {
        [Header("Target")]
        [SerializeField] [HideInInspector] private GraphicMaterialPropertyElement target = default;

        [Header("Value")]
        [SerializeField] [HideInInspector] private BoolElement value = default;

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

        public override string GetFeedbackTargetInfo()
        {
            return target.Graphic != null ? target.Graphic.gameObject.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            return $"Value: {value.Value}";
        }

        protected override void OnCreate()
        {
            target = AddElement<GraphicMaterialPropertyElement>("Target");
            target.MaterialPropertyType = MaterialPropertyType.All;

            value = AddElement<BoolElement>("Enabled");
        }

        public override void OnExectue(SequenceTween sequenceTween)
        {
            if (target.Graphic == null)
            {
                return;
            }

            GraphicMaterialUtils.TryInstantiateGraphicMaterial(target);

            Material material = target.Graphic.materialForRendering;

            bool hasProperty = material.HasProperty(target.Property);

            if (!hasProperty)
            {
                Debug.Log("");
                return;
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
        }
    }
}
