using System;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(CoordinatesSpaceElement))]
    public class CoordinatesSpaceElementCE : Editor
    {
        private CoordinatesSpaceElement CustomTarget => (CoordinatesSpaceElement)target;

        private SerializedProperty coordinatesSpaceProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(coordinatesSpaceProperty);

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(CustomTarget);
            }
        }

        private void GatherProperties()
        {
            coordinatesSpaceProperty = serializedObject.FindProperty("coordinatesSpace");
        }
    }
}
