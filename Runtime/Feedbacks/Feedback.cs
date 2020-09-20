using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void OnExectue(SequenceTween sequenceTween);
    }
}
