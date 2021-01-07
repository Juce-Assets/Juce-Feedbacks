using UnityEngine;

namespace Juce.Feedbacks.Example8
{
    public class ChestShaker : MonoBehaviour
    {
        [SerializeField] private FeedbacksPlayer shakeFeedbackPlayer = default;

        private void OnMouseDown()
        {
            if (!shakeFeedbackPlayer.IsPlaying)
            {
                shakeFeedbackPlayer.Play();
            }
        }
    }
}