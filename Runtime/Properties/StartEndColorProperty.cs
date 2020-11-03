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

        public bool UseStartValue { get => useStartValue; set => useStartValue = value; }
        public bool UseStartColor { get => useStartColor; set => useStartColor = value; }
        public bool UseStartAlpha { get => useStartAlpha; set => useStartAlpha = value; }
        public bool UseEndColor { get => useEndColor; set => useEndColor = value; }
        public bool UseEndAlpha { get => useEndAlpha; set => useEndAlpha = value; }
        public Color StartColor { get => startColor; set => startColor = value; }
        public float StartAlpha { get => startAlpha; set => startAlpha = Mathf.Max(0, value); }
        public Color EndColor { get => endColor; set => endColor = value; }
        public float EndAlpha { get => endAlpha; set => endAlpha = Mathf.Max(0, value); }
    }
}