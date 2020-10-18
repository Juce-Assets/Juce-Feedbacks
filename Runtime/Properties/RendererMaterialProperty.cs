using System;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class RendererMaterialProperty
    {
        [SerializeField] [HideInInspector] private Renderer renderer = default;
        [SerializeField] [HideInInspector] private Material material = default;
        [SerializeField] [HideInInspector] private int materialIndex = default;
        [SerializeField] [HideInInspector] private string property = default;
        [SerializeField] [HideInInspector] private MaterialPropertyType materialPropertyType = default;

        public RendererMaterialProperty()
        {
            materialPropertyType = MaterialPropertyType.All;
        }

        public RendererMaterialProperty(MaterialPropertyType materialPropertyType)
        {
            this.materialPropertyType = materialPropertyType;
        }

        public Renderer Renderer => renderer;
        public int MaterialIndex => materialIndex;
        public string Property => property;
    }
}
