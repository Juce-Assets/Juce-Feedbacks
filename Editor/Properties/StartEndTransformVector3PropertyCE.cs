using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndTransformVector3Property))]
    public class StartEndTransformVector3PropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 6;

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");

            if (useStartValueProperty.boolValue)
            {
                elementsCount += 5;
            }

            return layoutHelper.GetHeightOfElements(elementsCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty useStartXProperty = property.FindPropertyRelative("useStartX");
            SerializedProperty useStartYProperty = property.FindPropertyRelative("useStartY");
            SerializedProperty useStartZProperty = property.FindPropertyRelative("useStartZ");
            SerializedProperty useEndXProperty = property.FindPropertyRelative("useEndX");
            SerializedProperty useEndYProperty = property.FindPropertyRelative("useEndY");
            SerializedProperty useEndZProperty = property.FindPropertyRelative("useEndZ");
            SerializedProperty startValueProperty = property.FindPropertyRelative("startValue");
            SerializedProperty endValueProperty = property.FindPropertyRelative("endValue");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                EditorGUI.LabelField(layoutHelper.NextVerticalRect(), "Start");

                Rect totalStartValueRect = layoutHelper.NextVerticalRect();
                Rect startValueRect = new Rect(totalStartValueRect.x + 10, totalStartValueRect.y, totalStartValueRect.width - 10, totalStartValueRect.height);

                Rect totalStartXRect = layoutHelper.NextVerticalRect();
                Rect startXRect = new Rect(totalStartXRect.x + 10, totalStartXRect.y, totalStartXRect.width - 10, totalStartXRect.height);

                Rect totalStartYRect = layoutHelper.NextVerticalRect();
                Rect startYRect = new Rect(totalStartYRect.x + 10, totalStartYRect.y, totalStartYRect.width - 10, totalStartYRect.height);

                Rect totalStartZRect = layoutHelper.NextVerticalRect();
                Rect startZRect = new Rect(totalStartZRect.x + 10, totalStartZRect.y, totalStartZRect.width - 10, totalStartZRect.height);

                EditorGUI.PropertyField(startValueRect, startValueProperty);

                EditorGUI.PropertyField(startXRect, useStartXProperty, new GUIContent("X"));
                EditorGUI.PropertyField(startYRect, useStartYProperty, new GUIContent("Y"));
                EditorGUI.PropertyField(startZRect, useStartZProperty, new GUIContent("Z"));
            }

            EditorGUI.LabelField(layoutHelper.NextVerticalRect(), "End");

            Rect totalEndValueRect = layoutHelper.NextVerticalRect();
            Rect endValueRect = new Rect(totalEndValueRect.x + 10, totalEndValueRect.y, totalEndValueRect.width - 10, totalEndValueRect.height);

            Rect totalEndXRect = layoutHelper.NextVerticalRect();
            Rect endXRect = new Rect(totalEndXRect.x + 10, totalEndXRect.y, totalEndXRect.width - 10, totalEndXRect.height);

            Rect totalEndYRect = layoutHelper.NextVerticalRect();
            Rect endYRect = new Rect(totalEndYRect.x + 10, totalEndYRect.y, totalEndYRect.width - 10, totalEndYRect.height);

            Rect totalEndZRect = layoutHelper.NextVerticalRect();
            Rect endZRect = new Rect(totalEndZRect.x + 10, totalEndZRect.y, totalEndZRect.width - 10, totalEndZRect.height);

            EditorGUI.PropertyField(endValueRect, endValueProperty);

            EditorGUI.PropertyField(endXRect, useStartXProperty, new GUIContent("X"));
            EditorGUI.PropertyField(endYRect, useStartYProperty, new GUIContent("Y"));
            EditorGUI.PropertyField(endZRect, useStartZProperty, new GUIContent("Z"));

            EditorGUI.EndProperty();
        }
    }
}