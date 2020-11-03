using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndUnitFloatProperty
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] [Range(0, 1)] private float startValue = default;
        [SerializeField] [HideInInspector] [Range(0, 1)] private float endValue = default;

        public bool UseStartValue { get => useStartValue; set => useStartValue = value; }

        public float StartValue { get => startValue; set => startValue = Mathf.Max(0, value); }
        public float EndValue { get => endValue; set => endValue = Mathf.Max(0, value); }
    }
}