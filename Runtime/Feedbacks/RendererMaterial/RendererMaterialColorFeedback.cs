using System;
using UnityEngine;
using UnityEngine.UI;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Color", "Renderer Material/")]
    public class RendererMaterialColorFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private RendererMaterialColorProperty target = new RendererMaterialColorProperty();

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private StartEndColorProperty value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;
        [SerializeField] [Min(0)] private float duration = default;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty loop = default;

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
            string info = $"{duration}s";

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

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if(target.Renderer == null)
            {
                return null;
            }

            Material material = target.Renderer.materials[target.MaterialIndex];

            bool hasProperty = material.HasProperty(target.Property);

            if(!hasProperty)
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
                if (value.UseStartColor)
                {
                    sequenceTween.Append(material.TweenColorNoAlpha(value.StartColor, 0.0f));
                }

                if (value.UseStartAlpha)
                {
                    sequenceTween.Append(material.TweenColorAlpha(value.StartAlpha, 0.0f));
                }
            }

            SequenceTween endSequence = new SequenceTween();

            if (value.UseEndColor)
            {
                endSequence.Append(material.TweenColorNoAlpha(value.EndColor, duration));
            }

            if (value.UseEndAlpha)
            {
                endSequence.Append(material.TweenColorAlpha(value.EndAlpha, duration));
            }

            Tween.Tween progressTween = endSequence;

            EasingUtils.SetEasing(sequenceTween, easing);
            LoopUtils.SetLoop(sequenceTween, loop);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}
