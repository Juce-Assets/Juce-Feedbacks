using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks.Example6
{
    public class Example6 : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button button = default;

        [SerializeField] private FeedbacksPlayer showFeedback = default;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                showFeedback.Play();
            });
        }
    }
}