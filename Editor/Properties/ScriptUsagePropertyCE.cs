using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(ScriptUsageProperty))]
    public class ScriptUsagePropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 1;

            SerializedProperty usedByScriptProperty = property.FindPropertyRelative("usedByScript");

            if (usedByScriptProperty.boolValue)
            {
                ++elementsCount;
            }

            return layoutHelper.GetHeightOfElements(elementsCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty usedByScriptProperty = property.FindPropertyRelative("usedByScript");
            SerializedProperty idUsedByScriptProperty = property.FindPropertyRelative("idUsedByScript");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), usedByScriptProperty);

            if (usedByScriptProperty.boolValue)
            {
                EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), idUsedByScriptProperty);
            }

            EditorGUI.EndProperty();
        }
    }
}