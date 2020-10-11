using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    public static class MaterialUtils
    {
        public static ShaderUtil.ShaderPropertyType TypeToShaderType(MaterialPropertyType type)
        {
            switch (type)
            {
                case MaterialPropertyType.Color:
                    {
                        return ShaderUtil.ShaderPropertyType.Color;
                    }

                case MaterialPropertyType.Float:
                    {
                        return ShaderUtil.ShaderPropertyType.Float;
                    }

                case MaterialPropertyType.Vector:
                    {
                        return ShaderUtil.ShaderPropertyType.Vector;
                    }
            }

            return default;
        }

        public static List<string> GetMaterialProperties(Material material, MaterialPropertyType type)
        {
            List<string> properties = new List<string>();

            if(material == null)
            {
                return properties;
            }

            int propertiesCount = ShaderUtil.GetPropertyCount(material.shader);

            ShaderUtil.ShaderPropertyType typeLookingFor = TypeToShaderType(type);

            for (int i = 0; i < propertiesCount; ++i)
            {
                ShaderUtil.ShaderPropertyType currPropertyType = ShaderUtil.GetPropertyType(material.shader, i);

                if (typeLookingFor == currPropertyType || type == MaterialPropertyType.All)
                {
                    properties.Add(ShaderUtil.GetPropertyName(material.shader, i));
                }
            }

            return properties;
        }
    }
}
