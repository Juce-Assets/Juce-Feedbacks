using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Set Enabled", "MonoBehaviour/")]
    public class MonoBehaviourSetEnabledFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private MonoBehaviour target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private bool setEnabled = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        private bool initialEnabledValue;

        public MonoBehaviour Target { get => target; set => target = value; }
        public bool SetEnabled { get => setEnabled; set => setEnabled = value; }
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
            infoList.Add($"Enabled: {setEnabled}");
        }

        public override void OnFirstTimeExecute()
        {
            if (target == null)
            {
                return;
            }

            initialEnabledValue = target.enabled;
        }

        public override void OnReset()
        {
            if (target == null)
            {
                return;
            }

            target.enabled = initialEnabledValue;
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

            sequenceTween.AppendCallback(() => target.enabled = setEnabled);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}