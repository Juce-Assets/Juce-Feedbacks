using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class TimingElement : Element
    {
        [SerializeField] [HideInInspector] private float delay = default;
        [SerializeField] [HideInInspector] private bool useDuration = true;
        [SerializeField] [HideInInspector] private float duration = 1.0f;

        public float Delay => delay;
        public bool UseDuration { get => useDuration; set => useDuration = value; }
        public float Duration => duration;
    }
}
