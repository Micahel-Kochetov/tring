using System;
using UnityEngine;

namespace Assets.Scripts.States.Common.Service
{
    public class ApplicationFocusService : MonoBehaviour
    {
        /// <summary>
        /// Fires true when app is in focus, false when app is soft closed
        /// </summary>
        public event Action<bool> OnFocusChange;
#if UNITY_EDITOR || UNITY_ANDROID
        void OnApplicationFocus(bool hasFocus)
        {
            OnFocusChange?.Invoke(hasFocus);
        }
#endif

#if !UNITY_EDITOR && UNITY_IOS
        void OnApplicationPause(bool isPaused)
        {
            Debug.Log("OnApplicationPause " + isPaused);
            OnFocusChange?.Invoke(!isPaused);
        }
#endif
    }
}
