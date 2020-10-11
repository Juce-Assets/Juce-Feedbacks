using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class AudioClipElement : Element
    {
        [SerializeField] [HideInInspector] private AudioClip value = default;
        [SerializeField] [HideInInspector] private bool oneShot = default;

        public AudioClip Value => value;
        public bool OneShot => value;
    }
}
