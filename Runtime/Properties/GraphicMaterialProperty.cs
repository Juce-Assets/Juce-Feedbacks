using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks
{
    [System.Serializable]
    public class GraphicMaterialProperty
    {
        [SerializeField] [HideInInspector] private Graphic graphic = default;
        [SerializeField] [HideInInspector] private bool instantiateMaterial = true;
        [SerializeField] [HideInInspector] private string property = default;
        [SerializeField] [HideInInspector] private MaterialPropertyType materialPropertyType = default;

        public GraphicMaterialProperty()
        {
            materialPropertyType = MaterialPropertyType.All;
        }

        public GraphicMaterialProperty(MaterialPropertyType materialPropertyType)
        {
            this.materialPropertyType = materialPropertyType;
        }

        public Graphic Graphic { get => graphic; set => graphic = value; }
        public bool InstantiateMaterial { get => instantiateMaterial; set => instantiateMaterial = value; }
        public string Property { get => property; set => property = value; }
    }
}