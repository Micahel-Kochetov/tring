using Common.UI;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.States.ARRing.View
{
    public class GetVoucherView : ModalView
    {
        public event Action OnClose;
        [SerializeField]
        ModalButton closeBtn;
        [SerializeField]
        Image ringImage;
        public Sprite RingImage
        {
            set
            {
                ringImage.sprite = value;
            }
        }

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(closeBtn, OnCloseHandler);
        }

        private void OnCloseHandler()
        {
            OnClose?.Invoke();
        }
    }
}
