using Juce.Tween;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Feedbacks
{
    public abstract class Feedback : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private bool expanded = true;
        [SerializeField] [HideInInspector] private bool disabled = default;

        [SerializeField] private string userData = default;

        [Header("Scripting")]
        [SerializeField] private ScriptUsageProperty scriptUsage = default;

        public bool Expanded { get => expanded; set => expanded = value; }
        public bool Disabled { get => disabled; set => disabled = value; }
        public string UserData { get => userData; set => userData = value; }
        public ScriptUsageProperty ScriptUsage => scriptUsage;

        public ExecuteResult ExecuteResult { get; set; }

        public virtual string GetFeedbackTargetInfo()
        {
            return string.Empty;
        }

        public virtual bool GetFeedbackErrors(out string errors)
        {
            errors = string.Empty; return false;
        }

        public virtual void GetFeedbackInfo(ref List<string> infoList)
        {
        }

        public abstract ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween);
    }
}