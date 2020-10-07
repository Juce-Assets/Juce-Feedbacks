using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class FloatElement : Element
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] private float startValue = default;
        [SerializeField] [HideInInspector] private float endValue = default;

        [SerializeField] [HideInInspector] private float minValue = default;
        [SerializeField] [HideInInspector] private float maxValue = default;

        public bool UseStartValue => useStartValue;
        public float StartValue => startValue;
        public float EndValue => endValue;

        public float MinValue { get => minValue; set => minValue = value; }
        public float MaxValue { get => maxValue; set => maxValue = value; }
    }
}
