using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomEditor(typeof(RendererMaterialPropertyElement))]
    public class RendererMaterialPropertyElementCE : Editor
    {
        private RendererMaterialPropertyElement CustomTarget => (RendererMaterialPropertyElement)target;

        private SerializedProperty rendererProperty;
        private SerializedProperty materialIndexProperty;
        private SerializedProperty materialPropertyTypeProperty;
        private SerializedProperty propertyProperty;

        private Material material;
        private int propertyIndex = -1;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(rendererProperty);

            if (rendererProperty.objectReferenceValue != null)
            {
                Renderer renderer = (Renderer)rendererProperty.objectReferenceValue;

                string[] materials = new string[renderer.sharedMaterials.Length];

                for (int i = 0; i < renderer.sharedMaterials.Length; ++i)
                {
                    materials[i] = renderer.sharedMaterials[i].name;
                }

                materialIndexProperty.intValue = EditorGUILayout.Popup("Material", materialIndexProperty.intValue, materials);

                if (materialIndexProperty.intValue < renderer.sharedMaterials.Length)
                {
                    material = renderer.sharedMaterials[materialIndexProperty.intValue];
                }
                else
                {
                    material = null;
                    propertyIndex = -1;
                }

                if (material != null)
                {
                    MaterialPropertyType type = (MaterialPropertyType)materialPropertyTypeProperty.enumValueIndex;

                    List<string> properties = MaterialUtils.GetMaterialProperties(material, type);

                    if (propertyIndex == -1)
                    {
                        propertyIndex = 0;

                        for (int i = 0; i < properties.Count; ++i)
                        {
                            if (string.Equals(properties[i], propertyProperty.stringValue))
                            {
                                propertyIndex = i;
                                break;
                            }
                        }
                    }

                    propertyIndex = EditorGUILayout.Popup("Properties", propertyIndex, properties.ToArray());

                    if (propertyIndex > -1)
                    {
                        if (propertyIndex < properties.Count)
                        {
                            propertyProperty.stringValue = properties[propertyIndex];
                        }
                        else
                        {
                            propertyProperty.stringValue = "";
                        }
                    }
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
            rendererProperty = serializedObject.FindProperty("renderer");
            materialIndexProperty = serializedObject.FindProperty("materialIndex");
            materialPropertyTypeProperty = serializedObject.FindProperty("materialPropertyType");
            propertyProperty = serializedObject.FindProperty("property");
        }
    }
}
