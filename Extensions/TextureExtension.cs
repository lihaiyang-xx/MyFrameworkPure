using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class TextureExtension
    {
        public static void Scale(this Texture2D inputTexture, int width, int height, FilterMode mode = FilterMode.Trilinear)
        {
            var textureRect = new Rect(0, 0, width, height);
            GpuScale(inputTexture, width, height, mode);

            inputTexture.Resize(width, height);
            inputTexture.ReadPixels(textureRect, 0, 0, true);
            inputTexture.Apply(false);
        }

        private static void GpuScale(Texture2D src, int width, int height, FilterMode filterMode)
        {
            src.filterMode = filterMode;
            src.Apply(false);
            var renderTexture = new RenderTexture(width, height, 32);
            Graphics.SetRenderTarget(renderTexture);
            GL.LoadPixelMatrix(0, 1, 1, 0);
            GL.Clear(true, true, new Color(0, 0, 0, 0));
            Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
            Object.Destroy(renderTexture);
        }
    }
}

