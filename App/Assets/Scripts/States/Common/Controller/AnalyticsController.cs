using Assets.Scripts.Common.Controller;
using Assets.Scripts.States.Common.Interface;
using Assets.Scripts.States.Common.Service;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.Common.Controller
{
    public class AnalyticsController : AbstractController
    {
        float deactivateTime;
        string id;
        [Inject]
        protected IAnalyticsService analyticsService;
        [Inject]
        InputTapListenerService tapListenerService;
        [Inject]
        InputSwipeListenerService swipeListenerService;
        int analyticsEventID;
        float updateTime;

        public override void Activate()
        {
            base.Activate();
            updateTime = Time.time;
            Unsubscribe();
            Subscribe();
            analyticsService.RegisterVisitedScreen(id);
            analyticsEventID = analyticsService.CurrentEventID;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            deactivateTime = Time.time;
            Unsubscribe();
        }

        public virtual void Init(string id)
        {
            this.id = id;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            analyticsService.RegisterScreenDuration(analyticsEventID, id, GetActiveDuration());
        }

        void Subscribe()
        {
            tapListenerService.OnTap += OnTapHandler;
            swipeListenerService.OnSwipe += OnSwipeListenerService;
        }

        void Unsubscribe()
        {
            tapListenerService.OnTap -= OnTapHandler;
            swipeListenerService.OnSwipe -= OnSwipeListenerService;
        }

        private void OnTapHandler()
        {
            analyticsService.RegisterTap(id);
        }

        private void OnSwipeListenerService()
        {
            analyticsService.RegisterSwipe();
        }

        float GetActiveDuration()
        {
            var duration = Time.time - updateTime;
            updateTime = Time.time;
            return duration;
        }
    }
}
