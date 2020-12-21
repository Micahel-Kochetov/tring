using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ColorCorrection
{
    public class SkinColorMonitor : MonoBehaviour
    {
        [SerializeField]
        Camera viewCamera;
        [SerializeField]
        Transform measurementPoint0;
        [SerializeField]
        Transform measurementPoint1;
        [SerializeField]
        CustomTrackableEventHandler vuforiaEventHandler;
        [SerializeField]
        UnityEngine.UI.Image skinImage;
        bool markerFound;
        RenderTexture rt;
        RectTransform canvasRect;
        Queue<Color> colors0;
        Queue<Color> colors1;
        int maxCount = 60;
        Camera renderCamera;
        string vuforiCameraBackgroundLayerName = "VuforiaCamera";
        [SerializeField]
        float colorMultiplier = 1.1f;
        [SerializeField]
        Material handMaterial;
        public float SkinColorAdditiveParameter
        {
            get; private set;
        }

        void Start()
        {
            vuforiaEventHandler.OnTrackingFoundEvent += OnTrackingFoundHandler;
            vuforiaEventHandler.OnTrackingLostEvent += OnTrackingLostHandler;
            rt = new RenderTexture(Screen.width, Screen.height, 24);
            //canvasRect = refPointRect0.parent.GetComponent<RectTransform>();
            colors0 = new Queue<Color>();
            colors1 = new Queue<Color>();
            var renderCameraGO = new GameObject();
            renderCamera = renderCameraGO.AddComponent<Camera>();
            //according to tests we brighten the skin color
            SkinColorAdditiveParameter = 20f / 255f;
            SetLayerAsync();
        }

        async Task SetLayerAsync()
        {
            await Task.Delay(1000);
            viewCamera.transform.GetChild(0).gameObject.layer = viewCamera.gameObject.layer;
        }

        void OnDestoy()
        {
            vuforiaEventHandler.OnTrackingFoundEvent -= OnTrackingFoundHandler;
            vuforiaEventHandler.OnTrackingLostEvent -= OnTrackingLostHandler;
            rt = null;
        }

        void OnTrackingFoundHandler()
        {
            markerFound = true;
        }

        void OnTrackingLostHandler()
        {
            markerFound = false;
        }

        void Update()
        {
            //refPointRect0.gameObject.SetActive(markerFound && debugPoints);
            //refPointRect1.gameObject.SetActive(markerFound && debugPoints);
            if (markerFound)
            {
                CopyCameraSettings(viewCamera, renderCamera);

                Color col0;
                if (GetScreenPixel(measurementPoint0.position, out col0))
                    AddColor(colors0, col0);

                Color col1;
                if (GetScreenPixel(measurementPoint1.position, out col1))
                    AddColor(colors1, col1);

                Color smoothedColor0;
                bool haveSColor0 = GetColor(colors0, out smoothedColor0);
                Color smoothedColor1;
                bool haveSColor1 = GetColor(colors1, out smoothedColor1);

                var averageSmoothedColor = Color.white;
                if (haveSColor0 && haveSColor1)
                {
                    averageSmoothedColor = GetAverageColor(smoothedColor0, smoothedColor1);
                }
                else if (!haveSColor0 && haveSColor1)
                {
                    averageSmoothedColor = smoothedColor1;
                }
                else if (haveSColor0 && !haveSColor1)
                {
                    averageSmoothedColor = smoothedColor0;
                }

                ChangeToneColor(averageSmoothedColor);

                //SetRefPointPos(refPointRect0, measurementPoint0.position);
                //SetRefPointPos(refPointRect1, measurementPoint1.position);
            }
            else
            {
                colors0?.Clear();
            }
        }

        public void ChangeToneColor(Color col)
        {
            col.r += SkinColorAdditiveParameter;
            col.g += SkinColorAdditiveParameter;
            col.b += SkinColorAdditiveParameter;
            handMaterial.SetColor("_Color", col);
        }

        void SetRefPointPos(RectTransform refPoint, Vector3 worldPos)
        {
            var screenPos = viewCamera.WorldToScreenPoint(worldPos);
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out localPoint))
            {
                refPoint.transform.localPosition = localPoint;
            }
        }

        Color GetAverageColor(Color color1, Color color2)
        {
            var r = colorMultiplier * (color1.r + color2.r) / 2f;
            var g = colorMultiplier * (color1.g + color2.g) / 2f;
            var b = colorMultiplier * (color1.b + color2.b) / 2f;
            return new Color(r, g, b);
        }

        bool GetScreenPixel(Vector3 worldPoint, out Color color)
        {
            var screenPos = viewCamera.WorldToScreenPoint(worldPoint);
            Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
            if (!screenRect.Contains(screenPos))
            {
                color = Color.white;
                return false;
            }
            var cameraImage = GetCameraTexture(renderCamera, (int)screenPos.x, (int)screenPos.y);
            color = cameraImage.GetPixel(0, 0);
            return true;
        }

        Texture2D GetCameraTexture(Camera camera, int x, int y)
        {
            RenderTexture rendText = RenderTexture.active;
            camera.targetTexture = rt;
            RenderTexture.active = rt;
            camera.Render();
            Texture2D cameraImage = new Texture2D(1, 1, TextureFormat.RGB24, false);
            cameraImage.ReadPixels(new Rect(x, y, 1, 1), 0, 0);
            cameraImage.Apply();
            RenderTexture.active = rendText;
            camera.targetTexture = null;

            return cameraImage;
        }

        void AddColor(Queue<Color> colors, Color color)
        {
            colors.Enqueue(color);
            if (colors.Count > maxCount)
            {
                colors.Dequeue();
            }
        }

        bool GetColor(Queue<Color> colors, out Color color)
        {
            if (colors.Count == 0)
            {
                color = Color.white;
                return false;
            }
            float r = 0;
            float g = 0;
            float b = 0;
            foreach (var item in colors)
            {
                r += item.r;
                g += item.g;
                b += item.b;
            }
            color = new Color(r / colors.Count, g / colors.Count, b / colors.Count);
            return true;
        }

        void CopyCameraSettings(Camera source, Camera destination)
        {
            destination.transform.parent = source.transform.parent;
            destination.transform.position = source.transform.position;
            destination.transform.rotation = source.transform.rotation;
            destination.transform.localScale = source.transform.localScale;
            destination.fieldOfView = source.fieldOfView;
            destination.nearClipPlane = source.nearClipPlane;
            destination.farClipPlane = source.farClipPlane;
            destination.clearFlags = CameraClearFlags.Depth;
            destination.depth = source.depth - 1;
            destination.cullingMask = 1 << VuforiaBackgroundLayer;
        }

        int VuforiaBackgroundLayer
        {
            get
            {
                return LayerMask.NameToLayer(vuforiCameraBackgroundLayerName);
            }
        }
    }
}
