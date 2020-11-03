namespace Juce.Feedbacks
{
    public static class LoopUtils
    {
        public static void SetLoop(Tween.Tween tween, LoopProperty loopProperty)
        {
            switch (loopProperty.LoopMode)
            {
                case LoopMode.XTimes:
                    {
                        tween.SetLoops(loopProperty.Loops, loopProperty.LoopResetMode);
                    }
                    break;

                case LoopMode.UntilManuallyStoped:
                    {
                        tween.SetLoops(int.MaxValue, loopProperty.LoopResetMode);
                    }
                    break;
            }
        }
    }
}