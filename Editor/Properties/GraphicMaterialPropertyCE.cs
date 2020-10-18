using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks
{
    [CustomPropertyDrawer(typeof(GraphicMaterialProperty), true)]
    public class GraphicMaterialPropertyCE : PropertyDrawer
    {
        private int propertyIndex = -1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty graphicProperty = property.FindPropertyRelative("graphic");
            SerializedProperty instantiateMaterialProperty = property.FindPropertyRelative("instantiateMaterial");
            SerializedProperty propertyProperty = property.FindPropertyRelative("property");
            SerializedProperty materialPropertyTypeProperty = property.FindPropertyRelative("materialPropertyType");

            EditorGUI.PropertyField(position, graphicProperty);

            if (graphicProperty.objectReferenceValue != null)
            {
                Graphic graphic = (Graphic)graphicProperty.objectReferenceValue;

                if (graphic != null)
                {
                    EditorGUILayout.PropertyField(instantiateMaterialProperty);

                    Material material = graphic.materialForRendering;

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

            EditorGUI.EndProperty();
        }
    }
}
