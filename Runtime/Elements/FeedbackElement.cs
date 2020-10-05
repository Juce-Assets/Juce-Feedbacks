using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class FeedbackElement : ScriptableObject
    {
        [SerializeField] private string elementName = default;

        public string ElementName => elementName;

        public void Init(string elementName)
        {
            this.elementName = elementName;
        }
    }
}
