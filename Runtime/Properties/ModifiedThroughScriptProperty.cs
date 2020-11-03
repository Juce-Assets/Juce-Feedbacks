using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class ModifiedThroughScriptProperty
    {
        [SerializeField] [HideInInspector] private bool modifiedThroughScript = default;
        [SerializeField] [HideInInspector] private string feedbackId = default;

        public bool ModifiedThroughScript => modifiedThroughScript;
        public string FeedbackId => feedbackId;
    }
}