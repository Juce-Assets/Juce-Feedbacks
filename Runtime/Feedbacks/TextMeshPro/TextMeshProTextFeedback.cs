#if JUCE_TEXT_MESH_PRO_EXTENSIONS

using Juce.Tween;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Text", "TextMeshPro/")]
    public class TextMeshProTextFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private TextMeshProUGUI target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private string value = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        public TextMeshProUGUI Target { get => target; set => target = value; }
        public string Value => value;
        public float Delay { get => delay; set => delay = Mathf.Max(0, value); }

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target == null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            errors = string.Empty;
            return false;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.gameObject.name : string.Empty;
        }

        public override void GetFeedbackInfo(ref List<string> infoList)
        {
            InfoUtils.GetTimingInfo(ref infoList, delay);
            infoList.Add($"Value {value}");
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if (target == null)
            {
                return null;
            }

            Tween.Tween delayTween = null;

            if (delay > 0)
            {
                delayTween = new WaitTimeTween(delay);
                sequenceTween.Append(delayTween);
            }

            sequenceTween.AppendCallback(() => target.text = value);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}

#endif