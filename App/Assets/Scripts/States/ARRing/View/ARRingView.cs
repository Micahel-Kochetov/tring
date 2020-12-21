using Assets.Scripts.Common.UI;
using Common.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.States.ARRing.View
{
    public class ARRingView : ModalView
    {
        public event Action OnClose;
        public event Action OnBuy;
        public event Action OnStartRecording;
        public event Action<int> OnRingChanged;
        public event Action OnPrevRing;
        public event Action OnNextRing;
        public event Action<int, bool> OnFavouriteChanged;
        [SerializeField]
        ModalButton closeBtn;
        [SerializeField]
        ModalButton buyBtn;
        [SerializeField]
        ModalButton recordBtn;
        [SerializeField]
        ScrollCircledListController scrollCircledListController;
        [SerializeField]
        ModalButton prevRingBtn;
        [SerializeField]
        ModalButton nextRingBtn;
        [SerializeField]
        ModalButton favouriteBtn;
        [SerializeField]
        SelectableImage favouriteImage;
        [SerializeField]
        Animator favouriteBtnAnimator;
        string favouriteBtnAnimatorTrigger = "action";
        private int ringIndex;

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(closeBtn, OnCloseHandler);
            AddButton(buyBtn, OnBuyHandler);
            AddButton(recordBtn, OnStartRecordingHandler);
            AddButton(prevRingBtn, OnPrevBtnHandler);
            AddButton(nextRingBtn, OnNextBtnHandler);
            AddButton(favouriteBtn, OnFavouriteChangedHandler);
        }

        private void OnPrevBtnHandler()
        {
            OnPrevRing?.Invoke();
        }

        private void OnNextBtnHandler()
        {
            OnNextRing?.Invoke();
        }

        private void OnStartRecordingHandler()
        {
            OnStartRecording?.Invoke();
        }

        private void OnBuyHandler()
        {
            OnBuy?.Invoke();
        }

        private void OnCloseHandler()
        {
            OnClose?.Invoke();
        }

        public override void Subscribe()
        {
            base.Subscribe();
            scrollCircledListController.OnScrollItemChanged += OnRingChangedHandler;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            scrollCircledListController.OnScrollItemChanged -= OnRingChangedHandler;
        }

        public void ScrollCarouselToIndex(int index)
        {
            ringIndex = index;
            scrollCircledListController.ForceSelectIndex(ringIndex);
        }

        private void OnRingChangedHandler(int index)
        {
            ringIndex = index;
            OnRingChanged?.Invoke(ringIndex);
        }

        private void OnFavouriteChangedHandler()
        {
            var isSelected = !favouriteImage.IsSelected;
            favouriteImage.UpdateUIState(isSelected);
            OnFavouriteChanged?.Invoke(ringIndex, isSelected);
            if (isSelected&& favouriteBtnAnimator!=null)
            {
                favouriteBtnAnimator.SetTrigger(favouriteBtnAnimatorTrigger);
            }
        }

        public void ForceSetCurrentFavourite(bool isFavourite)
        {
            favouriteImage.UpdateUIState(isFavourite);
        }
    }
}
