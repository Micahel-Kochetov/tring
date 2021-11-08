// Only works on ARGB32, RGB24 and Alpha8 textures that are marked readable
//source: http://wiki.unity3d.com/index.php/TextureScale?_ga=2.96760092.267922783.1570440185-456249921.1547537964

using System;
using UnityEngine;

namespace Assets.Scripts.Common.Utils
{

    public class TextureScaleSingleThread
    {
        private Color[] newColors;
        private int w;
        private float ratioX;
        private float ratioY;
        private int w2;
        private Texture2D tex;

        public void Point(Texture2D tex, int maxSize)
        {
            int width, height;
            CalculateTextureDimentions(tex, maxSize, out width, out height);
            ThreadedScale(tex, width, height, false);
        }

        public void Point(Texture2D tex, int newWidth, int newHeight)
        {
            ThreadedScale(tex, newWidth, newHeight, false);
        }

        private void CalculateTextureDimentions(Texture2D source, int maxSize, out int width, out int height)
        {
            if (source.width < maxSize && source.height < maxSize)
            {
                width = source.width;
                height = source.height;
            }
            else
            {
                var aspect = (float)source.width / (float)source.height;
                float scaleFactor;
                if (aspect >= 1)
                {
                    scaleFactor = (float)maxSize / (float)source.width;
                }
                else
                {
                    scaleFactor = (float)maxSize / (float)source.height;
                }
                width = (int)(source.width * scaleFactor);
                height = (int)(source.height * scaleFactor);
            }
        }

        private void ThreadedScale(Texture2D texture, int newWidth, int newHeight, bool useBilinear)
        {
            tex = texture;
            newColors = new Color[newWidth * newHeight];
            if (useBilinear)
            {
                ratioX = 1.0f / ((float)newWidth / (tex.width - 1));
                ratioY = 1.0f / ((float)newHeight / (tex.height - 1));
            }
            else
            {
                ratioX = ((float)tex.width) / newWidth;
                ratioY = ((float)tex.height) / newHeight;
            }
            w = tex.width;
            w2 = newWidth;
            ThreadData threadData = new ThreadData(0, newHeight);
            PointScale(threadData);
            tex.Reinitialize(newWidth, newHeight);
            tex.SetPixels(newColors);
            tex.Apply();
            newColors = null;
        }

        private void PointScale(System.Object obj)
        {
            ThreadData threadData = (ThreadData)obj;
            for (var y = threadData.start; y < threadData.end; y++)
            {
                var thisY = (int)(ratioY * y) * w;
                var yw = y * w2;
                for (var x = 0; x < w2; x++)
                {
                    newColors[yw + x] = tex.GetPixel((int)(x * ratioX), (int)(ratioY * y));
                }
            }
        }

        public class ThreadData
        {
            public int start;
            public int end;
            public ThreadData(int s, int e)
            {
                start = s;
                end = e;
            }
        }
    }
}