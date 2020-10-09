using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class ColorElement : Element
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] private Color startValue = Color.white;
        [SerializeField] [HideInInspector] private Color endValue = Color.white;

        public bool UseStartValue => useStartValue;
        public Color StartValue => startValue;
        public Color EndValue => endValue;
    }
}
