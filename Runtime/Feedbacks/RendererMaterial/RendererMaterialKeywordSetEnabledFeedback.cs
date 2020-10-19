using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;
using System.Collections.Generic;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Keyword Set Enabled", "Renderer Material/")]
    public class RendererMaterialKeywordSetEnabledFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] [HideInInspector] private RendererMaterialProperty target = new RendererMaterialProperty();

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] [HideInInspector] private bool enabled = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target.Renderer == null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            if (string.IsNullOrEmpty(target.Property))
            {
                errors = ErrorUtils.MaterialPropertyNotSelected;
                return true;
            }

            errors = "";
            return false;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target.Renderer != null ? target.Renderer.gameObject.name : string.Empty;
        }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            InfoUtils.GetTimingInfo(ref infoList, delay);
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

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() =>
            {
                if (enabled)
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
