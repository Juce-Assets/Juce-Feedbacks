using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndVector3Property))]
    public class StartEndVector3PropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 5;

            SerializedProperty useStartValueProperty = property.FindPropertyRelative("useStartValue");

            if (useStartValueProperty.boolValue)
            {
                elementsCount += 4;
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
            SerializedProperty startValueXProperty = property.FindPropertyRelative("startValueX");
            SerializedProperty startValueYProperty = property.FindPropertyRelative("startValueY");
            SerializedProperty startValueZProperty = property.FindPropertyRelative("startValueZ");
            SerializedProperty endValueXProperty = property.FindPropertyRelative("endValueX");
            SerializedProperty endValueYProperty = property.FindPropertyRelative("endValueY");
            SerializedProperty endValueZProperty = property.FindPropertyRelative("endValueZ");

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

                Rect totalStartZRect = layoutHelper.NextVerticalRect();
                Rect useStartZRect = new Rect(totalStartZRect.x + 10, totalStartZRect.y, 10, totalStartZRect.height);
                Rect startZRect = new Rect(totalStartZRect.x + 30, totalStartZRect.y, totalStartZRect.width - 30, totalStartZRect.height);

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

                EditorGUI.PropertyField(useStartZRect, useStartZProperty, new GUIContent(""));

                EditorGUI.BeginDisabledGroup(!useStartZProperty.boolValue);
                {
                    EditorGUI.PropertyField(startZRect, startValueZProperty, new GUIContent("Z"));
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

            Rect totalEndZRect = layoutHelper.NextVerticalRect();
            Rect useEndZRect = new Rect(totalEndZRect.x + 10, totalEndZRect.y, 10, totalEndZRect.height);
            Rect endZRect = new Rect(totalEndZRect.x + 30, totalEndZRect.y, totalEndZRect.width - 30, totalEndZRect.height);

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

            EditorGUI.PropertyField(useEndZRect, useEndZProperty, new GUIContent(""));

            EditorGUI.BeginDisabledGroup(!useEndZProperty.boolValue);
            {
                EditorGUI.PropertyField(endZRect, endValueZProperty, new GUIContent("Z"));
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }
    }
}
