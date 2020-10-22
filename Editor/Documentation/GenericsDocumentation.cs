using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    public static class GenericsDocumentation
    {
        public static void SameTimeSequencingDocumentation()
        {
            GUILayout.Label("It will play at the same time as the other feedbacks of the sequence.", EditorStyles.wordWrappedLabel);
        }

        public static void EasingDocumentation()
        {
            GUILayout.Label("- Use Animation Curve: Allows for the choosing of a predefined Easing, or to use an Animation Curve", EditorStyles.wordWrappedLabel);
            GUILayout.Label("- Ease: (If enabled) Predefined easing used to interpolate", EditorStyles.wordWrappedLabel);
            GUILayout.Label("- Animation Curve: (If enabled) To set custom easings", EditorStyles.wordWrappedLabel);
        }

        public static void LoopDocumentation()
        {
            GUILayout.Label("- Loop:", EditorStyles.wordWrappedLabel);
            GUILayout.Label("   Disabled: Does not loop", EditorStyles.wordWrappedLabel);
            GUILayout.Label("   X Times: Loops an x number of times", EditorStyles.wordWrappedLabel);
            GUILayout.Label("   Until Manually Stoped: Loops until Complete or Kill is called on the feedback", EditorStyles.wordWrappedLabel);
        }

        public static void DelayDocumentation()
        {
            GUILayout.Label("- Delay: Delay to start the feedback", EditorStyles.wordWrappedLabel);
        }

        public static void DurationDocumentation()
        {
            GUILayout.Label("- Duration: Time it will take to reach the end", EditorStyles.wordWrappedLabel);
        }
    }
}
