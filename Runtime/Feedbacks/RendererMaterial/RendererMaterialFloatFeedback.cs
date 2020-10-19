using System;
using UnityEngine;
using Juce.Tween;
using System.Collections.Generic;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Float", "Renderer Material/")]
    public class RendererMaterialFloatFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private RendererMaterialFloatProperty target = new RendererMaterialFloatProperty();

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private StartEndFloatProperty value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;
        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty loop = default;

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
            InfoUtils.GetTimingInfo(ref infoList, delay, duration);
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

            if (value.UseStartValue)
            {
                sequenceTween.Append(material.TweenFloat(value.StartValue, target.Property, 0.0f));
            }

            Tween.Tween progressTween = material.TweenFloat(value.EndValue, target.Property, duration);
            sequenceTween.Append(progressTween);

            EasingUtils.SetEasing(sequenceTween, easing);
            LoopUtils.SetLoop(sequenceTween, loop);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}
