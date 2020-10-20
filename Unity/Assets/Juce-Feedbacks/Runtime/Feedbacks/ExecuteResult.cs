using System;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class ExecuteResult
    {
        public Tween.Tween DelayTween { get; set; }
        public Tween.Tween ProgresTween { get; set; }
    }
}
