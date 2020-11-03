using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndColorNoAlphaProperty
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] [ColorUsage(false)] private Color startColor = Color.white;
        [SerializeField] [HideInInspector] [ColorUsage(false)] private Color endColor = Color.white;

        public bool UseStartValue => useStartValue;
        public Color StartColor => startColor;
        public Color EndColor => endColor;
    }
}