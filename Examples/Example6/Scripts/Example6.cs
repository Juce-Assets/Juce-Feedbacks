using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks.Example6
{
    public class Example6 : MonoBehaviour
    {
        [SerializeField] private Button button = default;
        [SerializeField] private ClickDetector clickDetector = default;
        [SerializeField] private FeedbacksPlayer showPanelFeedback = default;
        [SerializeField] private FeedbacksPlayer hidePanelFeedback = default;

        private bool showingPanel;

        private void Awake()
        {
            clickDetector.gameObject.SetActive(false);

            button.onClick.AddListener(() =>
            {
                showPanelFeedback.Play(() =>
                {
                    clickDetector.gameObject.SetActive(true);
                });
            });

            clickDetector.OnClick += () =>
            {
                clickDetector.gameObject.SetActive(false);

                hidePanelFeedback.Play();
            };
        }
    }
}