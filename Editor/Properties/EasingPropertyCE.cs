using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(EasingProperty))]
    public class EasingPropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useAnimationCurveProperty = property.FindPropertyRelative("useAnimationCurve");
            SerializedProperty easingProperty = property.FindPropertyRelative("easing");
            SerializedProperty animationCurveEasingProperty = property.FindPropertyRelative("animationCurveEasing");

            EditorGUI.PropertyField(position, useAnimationCurveProperty);

            if (!useAnimationCurveProperty.boolValue)
            {
                EditorGUILayout.PropertyField(easingProperty, new GUIContent("Ease"));
            }
            else
            {
                EditorGUILayout.PropertyField(animationCurveEasingProperty, new GUIContent("Ease"));
            }

            EditorGUI.EndProperty();
        }
    }
}
