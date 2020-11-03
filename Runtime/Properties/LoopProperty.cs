using Juce.Tween;
using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class LoopProperty
    {
        [SerializeField] [HideInInspector] private LoopMode loopMode = LoopMode.Disabled;
        [SerializeField] [HideInInspector] private ResetMode loopResetMode = ResetMode.RestartValues;
        [SerializeField] [HideInInspector] [Min(0)] private int loops = default;

        public LoopMode LoopMode { get => loopMode; set => loopMode = value; }
        public ResetMode LoopResetMode { get => loopResetMode; set => loopResetMode = value; }
        public int Loops { get => loops; set => loops = Mathf.Max(0, value); }
    }
}