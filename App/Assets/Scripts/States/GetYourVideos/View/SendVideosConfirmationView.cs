using Common.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.States.GetYourVideos.View
{
    public class SendVideosConfirmationView : ModalView
    {
        public event Action OnFinish;
        public event Action OnBackToRingsSelector;
        [SerializeField]
        ModalButton finishBtn;
        [SerializeField]
        ModalButton backToRingsSelectorBtn;

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(finishBtn, OnFinishHandler);
            AddButton(backToRingsSelectorBtn, OnBackToRingsSelectorHandler);
        }

        private void OnFinishHandler()
        {
            OnFinish?.Invoke();
        }

        private void OnBackToRingsSelectorHandler()
        {
            OnBackToRingsSelector?.Invoke();
        }
    }
}
