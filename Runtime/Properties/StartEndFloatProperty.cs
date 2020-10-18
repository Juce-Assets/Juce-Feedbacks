using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndFloatProperty
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] private float startValue = default;
        [SerializeField] [HideInInspector] private float endValue = default;

        public bool UseStartValue => useStartValue;
        public float StartValue => startValue;
        public float EndValue => endValue;
    }
}
