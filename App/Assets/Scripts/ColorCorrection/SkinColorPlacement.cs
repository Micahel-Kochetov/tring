using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Experimental.Rendering;

namespace Assets.Scripts.ColorCorrection
{
    public class SkinColorPlacement : MonoBehaviour
    {
        [SerializeField]
        CustomTrackableEventHandler vuforiaEventHandler;
        bool markerFound;
        [SerializeField]
        RenderTexture rt;
        [SerializeField]
        Material handMaterial;
        [SerializeField]
        Transform p00;
        [SerializeField]
        Transform p01;
        [SerializeField]
        Transform p10;
        [SerializeField]
        Transform p11;
        [SerializeField]
        Transform handBlend;
        [SerializeField]
        bool start = false;
        Camera renderCamera;
        readonly string vuforiCameraBackgroundLayerName = "VuforiaCamera";
        [SerializeField]
        RenderTextureFormat renderTextureFormat;
        [SerializeField]
        TextureFormat readPixelsDestinationFormat;

        Camera viewCamera;

        public float SkinColorAdditiveParameter
        {
            get; set;
        }

        void Start()
        {
            vuforiaEventHandler.OnTrackingFoundEvent += OnTrackingFoundHandler;
            vuforiaEventHandler.OnTrackingLostEvent += OnTrackingLostHandler;
            rt = new RenderTexture(Screen.width, Screen.height, 24, renderTextureFormat);
            var renderCameraGO = new GameObject();
            renderCamera = renderCameraGO.AddComponent<Camera>();
            viewCamera = VuforiaARCameraController.Instance.Camera;
            SkinColorAdditiveParameter = 0;
            SetLayerAsync();
        }

        async Task SetLayerAsync()
        {
            while (viewCamera.transform.childCount < 1)
            {
                await Task.Delay(100);
            }
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
            if (markerFound && start)
            {
                CopyCameraSettings(viewCamera, renderCamera);
                Texture2D tex;
                var pos = handBlend.InverseTransformPoint(viewCamera.transform.position);
                pos.y = 0;
                handBlend.LookAt(handBlend.TransformPoint(pos));
                if (TryGetSkinTexture(p00.position, p01.position, p10.position, p11.position, out tex))
                {
                    handMaterial.SetTexture("_MainTex", tex);
                    Debug.Log("Texture updated " +Time.time);
                }

                Resources.UnloadUnusedAssets();
            }
        }

        Vector2 RotatePoint(float clockwiseAngle, Vector2 pos)
        {
            var cos = Mathf.Cos(clockwiseAngle * Mathf.PI / 180f);
            var sin = Mathf.Sin(clockwiseAngle * Mathf.PI / 180f);
            var x = pos.x * cos - pos.y * sin;
            var y = pos.x * sin + pos.y * cos;
            return new Vector2(x, y);
        }

        bool TryGetSkinTexture(Vector3 wp00, Vector3 wp01, Vector3 wp10, Vector3 wp11, out Texture2D tex)
        {
            var sp00 = viewCamera.WorldToScreenPoint(wp00);
            var sp01 = viewCamera.WorldToScreenPoint(wp01);
            var sp10 = viewCamera.WorldToScreenPoint(wp10);
            var sp11 = viewCamera.WorldToScreenPoint(wp11);
            var center = (sp00 + sp10) / 2f;
            var points = new List<Vector2>() { sp00, sp01, sp10, sp11 };
            var minXPoint = points.OrderBy(p => p.x).First();
            tex = null;
//#if UNITY_EDITOR
//            var rect = new Rect(sp01.x, Screen.height - sp01.y, (sp01 - sp10).magnitude, (sp00 - sp01).magnitude);
//#else
            var rect = new Rect(sp01.x, sp01.y, (sp01 - sp10).magnitude, (sp00 - sp01).magnitude);
//#endif
            var angle = Vector2.Angle(Vector2.right, sp00 - sp11);
            if ((sp00 - sp11).y < 0)
            {
                angle *= -1;
            }
            Vector2 dc = sp11;
            Texture2D fullScreenTexure = GetCameraRenderTexture(renderCamera);

            tex = new Texture2D((int)(sp01 - sp10).magnitude, (int)(sp00 - sp01).magnitude);

            for (int i = 0; i < tex.width; i++)
            {
                for (int j = 0; j < tex.height; j++)
                {
                    var pos = RotatePoint(angle, new Vector2(i, j));
                    pos += dc;
                    if (pos.x < 0 || pos.x > fullScreenTexure.width ||
                        pos.y < 0 || pos.y > fullScreenTexure.height)
                    {
                        return false;
                    }
                    var color = fullScreenTexure.GetPixel((int)pos.x, (int)pos.y);
                    tex.SetPixel(i, j, color);
                }
            }
            tex.Apply();
            return true;
        }

        Texture2D GetCameraRenderTexture(Camera camera)
        {
            RenderTexture rendText = RenderTexture.active;
            camera.targetTexture = rt;
            RenderTexture.active = rt;
            camera.Render();
            var cameraImage = new Texture2D(Screen.width, Screen.height, readPixelsDestinationFormat, false);
            cameraImage.ReadPixels(new Rect(0, 0, cameraImage.width, cameraImage.height), 0, 0);
            cameraImage.Apply();
            RenderTexture.active = rendText;
            camera.targetTexture = null;
            return cameraImage;
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
