using UnityEngine;

namespace Gameknit
{
    public static class RendererUtils
    {
        private static readonly int Color = Shader.PropertyToID("_Color");

        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        public static Color GetColor(this Renderer renderer)
        {
            var material = renderer.material;
            return material.GetColor(Color);
        }

        public static void SetColor(this Renderer renderer, Color color)
        {
            var material = renderer.material;
            material.SetColor(Color, color);
        }

        public static void SetBaseColor(this Renderer renderer, Color color)
        {
            var material = renderer.material;
            material.SetColor(BaseColor, color);
        }
        
        public static void GetBaseColor(this Renderer renderer)
        {
            var material = renderer.material;
            material.GetColor(BaseColor);
        }
    }
}