using System;
using UnityEngine;

namespace Assets.Scripts.States.ARRing.View
{
    [Serializable]
    public class VuforiaMarkersView
    {
        public event Action OnTrackingFoundEvent;
        public event Action OnTrackingLostEvent;
        [SerializeField]
        CustomTrackableEventHandler ringEventHandler;

        public void Init()
        {
            ringEventHandler.OnTrackingFoundEvent += OnTrakingFoundHandler;
            ringEventHandler.OnTrackingLostEvent += OnTrakingLostHandler;
        }

        public void Dispose()
        {
            ringEventHandler.OnTrackingFoundEvent -= OnTrakingFoundHandler;
            ringEventHandler.OnTrackingLostEvent -= OnTrakingLostHandler;
        }

        private void OnTrakingFoundHandler()
        {
            OnTrackingFoundEvent?.Invoke();
        }

        private void OnTrakingLostHandler()
        {
            OnTrackingLostEvent?.Invoke();
        }
    }
}
