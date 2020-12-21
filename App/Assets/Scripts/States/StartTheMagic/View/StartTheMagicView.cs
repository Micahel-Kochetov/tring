using Common.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.States.StartTheMagic.View
{
    public class StartTheMagicView : ModalView
    {
        public event Action OnStartTheMagic;
        [SerializeField]
        ModalButton startTheMagicBtn;
        [SerializeField]
        SwipeTransitionControl swipeControl;

        protected override void AddButtons()
        {
            AddButton(startTheMagicBtn, OnStartTheMagicHandler);
        }

        private void OnStartTheMagicHandler()
        {
            OnStartTheMagic?.Invoke();
        }

        public override void Show()
        {
            base.Show();
            swipeControl.StartAnimationCoroutine();
        }

        public override void Hide()
        {
            base.Hide();
            swipeControl.StopAnimationCoroutine();
        }
    }
}
