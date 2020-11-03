using UnityEngine;
using UnityEngine.UI;

namespace Juce.Feedbacks
{
    public class GraphicMaterialInstance : MonoBehaviour
    {
        public void Init(Graphic graphic)
        {
            graphic.material = new Material(graphic.material);
        }
    }
}