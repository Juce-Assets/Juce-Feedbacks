using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [FeedbackIdentifier("Play", "AudioSource/")]
    public class AudioSourcePlayFeedback : Feedback
    {
        [Header(FeedbackSectionsUtils.TargetSection)]
        [SerializeField] private AudioSource target = default;

        [Header(FeedbackSectionsUtils.ValuesSection)]
        [SerializeField] private AudioClip audioClip = default;
        [SerializeField] private bool oneShot = default;

        [Header(FeedbackSectionsUtils.TimingSection)]
        [SerializeField] [Min(0)] private float delay = default;

        [Header(FeedbackSectionsUtils.LoopSection)]
        [SerializeField] private LoopProperty loop = default;

        public override bool GetFeedbackErrors(out string errors)
        {
            if (target != null)
            {
                errors = ErrorUtils.TargetNullErrorMessage;
                return true;
            }

            errors = "";

            return false;
        }

        public override string GetFeedbackTargetInfo()
        {
            return target != null ? target.gameObject.name : string.Empty;
        }

        public override string GetFeedbackInfo()
        {
            string info = "";

            return info;
        }

        public override ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween)
        {
            if(target == null)
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
                if (!oneShot)
                {
                    target.clip = audioClip;

                    target.Play();
                }
                else
                {
                    target.PlayOneShot(audioClip);
                }
            });

            LoopUtils.SetLoop(sequenceTween, loop);

            ExecuteResult result = new ExecuteResult();
            result.DelayTween = delayTween;

            return result;
        }
    }
}
