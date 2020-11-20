using Juce.Utils.Singletons;
using System;
using System.Collections.Generic;

namespace Juce.Feedbacks
{
    public class JuceFeedbacks : AutoStartMonoSingleton<JuceFeedbacks>
    {
        private readonly Dictionary<string, FeedbacksPlayer> feedbackPlayersUsedByScript = new Dictionary<string, FeedbacksPlayer>();

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
        /// Returns a <typeparamref name="FeedbacksPlayer"/> which has the Used By Script toggle, and the Id Used By Script selected on the editor
        /// </summary>
        public FeedbacksPlayer GetFeedbacksPlayer(string id)
        {
            feedbackPlayersUsedByScript.TryGetValue(id, out FeedbacksPlayer feedbacksPlayer);

            return feedbacksPlayer;
        }
    }
}