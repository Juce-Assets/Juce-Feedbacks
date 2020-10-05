using System;
using System.Collections.Generic;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public abstract class Feedback : ScriptableObject
    {
        [SerializeField] [HideInInspector] private bool expanded = default;
        [SerializeField] private string userData = default;

        [SerializeField] [HideInInspector] private bool enabled = true;
        [SerializeField] [Min(0)] private float delay = default;

        [SerializeField] [HideInInspector] protected List<FeedbackElement> elements = new List<FeedbackElement>();

        public IReadOnlyList<FeedbackElement> Elements => elements;

        public bool Expanded { get => expanded; set => expanded = value; }
        public string UserData { get => userData; set => userData = value; }
        public bool Enabled { get => enabled; set => enabled = value; }
        public float Delay => delay;

        protected T AddElement<T>(string name) where T : FeedbackElement
        {
            T elementInstance = ScriptableObject.CreateInstance<T>();

            elementInstance.Init(name);

            elements.Add(elementInstance);

            return elementInstance;
        }

        public virtual void OnCreate() { }

        public abstract void OnExectue(SequenceTween sequenceTween);
    }
}
