using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndUnitFloatProperty
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] [Range(0, 1)] private float startValue = default;
        [SerializeField] [HideInInspector] [Range(0, 1)] private float endValue = default;

        public bool UseStartValue => useStartValue;
        public float StartValue => startValue;
        public float EndValue => endValue;
    }
}
