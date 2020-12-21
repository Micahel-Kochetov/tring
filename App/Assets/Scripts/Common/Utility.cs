using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class Utility
    {

        public static void PrintElapsedTime(this Stopwatch stopWatch, string msg = "")
        {
            TimeSpan timeSpan = stopWatch.Elapsed;
            string elapsedTime = String.Format(" {0:00}.{1:000}", timeSpan.Seconds,
                timeSpan.Milliseconds);
            UnityEngine.Debug.Log($"{msg} {elapsedTime}");
        }

        public static Sprite ConvertTex2DToSprite(Texture2D tex, float pixelsPerUnit = 100f)
        {
            return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }

        public static Color HexToColor(int hex)
        {
            var r = (hex >> 16) & 255;
            var g = (hex >> 8) & 255;
            var b = hex & 255;
            return new Color(r / 255f, g / 255f, b / 255f, 1f);
        }

        public static int ParseInt(string input)
        {
            int result;
            try
            {
                result = Convert.ToInt32(Regex.Replace(input, "[^0-9]", string.Empty));
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }

        public static Texture2D LoadTexture(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            if (System.IO.File.Exists(path))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);
                return texture;
            }
            return null;
        }

        public static Texture2D CropToSquareImage(Texture2D source)
        {
            if (source == null)
            {
                return null;
            }
            if (source.width == source.height)
            {
                return source;
            }
            else
            {
                int width, height;
                int paddingX, paddingY;
                if (source.width > source.height)
                {
                    width = source.height;
                    height = source.height;
                    paddingX = (source.width - source.height) / 2;
                    paddingY = 0;
                }
                else
                {
                    width = source.width;
                    height = source.width;
                    paddingY = (source.height - source.width) / 2;
                    paddingX = 0;
                }
                var output = new Texture2D(width, height, TextureFormat.ARGB32, false);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        var pixel = source.GetPixel(paddingX + i, paddingY + j);
                        output.SetPixel(i, j, pixel);
                    }
                }
                output.Apply(false, false);
                return output;
            }
        }
    }
}
