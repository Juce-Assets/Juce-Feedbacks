using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class LoopProperty
    {
        [SerializeField] [HideInInspector] private LoopMode loopMode = LoopMode.Disabled;
        [SerializeField] [HideInInspector] private ResetMode loopResetMode = ResetMode.Restart;
        [SerializeField] [HideInInspector] [Min(0)] private int loops = default;

        public LoopMode LoopMode => loopMode;
        public ResetMode LoopResetMode => loopResetMode;
        public int Loops => loops;
    }
}
