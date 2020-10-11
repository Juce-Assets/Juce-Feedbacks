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
        [SerializeField] [HideInInspector] [Min(0)] private float delay = default;

        [SerializeField] [HideInInspector] protected List<Element> elements = new List<Element>();

        public IReadOnlyList<Element> Elements => elements;

        public bool Expanded { get => expanded; set => expanded = value; }
        public string UserData { get => userData; set => userData = value; }
        public bool Enabled { get => enabled; set => enabled = value; }
        public float Delay => delay;

        protected T AddElement<T>(string name) where T : Element
        {
            T elementInstance = ScriptableObject.CreateInstance<T>();

            elementInstance.Init(name);

            elements.Add(elementInstance);

            return elementInstance;
        }

        public void Init()
        {
            OnCreate();
        }

        public virtual string GetFeedbackTargetInfo() { return string.Empty; }
        public virtual bool GetFeedbackErrors(out string errors) { errors = string.Empty; return false; }
        public virtual string GetFeedbackInfo() { return string.Empty; }
        protected virtual void OnCreate() { }

        public abstract void OnExectue(SequenceTween sequenceTween);
    }
}
