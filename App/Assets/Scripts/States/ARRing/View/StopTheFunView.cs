using UnityEngine;
using System;
using Common.UI;

namespace Assets.Scripts.States.ARRing.View
{
    public class StopTheFunView : ModalView
    {
        public event Action OnYes;
        public event Action OnNo;
        [SerializeField]
        ModalButton yesBtn;
        [SerializeField]
        ModalButton noBtn;
        [SerializeField]
        ModalButton closeBtn;

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(yesBtn, OnYesHandler);
            AddButton(noBtn, OnNoHandler);
            AddButton(closeBtn, OnNoHandler);
        }

        private void OnYesHandler()
        {
            OnYes?.Invoke();
        }

        private void OnNoHandler()
        {
            OnNo?.Invoke();
        }
    }
}
