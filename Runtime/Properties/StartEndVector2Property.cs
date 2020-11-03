using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndVector2Property
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] private bool useStartX = default;
        [SerializeField] [HideInInspector] private bool useStartY = default;
        [SerializeField] [HideInInspector] private bool useEndX = default;
        [SerializeField] [HideInInspector] private bool useEndY = default;
        [SerializeField] [HideInInspector] private float startValueX = default;
        [SerializeField] [HideInInspector] private float startValueY = default;
        [SerializeField] [HideInInspector] private float endValueX = default;
        [SerializeField] [HideInInspector] private float endValueY = default;

        public bool UseStartValue { get => useStartValue; set => useStartValue = value; }
        public bool UseStartX { get => useStartX; set => useStartX = value; }
        public bool UseStartY { get => useStartY; set => useStartY = value; }
        public bool UseEndX { get => useEndX; set => useEndX = value; }
        public bool UseEndY { get => useEndY; set => useEndY = value; }
        public float StartValueX { get => startValueX; set => startValueX = value; }
        public float StartValueY { get => startValueY; set => startValueY = value; }
        public float EndValueX { get => endValueX; set => endValueX = value; }
        public float EndValueY { get => endValueY; set => endValueY = value; }
    }
}