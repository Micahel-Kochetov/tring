using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common
{
    public class FPSCounter : MonoBehaviour
    {
        float deltaTime = 0.0f;
        [SerializeField]
        Text text;
        float updateTime;
        float interval = 1f;

        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            if (Time.time - updateTime > interval)
            {
                text.text = string.Format("{0:0}", fps);
                updateTime = Time.time;
            }
        }


    }
}
