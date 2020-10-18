using System;

namespace Juce.Feedbacks
{
    public static class ErrorUtils
    {
        public static bool CheckTargetNull(object target, out string errorMessage)
        {
            if(target == null)
            {
                errorMessage = "Target is null";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
