using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks.Example7
{
    public class Example7 : MonoBehaviour
    {
        [SerializeField] private List<ButtonExample7> buttons = default;

        [SerializeField] private FeedbacksPlayer selectFeedback = default;
        [SerializeField] private FeedbacksPlayer deselectFeedback = default;

        private GameObject selectedButton = default;

        // Start is called before the first frame update
        private void Start()
        {
            for (int i = 0; i < buttons.Count; ++i)
            {
                ButtonExample7 currButton = buttons[i];

                currButton.OnClick += () =>
                {
                    SelectButton(currButton.gameObject);
                };
            }

            SelectButton(buttons[0].gameObject);
        }

        private void SelectButton(GameObject button)
        {
            if (button == selectedButton)
            {
                return;
            }

            if (selectedButton != null)
            {
                ImageColorFeedback deselectImageColorFeedback = deselectFeedback.GetFeedback<ImageColorFeedback>("color");
                TransformScaleFeedback deselectScaleFeedback = deselectFeedback.GetFeedback<TransformScaleFeedback>("scale");

                deselectImageColorFeedback.Target = selectedButton.GetComponent<Image>();
                deselectScaleFeedback.Target = selectedButton.transform;

                deselectFeedback.Play();
            }

            selectedButton = button;

            TransformPositionFeedback selectPositionFeedback = selectFeedback.GetFeedback<TransformPositionFeedback>("position");
            ImageColorFeedback selectImageColorFeedback = selectFeedback.GetFeedback<ImageColorFeedback>("color");
            TransformScaleFeedback selectScaleFeedback = selectFeedback.GetFeedback<TransformScaleFeedback>("scale");

            selectPositionFeedback.Value.EndValueX = button.transform.position.x;
            selectImageColorFeedback.Target = button.GetComponent<Image>();
            selectScaleFeedback.Target = button.transform;

            selectFeedback.Play();
        }
    }
}