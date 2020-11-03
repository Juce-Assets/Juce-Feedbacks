using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField] [Min(0)] private float duration = 1.0f;

        [Header(FeedbackSectionsUtils.EasingSection)]
        [SerializeField] private EasingProperty easing = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty looping = default;

        public RendererMaterialColorProperty Target => target;
        public StartEndColorProperty Value => value;
        public float Delay { get => delay; set => delay = Mathf.Max(0, value); }
        public float Duration { get => duration; set => duration = Mathf.Max(0, value); }
        public EasingProperty Easing => easing;
        public LoopProperty Looping => looping;

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

            errors = string.Empty;
            return false;
        }

        public override string GetFeedbackTargetInfo()
        {
            string targetInfo = string.Empty;

            if (target.Renderer != null)
            {
                targetInfo += target.Renderer.gameObject.name;
            }

            if (!string.IsNullOrEmpty(target.Property))
            {
                targetInfo += $" -> {target.Property}";
            }

            return targetInfo;
        }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            InfoUtils.GetTimingInfo(ref infoList, delay, duration);
            InfoUtils.GetStartEndColorPropertyInfo(ref infoList, value);
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
                SequenceTween startSequence = new SequenceTween();

                if (value.UseStartColor)
                {
                    startSequence.Join(material.TweenColorNoAlpha(value.StartColor, 0.0f));
                }

                if (value.UseStartAlpha)
                {
                    startSequence.Join(material.TweenColorAlpha(value.StartAlpha, 0.0f));
                }

                sequenceTween.Append(startSequence);
            }

            SequenceTween endSequence = new SequenceTween();

            if (value.UseEndColor)
            {
                endSequence.Join(material.TweenColorNoAlpha(value.EndColor, duration));
            }

            if (value.UseEndAlpha)
            {
                endSequence.Join(material.TweenColorAlpha(value.EndAlpha, duration));
            }

            Tween.Tween progressTween = endSequence;

            EasingUtils.SetEasing(sequenceTween, easing);
            LoopUtils.SetLoop(sequenceTween, looping);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;
            result.ProgresTween = progressTween;

            return result;
        }
    }
}