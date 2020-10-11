using System;
using UnityEditor;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(AudioClipElement))]
    public class AudioClipElementCE : Editor
    {
        private AudioClipElement CustomTarget => (AudioClipElement)target;

        private SerializedProperty valueProperty;
        private SerializedProperty oneShotProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.PropertyField(valueProperty);

                if (valueProperty.objectReferenceValue != null)
                {
                    EditorGUILayout.PropertyField(oneShotProperty);
                }
            }

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            valueProperty = serializedObject.FindProperty("value");
            oneShotProperty = serializedObject.FindProperty("oneShot");
        }
    }
}
