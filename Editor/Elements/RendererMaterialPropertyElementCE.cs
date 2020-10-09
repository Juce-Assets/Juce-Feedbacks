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
        private SerializedProperty materialProperty;
        private SerializedProperty materialPropertyTypeProperty;
        private SerializedProperty propertyIndexProperty;
        private SerializedProperty propertyProperty;

        private void OnEnable()
        {
            GatherProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(rendererProperty);

            if(rendererProperty.objectReferenceValue != null)
            {
                Renderer renderer = (Renderer)rendererProperty.objectReferenceValue;

                string[] materials = new string[renderer.sharedMaterials.Length];

                for(int i = 0; i < renderer.sharedMaterials.Length; ++i)
                {
                    materials[i] = renderer.sharedMaterials[i].name;
                }

                materialIndexProperty.intValue = EditorGUILayout.Popup("Material", materialIndexProperty.intValue, materials);

                if (materialIndexProperty.intValue < renderer.sharedMaterials.Length)
                {
                    materialProperty.objectReferenceValue = renderer.sharedMaterials[materialIndexProperty.intValue];
                }
                else
                {
                    materialProperty.objectReferenceValue = null;
                }

                if (materialProperty.objectReferenceValue != null)
                {
                    Material material = (Material)materialProperty.objectReferenceValue;

                    int propertiesCount = ShaderUtil.GetPropertyCount(material.shader);

                    MaterialPropertyType type = (MaterialPropertyType)materialPropertyTypeProperty.enumValueIndex;
                    ShaderUtil.ShaderPropertyType typeLookingFor = TypeToShaderType(type);

                    List<string> properties = new List<string>();

                    for (int i = 0; i < propertiesCount; ++i)
                    {
                        ShaderUtil.ShaderPropertyType currPropertyType = ShaderUtil.GetPropertyType(material.shader, i);

                        if(typeLookingFor == currPropertyType)
                        {
                            properties.Add(ShaderUtil.GetPropertyName(material.shader, i));
                        }
                    }

                    propertyIndexProperty.intValue = EditorGUILayout.Popup("Properties", propertyIndexProperty.intValue, properties.ToArray());

                    if(propertyIndexProperty.intValue < properties.Count)
                    {
                        propertyProperty.stringValue = properties[propertyIndexProperty.intValue];
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
            materialProperty = serializedObject.FindProperty("material");
            materialPropertyTypeProperty = serializedObject.FindProperty("materialPropertyType");
            propertyIndexProperty = serializedObject.FindProperty("propertyIndex");
            propertyProperty = serializedObject.FindProperty("property");
        }

        private ShaderUtil.ShaderPropertyType TypeToShaderType(MaterialPropertyType type)
        {
            switch(type)
            {
                case MaterialPropertyType.Color:
                    {
                        return ShaderUtil.ShaderPropertyType.Color;
                    }

                case MaterialPropertyType.Float:
                    {
                        return ShaderUtil.ShaderPropertyType.Float;
                    }
            }

            return default;
        }
    }
}

//if (materialProperty.objectReferenceValue != null)
//{
//    Material material = (Material)materialProperty.objectReferenceValue;

//    int propertiesCount = ShaderUtil.GetPropertyCount(material.shader);

//    for (int i = 0; i < propertiesCount; ++i)
//    {
//        Type type = ShaderUtil.GetPropertyType(material.shader, i);
//    }
//}
