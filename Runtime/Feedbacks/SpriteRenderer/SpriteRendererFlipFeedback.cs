using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Flip", "SpriteRenderer/")]
    public class SpriteRendererFlipFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private SpriteRenderer target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private bool flipX = default;

        [SerializeField] private bool flipY = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        private bool initialFlipXValue;
        private bool initialFlipYValue;

        public SpriteRenderer Target { get => target; set => target = value; }
        public bool FlipX { get => flipX; set => flipX = value; }
        public bool FlipY { get => flipY; set => flipY = value; }
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
            infoList.Add($"FlipX: {flipX}");
            infoList.Add($"FlipY: {flipY}");
        }

        public override void OnFirstTimeExecute()
        {
            if (target == null)
            {
                return;
            }

            initialFlipXValue = target.flipX;
            initialFlipYValue = target.flipY;
        }

        public override void OnReset()
        {
            if (target == null)
            {
                return;
            }

            target.flipX = initialFlipXValue;
            target.flipY = initialFlipXValue;
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

            sequenceTween.AppendCallback(() =>
            {
                target.flipX = flipX;
                target.flipY = flipY;
            });

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}