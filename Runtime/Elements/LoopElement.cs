using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class LoopElement : Element
    {
        [SerializeField] [HideInInspector] private LoopMode loopMode = LoopMode.Disabled;
        [SerializeField] [HideInInspector] private ResetMode loopResetMode = ResetMode.Restart;
        [SerializeField] [HideInInspector] [Min(0)] private int loops = default;

        public int Loops => loops;

        public void SetLoop(Tween.Tween tween)
        {
            switch(loopMode)
            {
                case LoopMode.XTimes:
                    {
                        tween.SetLoops(loops, loopResetMode);
                    }
                    break;

                case LoopMode.UntilManuallyStoped:
                    {
                        tween.SetLoops(int.MaxValue, loopResetMode);
                    }
                    break;
            }
        }
    }
}
