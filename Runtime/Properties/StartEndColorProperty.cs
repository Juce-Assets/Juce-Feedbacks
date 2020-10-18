using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndColorProperty
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] private bool useStartColor = default;
        [SerializeField] [HideInInspector] private bool useStartAlpha = default;
        [SerializeField] [HideInInspector] private bool useEndColor = default;
        [SerializeField] [HideInInspector] private bool useEndAlpha = default;
        [SerializeField] [HideInInspector] [ColorUsage(false)] private Color startColor = Color.white;
        [SerializeField] [HideInInspector] [Range(0, 1)] private float startAlpha = default;
        [SerializeField] [HideInInspector] [ColorUsage(false)] private Color endColor = Color.white;
        [SerializeField] [HideInInspector] [Range(0, 1)] private float endAlpha = default;

        public bool UseStartValue => useStartValue;
        public bool UseStartColor => useStartColor;
        public bool UseStartAlpha => useStartAlpha;
        public bool UseEndColor => useEndColor;
        public bool UseEndAlpha => useEndAlpha;
        public Color StartColor => startColor;
        public float StartAlpha => startAlpha;
        public Color EndColor => endColor;
        public float EndAlpha => endAlpha;
    }
}
