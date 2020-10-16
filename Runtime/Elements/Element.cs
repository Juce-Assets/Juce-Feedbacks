using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class Element : ScriptableObject
    {
        [SerializeField] private int id = default;
        [SerializeField] private string elementName = default;

        public int Id => id;
        public string ElementName => elementName;

        public void Init(int id, string elementName)
        {
            this.id = id;
            this.elementName = elementName;
        }
    }
}
