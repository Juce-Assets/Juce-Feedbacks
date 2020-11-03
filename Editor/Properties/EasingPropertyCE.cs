using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(EasingProperty))]
    public class EasingPropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return layoutHelper.GetHeightOfElements(2);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useAnimationCurveProperty = property.FindPropertyRelative("useAnimationCurve");
            SerializedProperty easingProperty = property.FindPropertyRelative("easing");
            SerializedProperty animationCurveEasingProperty = property.FindPropertyRelative("animationCurveEasing");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), useAnimationCurveProperty);

            if (!useAnimationCurveProperty.boolValue)
            {
                EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), easingProperty, new GUIContent("Ease"));
            }
            else
            {
                EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), animationCurveEasingProperty, new GUIContent("Ease"));
            }

            EditorGUI.EndProperty();
        }
    }
}