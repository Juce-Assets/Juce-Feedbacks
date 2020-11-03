namespace Juce.Feedbacks
{
    public static class EasingUtils
    {
        public static void SetEasing(Tween.Tween tween, EasingProperty easingProperty)
        {
            if (!easingProperty.UseAnimationCurve)
            {
                tween.SetEase(easingProperty.Easing);
            }
            else
            {
                tween.SetEase(easingProperty.AnimationCurveEasing);
            }
        }
    }
}