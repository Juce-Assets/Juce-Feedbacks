using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class DurationElement : Element
    {
        [SerializeField] [HideInInspector] private bool completeInstantly = default;
        [SerializeField] [HideInInspector] private float duration = 1.0f;

        public bool CompleteInstantly => completeInstantly;
        public float Duration => duration;
    }
}
