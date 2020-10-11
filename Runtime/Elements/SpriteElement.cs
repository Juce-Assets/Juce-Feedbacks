using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class SpriteElement : Element
    {
        [SerializeField] [HideInInspector] private Sprite value = default;

        public Sprite Value => value;
    }
}
