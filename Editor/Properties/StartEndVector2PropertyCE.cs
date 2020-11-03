using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndVector2Property))]
    public class StartEndVector2PropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 4;

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");

            if (useStartValueProperty.boolValue)
            {
                elementsCount += 3;
            }

            return layoutHelper.GetHeightOfElements(elementsCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");
            SerializedProperty useStartXProperty = property.FindPropertyRelative("useStartX");
            SerializedProperty useStartYProperty = property.FindPropertyRelative("useStartY");
            SerializedProperty useEndXProperty = property.FindPropertyRelative("useEndX");
            SerializedProperty useEndYProperty = property.FindPropertyRelative("useEndY");
            SerializedProperty startValueXProperty = property.FindPropertyRelative("startValueX");
            SerializedProperty startValueYProperty = property.FindPropertyRelative("startValueY");
            SerializedProperty endValueXProperty = property.FindPropertyRelative("endValueX");
            SerializedProperty endValueYProperty = property.FindPropertyRelative("endValueY");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                EditorGUI.LabelField(layoutHelper.NextVerticalRect(), "Start");

                Rect totalStartXRect = layoutHelper.NextVerticalRect();
                Rect useStartXRect = new Rect(totalStartXRect.x + 10, totalStartXRect.y, 10, totalStartXRect.height);
                Rect startXRect = new Rect(totalStartXRect.x + 30, totalStartXRect.y, totalStartXRect.width - 30, totalStartXRect.height);

                Rect totalStartYRect = layoutHelper.NextVerticalRect();
                Rect useStartYRect = new Rect(totalStartYRect.x + 10, totalStartYRect.y, 10, totalStartYRect.height);
                Rect startYRect = new Rect(totalStartYRect.x + 30, totalStartYRect.y, totalStartYRect.width - 30, totalStartYRect.height);

                EditorGUI.PropertyField(useStartXRect, useStartXProperty, new GUIContent(""));

                EditorGUI.BeginDisabledGroup(!useStartXProperty.boolValue);
                {
                    EditorGUI.PropertyField(startXRect, startValueXProperty, new GUIContent("X"));
                }
                EditorGUI.EndDisabledGroup();

                EditorGUI.PropertyField(useStartYRect, useStartYProperty, new GUIContent(""));

                EditorGUI.BeginDisabledGroup(!useStartYProperty.boolValue);
                {
                    EditorGUI.PropertyField(startYRect, startValueYProperty, new GUIContent("Y"));
                }
                EditorGUI.EndDisabledGroup();
            }

            EditorGUI.LabelField(layoutHelper.NextVerticalRect(), "End");

            Rect totalEndXRect = layoutHelper.NextVerticalRect();
            Rect useEndXRect = new Rect(totalEndXRect.x + 10, totalEndXRect.y, 10, totalEndXRect.height);
            Rect endXRect = new Rect(totalEndXRect.x + 30, totalEndXRect.y, totalEndXRect.width - 30, totalEndXRect.height);

            Rect totalEndYRect = layoutHelper.NextVerticalRect();
            Rect useEndYRect = new Rect(totalEndYRect.x + 10, totalEndYRect.y, 10, totalEndYRect.height);
            Rect endYRect = new Rect(totalEndYRect.x + 30, totalEndYRect.y, totalEndYRect.width - 30, totalEndYRect.height);

            EditorGUI.PropertyField(useEndXRect, useEndXProperty, new GUIContent(""));

            EditorGUI.BeginDisabledGroup(!useEndXProperty.boolValue);
            {
                EditorGUI.PropertyField(endXRect, endValueXProperty, new GUIContent("X"));
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.PropertyField(useEndYRect, useEndYProperty, new GUIContent(""));

            EditorGUI.BeginDisabledGroup(!useEndYProperty.boolValue);
            {
                EditorGUI.PropertyField(endYRect, endValueYProperty, new GUIContent("Y"));
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }
    }
}