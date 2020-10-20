using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(LoopProperty))]
    public class LoopPropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty loopModeProperty = property.FindPropertyRelative("loopMode");
            SerializedProperty loopResetModeProperty = property.FindPropertyRelative("loopResetMode");
            SerializedProperty loopsProperty = property.FindPropertyRelative("loops");

            EditorGUI.PropertyField(position, loopModeProperty);

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes || (LoopMode)loopModeProperty.enumValueIndex == LoopMode.UntilManuallyStoped)
            {
                EditorGUILayout.PropertyField(loopResetModeProperty);
            }

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes)
            {
                EditorGUILayout.PropertyField(loopsProperty);
            }

            EditorGUI.EndProperty();
        }
    }
}
