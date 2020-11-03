using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(StartEndColorProperty))]
    public class StartEndColorPropertyCE : PropertyDrawer
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
            SerializedProperty useStartColorProperty = property.FindPropertyRelative("useStartColor");
            SerializedProperty useStartAlphaProperty = property.FindPropertyRelative("useStartAlpha");
            SerializedProperty useEndColorProperty = property.FindPropertyRelative("useEndColor");
            SerializedProperty useEndAlphaProperty = property.FindPropertyRelative("useEndAlpha");
            SerializedProperty startColorProperty = property.FindPropertyRelative("startColor");
            SerializedProperty startAlphaProperty = property.FindPropertyRelative("startAlpha");
            SerializedProperty endColorProperty = property.FindPropertyRelative("endColor");
            SerializedProperty endAlphaProperty = property.FindPropertyRelative("endAlpha");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), useStartValueProperty);

            if (useStartValueProperty.boolValue)
            {
                EditorGUI.LabelField(layoutHelper.NextVerticalRect(), "Start");

                Rect totalStartColorRect = layoutHelper.NextVerticalRect();
                Rect useStartColorRect = new Rect(totalStartColorRect.x + 10, totalStartColorRect.y, 10, totalStartColorRect.height);
                Rect startColorRect = new Rect(totalStartColorRect.x + 30, totalStartColorRect.y, totalStartColorRect.width - 30, totalStartColorRect.height);

                Rect totalStartAlphaRect = layoutHelper.NextVerticalRect();
                Rect useStartAlphaRect = new Rect(totalStartAlphaRect.x + 10, totalStartAlphaRect.y, 10, totalStartAlphaRect.height);
                Rect startAlphaRect = new Rect(totalStartAlphaRect.x + 30, totalStartAlphaRect.y, totalStartAlphaRect.width - 30, totalStartAlphaRect.height);

                EditorGUI.PropertyField(useStartColorRect, useStartColorProperty, new GUIContent(""));

                EditorGUI.BeginDisabledGroup(!useStartColorProperty.boolValue);
                {
                    EditorGUI.PropertyField(startColorRect, startColorProperty, new GUIContent("Color"));
                }
                EditorGUI.EndDisabledGroup();

                EditorGUI.PropertyField(useStartAlphaRect, useStartAlphaProperty, new GUIContent(""));

                EditorGUI.BeginDisabledGroup(!useStartAlphaProperty.boolValue);
                {
                    EditorGUI.PropertyField(startAlphaRect, startAlphaProperty, new GUIContent("Alpha"));
                }
                EditorGUI.EndDisabledGroup();
            }

            EditorGUI.LabelField(layoutHelper.NextVerticalRect(), "End");

            Rect totalEndColorRect = layoutHelper.NextVerticalRect();
            Rect useEndColorRect = new Rect(totalEndColorRect.x + 10, totalEndColorRect.y, 10, totalEndColorRect.height);
            Rect endColorRect = new Rect(totalEndColorRect.x + 30, totalEndColorRect.y, totalEndColorRect.width - 30, totalEndColorRect.height);

            Rect totalEndAlphaRect = layoutHelper.NextVerticalRect();
            Rect useEndAlphaRect = new Rect(totalEndAlphaRect.x + 10, totalEndAlphaRect.y, 10, totalEndAlphaRect.height);
            Rect endAlphaRect = new Rect(totalEndAlphaRect.x + 30, totalEndAlphaRect.y, totalEndAlphaRect.width - 30, totalEndAlphaRect.height);

            EditorGUI.PropertyField(useEndColorRect, useEndColorProperty, new GUIContent(""));

            EditorGUI.BeginDisabledGroup(!useEndColorProperty.boolValue);
            {
                EditorGUI.PropertyField(endColorRect, endColorProperty, new GUIContent("Color"));
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.PropertyField(useEndAlphaRect, useEndAlphaProperty, new GUIContent(""));

            EditorGUI.BeginDisabledGroup(!useEndAlphaProperty.boolValue);
            {
                EditorGUI.PropertyField(endAlphaRect, endAlphaProperty, new GUIContent("Alpha"));
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }
    }
}