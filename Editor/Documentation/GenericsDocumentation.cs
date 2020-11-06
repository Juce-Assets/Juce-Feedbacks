using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    public static class GenericsDocumentation
    {
        public static void EasingDocumentation()
        {
            GUILayout.Label("- Use Animation Curve: allows for the choosing of a predefined Easing, or to use an Animation Curve", EditorStyles.wordWrappedLabel);
            GUILayout.Label("- Ease: (if enabled) predefined easing used to interpolate", EditorStyles.wordWrappedLabel);
            GUILayout.Label("- Animation Curve: (if enabled) to set custom easings", EditorStyles.wordWrappedLabel);
        }

        public static void LoopDocumentation()
        {
            GUILayout.Label("- Loop:", EditorStyles.wordWrappedLabel);
            GUILayout.Label("   Disabled: does not loop", EditorStyles.wordWrappedLabel);
            GUILayout.Label("   X Times: loops an x number of times", EditorStyles.wordWrappedLabel);
            GUILayout.Label("   Until Manually Stoped: loops until Complete or Kill is called on the feedback", EditorStyles.wordWrappedLabel);
        }

        public static void DelayDocumentation()
        {
            GUILayout.Label("- Delay: delay to start the feedback", EditorStyles.wordWrappedLabel);
        }

        public static void DurationDocumentation()
        {
            GUILayout.Label("- Duration: time it will take to reach the final value", EditorStyles.wordWrappedLabel);
        }
    }
}