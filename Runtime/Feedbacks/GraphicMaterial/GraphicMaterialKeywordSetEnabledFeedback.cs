using System;
using UnityEngine;
using Juce.Tween;
using System.Collections.Generic;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Keyword Set Enabled", "Graphic Material/")]
    public class GraphicMaterialKeywordSetEnabledFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private GraphicMaterialProperty target = new GraphicMaterialProperty();

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private bool setEnabled = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;
        [SerializeField] [Min(0)] private float duration = 1.0f;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target.Graphic == null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            if(string.IsNullOrEmpty(target.Property))
            {
                errors = ErrorUtils.MaterialPropertyNotSelected;
                return true;
            }

            errors = "";
            return false;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target.Graphic != null ? target.Graphic.gameObject.name : string.Empty;
        }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            InfoUtils.GetTimingInfo(ref infoList, delay, duration);
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if (target.Graphic == null)
            {
                return null;
            }

            GraphicMaterialUtils.TryInstantiateGraphicMaterial(target);

            Material material = target.Graphic.materialForRendering;

            bool hasProperty = material.HasProperty(target.Property);

            if (!hasProperty)
            {
                Debug.Log("");
                return null;
            }

            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() =>
            {
                if (setEnabled)
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
