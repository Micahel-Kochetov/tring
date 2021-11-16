using Assets.Scripts.ColorCorrection;
using Assets.Scripts.States.ARRing.View;
using Assets.Scripts.States.Common.Controller;
using Assets.Scripts.States.Common.Service;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class ARRingController : AnalyticsController
    {
        public event Action<int> OnRingCarouselIndexChanged;
        public event Action OnPrevRing;
        public event Action OnNextRing;
        [Inject]
        ARRingView view;
        [Inject]
        RingLabelView ringLabelView;
        [Inject]
        GetVoucherController getVoucherController;
        [Inject]
        StopTheFunController stopTheFunController;
        [Inject]
        RecordVideoController recordVideoController;
        [Inject]
        ScreenRecordingService screenRecordingController;
        [Inject]
        UserSessionService userSessionService;
        [Inject]
        PreshareController preshareController;
        ShowRingController showRingController;
        FavouriteSelectSaver favouriteSelectSaver;
        SkinColorMonitor skinColorMonitor;
        readonly int maxNumberOfVideoSupportedByPreshareScreen = 5;

        public void Init(ShowRingController showRingController, string screenID, SkinColorMonitor skinColorMonitor)
        {
            base.Init(screenID);
            this.showRingController = showRingController;
            showRingController.OnRingIndexChanged += OnRingIndexChanged;
            favouriteSelectSaver = new FavouriteSelectSaver();
            screenRecordingController.OnRecordingFailed += OnRecordingFailedHandler;
            this.skinColorMonitor = skinColorMonitor;
        }


        public override void Dispose()
        {
            base.Dispose();
            showRingController.OnRingIndexChanged -= OnRingIndexChanged;
            screenRecordingController.OnRecordingFailed -= OnRecordingFailedHandler;
        }

        public override void Activate()
        {
            base.Activate();
            view.Init();
            view.Show();
            view.OnClose += OnCloseHandler;
            view.OnPrevRing += OnPrevRingHandler;
            view.OnNextRing += OnNextRingHandler;
            view.OnBuy += OnBuyHandler;
            view.OnStartRecording += OnStartRecordingHandler;
            view.OnRingChanged += OnRingChangedHandler;
            view.OnFavouriteChanged += OnFavouriteChangedHandler;
            analyticsService.InitRings(showRingController.GetRingNames());
        }

        public override void Deactivate()
        {
            base.Deactivate();
            view.Hide();
            view.OnClose -= OnCloseHandler;
            view.OnPrevRing -= OnPrevRingHandler;
            view.OnNextRing -= OnNextRingHandler;
            view.OnBuy -= OnBuyHandler;
            view.OnStartRecording -= OnStartRecordingHandler;
            view.OnRingChanged -= OnRingChangedHandler;
            view.OnFavouriteChanged -= OnFavouriteChangedHandler;
            view.Dispose();
        }

        private void OnRecordingFailedHandler()
        {
            Deactivate();
            Activate();
        }

        private void OnPrevRingHandler()
        {
            OnPrevRing?.Invoke();
        }

        private void OnNextRingHandler()
        {
            OnNextRing?.Invoke();
        }

        private void OnRingIndexChanged(int obj)
        {
            view.ScrollCarouselToIndex(obj);
            view.ForceSetCurrentFavourite(favouriteSelectSaver.GetFavouriteValue(obj));
        }

        private void OnStartRecordingHandler()
        {
            Deactivate();
            if (userSessionService.Videos.Count >= maxNumberOfVideoSupportedByPreshareScreen)
            {
                preshareController.Activate();
            }
            else
            {
                recordVideoController.Activate();
            }
        }

        private void OnBuyHandler()
        {
            var selectRingSprite = showRingController.GetSelectedRingSprite();
            getVoucherController.Activate(selectRingSprite);
            var currentRingPrice = showRingController.GetSelectedRingPrice();
            analyticsService.RegisterBuyNowClick(currentRingPrice);
        }

        private void OnCloseHandler()
        {
            stopTheFunController.Activate();
        }

        private void OnRingChangedHandler(int index)
        {
            ringLabelView.FadeInAndFadeOut(index);
            Debug.Log($"OnRingChangedHandler {index}");
            OnRingCarouselIndexChanged?.Invoke(index);
            view.ForceSetCurrentFavourite(favouriteSelectSaver.GetFavouriteValue(index));
        }

        private void OnFavouriteChangedHandler(int index, bool isFavourite)
        {
            favouriteSelectSaver.SetFavouriteValue(index, isFavourite);
            var ringPrice = showRingController.GetRingPrice(index);
            var likeValue = isFavourite ? ringPrice : -ringPrice;
            analyticsService.RegisterLikeValue(likeValue);
            var ringName = showRingController.GetRingName(index);
            analyticsService.RegisterRingLikeState(ringName, isFavourite);
        }
    }
}
