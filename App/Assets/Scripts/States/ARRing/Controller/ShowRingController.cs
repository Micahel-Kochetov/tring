using Assets.Scripts.Common.Controller;
using Assets.Scripts.States.ARRing.View;
using Zenject;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class ShowRingController : AbstractController
    {
        public event Action<int> OnRingIndexChanged;
        ARRingController arRingController;
        [Inject]
        ShowRingView view;
        [Inject]
        VuforiaMarkersController markersController;
        [Inject]
        SwipeController swipeController;
        int currentModelIndex;
        bool markerFound = false;
        int CurrentModelIndex
        {
            get
            {
                return currentModelIndex;
            }
            set
            {
                currentModelIndex = value;
                if (currentModelIndex < 0)
                {
                    currentModelIndex = view.RingsCount - 1;
                }
                if (currentModelIndex == view.RingsCount)
                {
                    currentModelIndex = 0;
                }
                OnRingIndexChanged?.Invoke(currentModelIndex);
            }
        }

        public void Init(ARRingController arRingController)
        {
            view.Init();
            view.Hide();
            markersController.OnTrackingFoundEvent += OnTrackingFoundHandler;
            markersController.OnTrackingLostEvent += OnTrackingLostHandler;
            currentModelIndex = 0;
            swipeController.OnSwipeRight += OnSwipeRightHandler;
            swipeController.OnSwipeLeft += OnSwipeLeftHandler;
            this.arRingController = arRingController;
            this.arRingController.OnRingCarouselIndexChanged += OnCarouselRingIndexChangedHandler;
            this.arRingController.OnPrevRing += OnPrevRingHandler;
            this.arRingController.OnNextRing += OnNextRingHandler;
        }

        public float GetSelectedRingPrice()
        {
            var price = view.GetRingPrice(CurrentModelIndex);
            return price;
        }

        private void OnTrackingFoundHandler()
        {
            view.ShowModel(currentModelIndex);
            markerFound = true;
        }

        private void OnTrackingLostHandler()
        {
            view.Hide();
            markerFound = false;
        }

        public override void Dispose()
        {
            base.Dispose();
            view.Dispose();
            markersController.OnTrackingFoundEvent -= OnTrackingFoundHandler;
            markersController.OnTrackingLostEvent -= OnTrackingLostHandler;
            swipeController.OnSwipeRight -= OnSwipeRightHandler;
            swipeController.OnSwipeLeft -= OnSwipeLeftHandler;
            arRingController.OnRingCarouselIndexChanged -= OnCarouselRingIndexChangedHandler;
            arRingController.OnPrevRing -= OnPrevRingHandler;
            arRingController.OnNextRing -= OnNextRingHandler;
        }
        public float GetRingPrice(int i)
        {
            return view.GetRingPrice(i);
        }

        private void OnPrevRingHandler()
        {
            OnSwipeLeftHandler();
        }

        private void OnNextRingHandler()
        {
            OnSwipeRightHandler();
        }


        private void OnCarouselRingIndexChangedHandler(int obj)
        {
            currentModelIndex = obj;
            if (!markerFound)
            {
                return;
            }
            view.ShowModel(currentModelIndex);
        }

        public string GetRingName(int i)
        {
            return view.GetRingName(i);
        }

        public Sprite GetSelectedRingSprite()
        {
            return view.GetRingSprite(currentModelIndex);
        }

        public List<string> GetRingNames()
        {
            var list = new List<string>();
            for (int i = 0; i < RingsCount; i++)
            {
                list.Add(GetRingName(i));
            }
            return list;
        }

        private void OnSwipeRightHandler()
        {
            if (!markerFound)
            {
                return;
            }
            CurrentModelIndex++;
            view.ShowModel(CurrentModelIndex);
        }

        private void OnSwipeLeftHandler()
        {
            if (!markerFound)
            {
                return;
            }
            CurrentModelIndex--;
            view.ShowModel(CurrentModelIndex);
        }

        public int RingsCount
        {
            get
            {
                return view.RingsCount;
            }
        }
    }
}
