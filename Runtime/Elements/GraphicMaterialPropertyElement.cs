using System;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks
{
    public class GraphicMaterialPropertyElement : Element
    {
        [SerializeField] [HideInInspector] private Graphic graphic = default;
        [SerializeField] [HideInInspector] private bool instantiateMaterial = true;
        [SerializeField] [HideInInspector] private MaterialPropertyType materialPropertyType = default;
        [SerializeField] [HideInInspector] private string property = default;

        public Graphic Graphic => graphic;
        public bool InstantiateMaterial => instantiateMaterial;
        public MaterialPropertyType MaterialPropertyType { set => materialPropertyType = value; }
        public string Property => property;
    }
}
