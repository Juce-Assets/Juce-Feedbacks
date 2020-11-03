using UnityEngine;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class ScriptUsageProperty
    {
        [SerializeField] [HideInInspector] private bool usedByScript = default;
        [SerializeField] [HideInInspector] private string idUsedByScript = default;

        public bool UsedByScript => usedByScript;
        public string IdUsedByScript => idUsedByScript;
    }
}