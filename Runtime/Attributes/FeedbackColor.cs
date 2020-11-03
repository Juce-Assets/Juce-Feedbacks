using System;
using UnityEngine;

namespace Juce.Feedbacks
{
    public class FeedbackColor : Attribute
    {
        public Color Color { get; }

        public FeedbackColor(float r, float g, float b)
        {
            Color = new Color(r, g, b, 1.0f);
        }
    }
}