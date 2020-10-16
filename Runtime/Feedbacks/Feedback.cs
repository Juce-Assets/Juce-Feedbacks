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

        [SerializeField] [HideInInspector] protected List<Element> elements = new List<Element>();

        public IReadOnlyList<Element> Elements => elements;

        public bool Expanded { get => expanded; set => expanded = value; }
        public string UserData { get => userData; set => userData = value; }
        public bool Enabled { get => enabled; set => enabled = value; }

        public ExecuteResult ExecuteResult { get; set; }

        private void ClearElements()
        {
            elements.Clear();
        }

        protected T AddElement<T>(int id, string name) where T : Element
        {
            T elementInstance = ScriptableObject.CreateInstance<T>();

            elementInstance.Init(id, name);

            elements.Add(elementInstance);

            return elementInstance;
        }

        protected T GetElement<T>(int id) where T : Element
        {
            for(int i = 0; i < elements.Count; ++i)
            {
                Element currElement = elements[i];

                if (currElement.Id == id && currElement is T)
                {
                    return currElement as T;
                }
            }

            return null;
        }

        public void Init()
        {
            ClearElements();

            OnCreate();

            OnLink();
        }

        public void Init(Feedback copy)
        {
            ClearElements();

            foreach(Element element in copy.elements)
            {
                Element elementCopy = Instantiate(element) as Element;

                elements.Add(elementCopy);
            }

            OnLink();
        }

        public virtual string GetFeedbackTargetInfo() { return string.Empty; }
        public virtual bool GetFeedbackErrors(out string errors) { errors = string.Empty; return false; }
        public virtual string GetFeedbackInfo() { return string.Empty; }
        protected virtual void OnCreate() { }
        protected virtual void OnLink() { }

        public abstract ExecuteResult OnExecute(FlowContext context, SequenceTween sequenceTween);
    }
}
