using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class BoolElement : Element
    {
        [SerializeField] [HideInInspector] private bool value = default;

        public bool Value => value;
    }
}
