using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class FeedbacksPlayer : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private List<Feedback> feedbacks = new List<Feedback>();

        [SerializeField] private bool executeOnAwake = default;
        [SerializeField] private LoopProperty loop = default;

        internal SequenceTween CurrMainSequence { get; private set; }

        public IReadOnlyList<Feedback> Feedbacks => feedbacks;

        private void Start()
        {
            TryExecuteOnAwake(); 
        }

        private void TryExecuteOnAwake()
        {
            if(!executeOnAwake)
            {
                return;
            }

            Play();
        }

        public Task Play()
        {
            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            Play(() => taskCompletionSource.SetResult(null));

            return taskCompletionSource.Task;
        }

        public void Play(Action onFinish = null)
        {
            Kill();

            FlowContext context = new FlowContext();

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if(!currFeedback.Enabled)
                {
                    continue;
                }

                Tween.SequenceTween sequenceTween = new Tween.SequenceTween();

                ExecuteResult executeResult = currFeedback.OnExecute(context, sequenceTween);

                if(executeResult == null)
                {
                    continue;
                }

                context.CurrentSequence.Join(sequenceTween);

                currFeedback.ExecuteResult = executeResult;
            }

            context.MainSequence.Append(context.CurrentSequence);

            LoopUtils.SetLoop(context.MainSequence, loop);

            context.MainSequence.onCompleteOrKill += () => onFinish?.Invoke();

            context.MainSequence.Play();

            CurrMainSequence = context.MainSequence;
        }

        public void Complete()
        {
            if (CurrMainSequence == null)
            {
                return;
            }
;
            CurrMainSequence.Complete();
        }

        public void Kill()
        {
            if (CurrMainSequence == null)
            {
                return;
            }
;
            CurrMainSequence.Kill();
        }

        public void Restart()
        {
            if (CurrMainSequence == null)
            {
                return;
            }
;
            CurrMainSequence.Restart();
        }

        public T GetFeedback<T>(string id) where T : Feedback
        {
            Type lookingForType = typeof(T);

            for(int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if(!currFeedback.ScriptUsage.UsedByScript)
                {
                    continue;
                }

                if(currFeedback.GetType() == lookingForType)
                {
                    if(string.Equals(currFeedback.ScriptUsage.IdUsedByScript, id))
                    {
                        return currFeedback as T;
                    }
                }
            }

            return null;
        }
    }
}
