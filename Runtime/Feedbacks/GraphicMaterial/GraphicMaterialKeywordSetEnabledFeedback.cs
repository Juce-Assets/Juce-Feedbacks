using System;
using UnityEngine;
using Juce.Tween;

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
        [SerializeField] private float delay = default;
        [SerializeField] private float duration = default;

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
            return $"Value: {setEnabled}";
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
