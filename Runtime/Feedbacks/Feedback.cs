using System;
using System.Collections.Generic;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public abstract class Feedback : ScriptableObject
    {
        [SerializeField] [HideInInspector] private bool expanded = true;
        [SerializeField] private string userData = default;

        [SerializeField] [HideInInspector] private bool enabled = true;

        public bool Expanded { get => expanded; set => expanded = value; }
        public string UserData { get => userData; set => userData = value; }
        public bool Enabled { get => enabled; set => enabled = value; }

        public ExecuteResult ExecuteResult { get; set; }

        public virtual string GetFeedbackTargetInfo() { return string.Empty; }
        public virtual bool GetFeedbackErrors(out string errors) { errors = string.Empty; return false; }
        public virtual string GetFeedbackInfo() { return string.Empty; }

        public abstract ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween);
    }
}
