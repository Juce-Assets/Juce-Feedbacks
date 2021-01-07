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
        [SerializeField] private ScriptUsageProperty scriptUsage = default;
        [SerializeField] private bool useTimeScale = default;
        [SerializeField] private LoopProperty loop = default;

        private bool firstTimeExecute = true;
        private SequenceTween currMainSequence;

        internal ScriptUsageProperty RegisteredScriptUsage;

        public bool IsPlaying { get; private set; }
        public IReadOnlyList<Feedback> Feedbacks => feedbacks;

        public ScriptUsageProperty ScriptUsage => scriptUsage;
        public LoopProperty Loop => loop;

        public event Action<string> OnEventTrigger;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                TryRegister();
            }
        }

        private void Start()
        {
            if (Application.isPlaying)
            {
                TryExecuteOnAwake();
            }
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
            {
                TryUnregister();
            }

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

        private void TryRegister()
        {
            if (!scriptUsage.UsedByScript)
            {
                return;
            }

            bool success = JuceFeedbacks.Instance.RegisterFeedbacksPlayerUsedByScript(this);

            if (success)
            {
                RegisteredScriptUsage = scriptUsage;
            }
        }

        private void TryUnregister()
        {
            if (RegisteredScriptUsage == null)
            {
                return;
            }

            if (!RegisteredScriptUsage.UsedByScript)
            {
                return;
            }

            JuceFeedbacks.Instance.UnregisterFeedbacksPlayerUsedByScript(RegisteredScriptUsage.IdUsedByScript);

            RegisteredScriptUsage = null;
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

            if (firstTimeExecute)
            {
                firstTimeExecute = false;

                for (int i = 0; i < feedbacks.Count; ++i)
                {
                    Feedback currFeedback = feedbacks[i];

                    if (currFeedback == null)
                    {
                        continue;
                    }

                    currFeedback.OnFirstTimeExecute();
                }
            }

            FlowContext context = new FlowContext(OnEventTrigger);

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if (currFeedback == null)
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

            context.MainSequence.SetUseGeneralTimeScale(useTimeScale);

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

        public void KillAndReset()
        {
            if (currMainSequence == null)
            {
                return;
            }

            currMainSequence.Kill();
            currMainSequence.Reset(ResetMode.RestartValues);

            for (int i = 0; i < feedbacks.Count; ++i)
            {
                Feedback currFeedback = feedbacks[i];

                if (currFeedback == null)
                {
                    continue;
                }

                currFeedback.OnReset();
            }
        }

        public void Restart()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            KillAndReset();

            Play();
        }


        /// <summary>
        /// Returns the <typeparamref name="Feedback"/> found, which has the Used By Script toggled, and the Id Used By Script defined on the editor.
        /// Returns null if not found.
        /// </summary>
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