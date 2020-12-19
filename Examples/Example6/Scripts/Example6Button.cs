using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.Feedbacks.Example6
{
    public class Example6Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private FeedbacksPlayer pointerDownFeedback = default;
        [SerializeField] private FeedbacksPlayer pointerUpFeedback = default;

        public void OnPointerDown(PointerEventData eventData)
        {
            pointerUpFeedback.Kill();
            pointerDownFeedback.Play();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pointerDownFeedback.Kill();
            pointerUpFeedback.Play();
        }
    }
}