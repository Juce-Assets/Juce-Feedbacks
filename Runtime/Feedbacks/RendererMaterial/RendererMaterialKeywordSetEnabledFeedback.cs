using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Keyword Set Enabled", "Renderer Material/")]
    public class RendererMaterialKeywordSetEnabledFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private RendererMaterialProperty target = new RendererMaterialProperty();

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private bool setEnabled = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public RendererMaterialProperty Target => target;
        public bool SetEnabled { get => setEnabled; set => setEnabled = value; }
        public float Delay { get => delay; set => delay = Mathf.Max(0, value); }

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
            InfoUtils.GetTimingInfo(ref infoList, delay);
            infoList.Add($"Enabled: {setEnabled}");
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