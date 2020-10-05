using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class Vector3Element : FeedbackElement
    {
        [SerializeField] [HideInInspector] private bool useStartPosition = default;
        [SerializeField] [HideInInspector] private bool useStartX = default;
        [SerializeField] [HideInInspector] private bool useStartY = default;
        [SerializeField] [HideInInspector] private bool useStartZ = default;
        [SerializeField] [HideInInspector] private bool useEndX = default;
        [SerializeField] [HideInInspector] private bool useEndY = default;
        [SerializeField] [HideInInspector] private bool useEndZ = default;
        [SerializeField] [HideInInspector] private float startPositionX = default;
        [SerializeField] [HideInInspector] private float startPositionY = default;
        [SerializeField] [HideInInspector] private float startPositionZ = default;
        [SerializeField] [HideInInspector] private float endPositionX = default;
        [SerializeField] [HideInInspector] private float endPositionY = default;
        [SerializeField] [HideInInspector] private float endPositionZ = default;

        public bool UseStartPosition => useStartPosition;
        public bool UseStartX => useStartX;
        public bool UseStartY => useStartY;
        public bool UseStartZ => useStartZ;
        public bool UseEndX => useEndX;
        public bool UseEndY => useEndY;
        public bool UseEndZ => useEndZ;
        public float StartPositionX => startPositionX;
        public float StartPositionY => startPositionY;
        public float StartPositionZ => startPositionZ;
        public float EndPositionX => endPositionX;
        public float EndPositionY => endPositionY;
        public float EndPositionZ => endPositionZ;
    }
}
