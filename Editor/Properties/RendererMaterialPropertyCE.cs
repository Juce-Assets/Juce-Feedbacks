using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(RendererMaterialProperty), true)]
    public class RendererMaterialPropertyCE : PropertyDrawer
    {
        private readonly PropertyLayoutHelper layoutHelper = new PropertyLayoutHelper();

        private int materialIndex = -1;
        private int propertyIndex = -1;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int elementsCount = 1;

            SerializedProperty rendererProperty = property.FindPropertyRelative("renderer");
            SerializedProperty materialProperty = property.FindPropertyRelative("material");

            if (rendererProperty.objectReferenceValue != null)
            {
                Renderer renderer = (Renderer)rendererProperty.objectReferenceValue;

                if (materialIndex >= 0 && materialIndex < renderer.sharedMaterials.Length)
                {
                    ++elementsCount;
                }

                Material material = materialProperty.objectReferenceValue as Material;

                if (material != null)
                {
                    ++elementsCount;
                }
            }

            return layoutHelper.GetHeightOfElements(elementsCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty rendererProperty = property.FindPropertyRelative("renderer");
            SerializedProperty materialProperty = property.FindPropertyRelative("material");
            SerializedProperty materialIndexProperty = property.FindPropertyRelative("materialIndex");
            SerializedProperty propertyProperty = property.FindPropertyRelative("property");
            SerializedProperty materialPropertyTypeProperty = property.FindPropertyRelative("materialPropertyType");

            layoutHelper.Init(position);

            EditorGUI.PropertyField(layoutHelper.NextVerticalRect(), rendererProperty);

            if (rendererProperty.objectReferenceValue != null)
            {
                Renderer renderer = (Renderer)rendererProperty.objectReferenceValue;

                string[] materials = new string[renderer.sharedMaterials.Length];

                for (int i = 0; i < renderer.sharedMaterials.Length; ++i)
                {
                    Material currSharedMaterial = renderer.sharedMaterials[i];

                    if (currSharedMaterial != null)
                    {
                        materials[i] = currSharedMaterial.name;
                    }
                }

                if (materialIndex == -1)
                {
                    for (int i = 0; i < renderer.sharedMaterials.Length; ++i)
                    {
                        Material currMaterial = renderer.sharedMaterials[i];

                        if (currMaterial == (Material)materialProperty.objectReferenceValue)
                        {
                            materialIndex = i;
                            break;
                        }
                    }
                }

                if (materialIndex < 0 || materialIndex > materials.Length)
                {
                    materialIndex = 0;
                }

                if (materialIndex >= 0 && materialIndex < materials.Length)
                {
                    materialIndex = EditorGUI.Popup(layoutHelper.NextVerticalRect(), "Material", materialIndex, materials);
                }

                if (materialIndex >= 0 && materialIndex < renderer.sharedMaterials.Length)
                {
                    materialProperty.objectReferenceValue = renderer.sharedMaterials[materialIndex];
                }
                else
                {
                    materialProperty.objectReferenceValue = null;
                }

                Material material = materialProperty.objectReferenceValue as Material;

                if (material != null)
                {
                    materialIndexProperty.intValue = materialIndex;

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

                    propertyIndex = EditorGUI.Popup(layoutHelper.NextVerticalRect(), "Properties", propertyIndex, properties.ToArray());

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

            EditorGUI.EndProperty();
        }
    }
}