using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(LoopProperty))]
    public class LoopPropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 1;

            SerializedProperty loopModeProperty = property.FindPropertyRelative("loopMode");

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes || (LoopMode)loopModeProperty.enumValueIndex == LoopMode.UntilManuallyStopped)
            {
                ++elementsCount;
            }

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes)
            {
                ++elementsCount;
            }

            return layoutHelper.GetHeightOfElements(elementsCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty loopModeProperty = property.FindPropertyRelative("loopMode");
            SerializedProperty loopResetModeProperty = property.FindPropertyRelative("loopResetMode");
            SerializedProperty loopsProperty = property.FindPropertyRelative("loops");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), loopModeProperty);

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes || (LoopMode)loopModeProperty.enumValueIndex == LoopMode.UntilManuallyStopped)
            {
                EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), loopResetModeProperty);
            }

            if ((LoopMode)loopModeProperty.enumValueIndex == LoopMode.XTimes)
            {
                EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), loopsProperty);
            }

            EditorGUI.EndProperty();
        }
    }
}