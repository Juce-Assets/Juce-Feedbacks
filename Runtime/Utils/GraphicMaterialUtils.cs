namespace Juce.Feedbacks
{
    public static class GraphicMaterialUtils
    {
        public static void TryInstantiateGraphicMaterial(GraphicMaterialProperty target)
        {
            if (target.InstantiateMaterial)
            {
                GraphicMaterialInstance materialInstance = target.Graphic.gameObject.GetComponent<GraphicMaterialInstance>();

                if (materialInstance == null)
                {
                    materialInstance = target.Graphic.gameObject.AddComponent<GraphicMaterialInstance>();

                    materialInstance.Init(target.Graphic);
                }
            }
        }
    }
}