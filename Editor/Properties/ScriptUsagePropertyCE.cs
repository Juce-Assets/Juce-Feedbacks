using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(ScriptUsageProperty))]
    public class ScriptUsagePropertyCE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty usedByScriptProperty = property.FindPropertyRelative("usedByScript");
            SerializedProperty idUsedByScriptProperty = property.FindPropertyRelative("idUsedByScript");

            if (!usedByScriptProperty.boolValue)
            {
                EditorGUI.PropertyField(position, usedByScriptProperty);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUI.PropertyField(position, usedByScriptProperty);

                    EditorGUILayout.PropertyField(idUsedByScriptProperty);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.EndProperty();
        }
    }
}
