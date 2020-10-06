using System;
using UnityEngine;
using Juce.Tween;

namespace Juce.Feedbacks
{
    public class CoordinatesSpaceElement : Element
    {
        [SerializeField] [HideInInspector] private CoordinatesSpace coordinatesSpace = default;

        public CoordinatesSpace CoordinatesSpace => coordinatesSpace;
    }
}
