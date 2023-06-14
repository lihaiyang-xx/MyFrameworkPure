using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// Texture扩展类
    /// </summary>
    public static class TextureExtension
    {
        /// <summary>
        /// 缩放纹理
        /// </summary>
        /// <param name="inputTexture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        public static void Scale(this Texture2D inputTexture, int width, int height, FilterMode mode = FilterMode.Trilinear)
        {
            var textureRect = new Rect(0, 0, width, height);
            GpuScale(inputTexture, width, height, mode);

            inputTexture.Resize(width, height);
            inputTexture.ReadPixels(textureRect, 0, 0, true);
            inputTexture.Apply(false);
        }

        /// <summary>
        /// 使用GPU进行纹理缩放
        /// </summary>
        /// <param name="src">源纹理</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <param name="filterMode">滤波模式</param>
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

