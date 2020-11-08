using Juce.Tween;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.Feedbacks
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class FeedbacksPlayer : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private List<Feedback> feedbacks = new List<Feedback>();

        [SerializeField] private bool executeOnAwake = default;
        [SerializeField] private LoopProperty loop = default;

        private SequenceTween currMainSequence;

        public bool IsPlaying { get; private set; }
        public IReadOnlyList<Feedback> Feedbacks => feedbacks;

        public event Action<string> OnEventTrigger;

        private void Start()
        {
            if (Application.isPlaying)
            {
                TryExecuteOnAwake();
            }
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        private void CleanUp()
        {
            foreach (Feedback feedback in feedbacks)
            {
#if UNITY_EDITOR

                if (Application.isPlaying)
                {
                    Destroy(feedback);
                }
                else
                {
                    UnityEditor.EditorApplication.delayCall += () =>
                    {
                        DestroyImmediate(feedback);
                    };
                }

#else

                Destroy(feedback);

#endif
            }

            feedbacks.Clear();
        }

        private void TryExecuteOnAwake()
        {
            if (!executeOnAwake)
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

            IsPlaying = true;

            FlowContext context = new FlowContext(OnEventTrigger);

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if(currFeedback == null)
                {
                    UnityEngine.Debug.LogError($"There is a null {nameof(Feedback)} on the GameObject {gameObject.name}", gameObject);
                    continue;
                }

                if (currFeedback.Disabled)
                {
                    continue;
                }

                Tween.SequenceTween sequenceTween = new Tween.SequenceTween();

                ExecuteResult executeResult = currFeedback.OnExecute(context, sequenceTween);

                if (executeResult == null)
                {
                    continue;
                }

                context.CurrentSequence.Join(sequenceTween);

                currFeedback.ExecuteResult = executeResult;
            }

            context.MainSequence.Append(context.CurrentSequence);

            LoopUtils.SetLoop(context.MainSequence, loop);

            context.MainSequence.onCompleteOrKill += () =>
            {
                IsPlaying = false;

                onFinish?.Invoke();
            };

            context.MainSequence.Play();

            currMainSequence = context.MainSequence;
        }

        public void Complete()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Complete();
        }

        public void Kill()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Kill();
        }

        public void Restart()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Restart();
        }

        public T GetFeedback<T>(string id) where T : Feedback
        {
            Type lookingForType = typeof(T);

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if (!currFeedback.ScriptUsage.UsedByScript)
                {
                    continue;
                }

                if (currFeedback.GetType() == lookingForType)
                {
                    if (string.Equals(currFeedback.ScriptUsage.IdUsedByScript, id))
                    {
                        return currFeedback as T;
                    }
                }
            }

            return null;
        }
    }
}