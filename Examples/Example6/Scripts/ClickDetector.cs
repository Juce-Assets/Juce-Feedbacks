using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.Feedbacks.Example6
{
    public class ClickDetector : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnClick;

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            OnClick?.Invoke();
        }
    }
}