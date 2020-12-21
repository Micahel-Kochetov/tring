using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Debugging
{
    class ARContentScaler : MonoBehaviour
    {
        [SerializeField]
        InputField scaleInput;
        [SerializeField]
        Button btnApply;
        [SerializeField]
        Transform target;

        void Start()
        {
            btnApply.onClick.AddListener(OnApplyHandler);
        }

        void Destroy()
        {
            btnApply.onClick.RemoveListener(OnApplyHandler);
        }

        private void OnApplyHandler()
        {

            float scale;
            if (float.TryParse(scaleInput.text, out scale))
            {
                target.localScale = Vector3.one * scale;
            }
        }
    }
}
