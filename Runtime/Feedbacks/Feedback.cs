using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public abstract class Feedback : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float delay = default;

        public float Delay
        {
            get { return delay; }
        }

        public abstract void OnExectue(SequenceTween sequenceTween);
    }
}
