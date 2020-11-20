using Juce.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example9 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FeedbacksPlayer feedbacksPlayer = JuceFeedbacks.GetFeedbacksPlayer("testFeedback");

        feedbacksPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
