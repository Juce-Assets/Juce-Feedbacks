using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class StartEndTransformVector3Property
    {
        [SerializeField] [HideInInspector] private bool useStartValue = default;
        [SerializeField] [HideInInspector] private bool useStartX = default;
        [SerializeField] [HideInInspector] private bool useStartY = default;
        [SerializeField] [HideInInspector] private bool useStartZ = default;
        [SerializeField] [HideInInspector] private bool useEndX = default;
        [SerializeField] [HideInInspector] private bool useEndY = default;
        [SerializeField] [HideInInspector] private bool useEndZ = default;
        [SerializeField] [HideInInspector] private Transform startValue = default;
        [SerializeField] [HideInInspector] private Transform endValue = default;

        public bool UseStartValue { get => useStartValue; set => useStartValue = value; }
        public bool UseStartX { get => useStartX; set => useStartX = value; }
        public bool UseStartY { get => useStartY; set => useStartY = value; }
        public bool UseStartZ { get => useStartZ; set => useStartZ = value; }
        public bool UseEndX { get => useEndX; set => useEndX = value; }
        public bool UseEndY { get => useEndY; set => useEndY = value; }
        public bool UseEndZ { get => useEndZ; set => useEndZ = value; }
        public Transform StartValue { get => startValue; set => startValue = value; }
        public Transform EndValue { get => endValue; set => endValue = value; }
    }
}