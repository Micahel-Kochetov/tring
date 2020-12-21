using Assets.Scripts.Common.Controller;
using Assets.Scripts.States.ARRing.View;
using System;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class VuforiaMarkersController : AbstractController
    {
        public event Action OnTrackingFoundEvent;
        public event Action OnTrackingLostEvent;
        [Inject]
        VuforiaMarkersView view;
        public override void Activate()
        {
            base.Activate();
            view.OnTrackingFoundEvent += OnTrackingFoundHandler;
            view.OnTrackingLostEvent += OnTrackingLostHandler;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            view.OnTrackingFoundEvent -= OnTrackingFoundHandler;
            view.OnTrackingLostEvent -= OnTrackingLostHandler;
        }

        public void Init()
        {
            view.Init();
        }

        public override void Dispose()
        {
            base.Dispose();
            view.Dispose();
        }

        private void OnTrackingLostHandler()
        {
            OnTrackingLostEvent?.Invoke();
        }

        private void OnTrackingFoundHandler()
        {
            OnTrackingFoundEvent?.Invoke();
        }

        protected override void OnUpdate() { }
    }
}
