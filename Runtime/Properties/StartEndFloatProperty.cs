using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndFloatProperty
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] private float startValue = default;
        [SerializeField] [HideInInspector] private float endValue = default;

        public bool UseStartValue { get => useStartValue; set => useStartValue = value; }
        public float StartValue { get => startValue; set => startValue = value; }
        public float EndValue { get => endValue; set => endValue = value; }
    }
}