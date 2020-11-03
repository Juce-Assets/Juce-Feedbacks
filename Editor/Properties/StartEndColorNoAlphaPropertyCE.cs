using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndColorNoAlphaProperty))]
    public class StartEndColorNoAlphaPropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 2;

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");

            if (useStartValueProperty.boolValue)
            {
                ++elementsCount;
            }

            return layoutHelper.GetHeightOfElements(elementsCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty startColorProperty = property.FindPropertyRelative("startColor");
            SerializedProperty endColorProperty = property.FindPropertyRelative("endColor");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), startColorProperty);
            }

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), endColorProperty);

            EditorGUI.EndProperty();
        }
    }
}