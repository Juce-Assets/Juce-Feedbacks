using Juce.Tween;
using Juce.Utils.Singletons;
using System.Collections.Generic;

namespace Juce.Feedbacks
{
    public class JuceFeedbacks : AutoStartMonoSingleton<JuceFeedbacks>
    {
        private readonly Dictionary<string, FeedbacksPlayer> feedbackPlayersUsedByScript = new Dictionary<string, FeedbacksPlayer>();

        public static float TimeScale { get => JuceTween.TimeScale; set => JuceTween.TimeScale = value; }

        internal bool RegisterFeedbacksPlayerUsedByScript(FeedbacksPlayer feedbacksPlayer)
        {
            if (!feedbacksPlayer.ScriptUsage.UsedByScript)
            {
                return false;
            }

            if (string.IsNullOrEmpty(feedbacksPlayer.ScriptUsage.IdUsedByScript))
            {
                UnityEngine.Debug.LogError($"Trying to register a {nameof(FeedbacksPlayer)}, but the Id Used By Script is empty. " +
                    $"Please set an id or untoggle Used By Script.", feedbacksPlayer);

                return false;
            }

            if (feedbackPlayersUsedByScript.ContainsKey(feedbacksPlayer.ScriptUsage.IdUsedByScript))
            {
                UnityEngine.Debug.LogError($"Trying to register a {nameof(FeedbacksPlayer)}, but the Id Used By Script " +
                    $"'{feedbacksPlayer.ScriptUsage.IdUsedByScript}' is already used by " +
                    $"another {nameof(FeedbacksPlayer)}. Please set an Id Used By Script that's unique.", feedbacksPlayer);

                return false;
            }

            feedbackPlayersUsedByScript.Add(feedbacksPlayer.ScriptUsage.IdUsedByScript, feedbacksPlayer);

            return true;
        }

        internal void UnregisterFeedbacksPlayerUsedByScript(string id)
        {
            feedbackPlayersUsedByScript.Remove(id);
        }

        /// <summary>
        /// Returns the <typeparamref name="FeedbacksPlayer"/> found, which has the Used By Script toggled, and the Id Used By Script defined on the editor.
        /// Returns null if not found.
        /// </summary>
        public static FeedbacksPlayer GetFeedbacksPlayer(string id)
        {
            Instance.feedbackPlayersUsedByScript.TryGetValue(id, out FeedbacksPlayer feedbacksPlayer);

            return feedbacksPlayer;
        }

        /// <summary>
        /// Outs the <typeparamref name="FeedbacksPlayer"/> found, which has the Used By Script toggled, and the Id Used By Script defined on the editor.
        /// Returns false if not found.
        /// </summary>
        public static bool TryGetFeedbacksPlayer(string id, out FeedbacksPlayer feedbacksPlayer)
        {
            return Instance.feedbackPlayersUsedByScript.TryGetValue(id, out feedbacksPlayer);
        }
    }
}