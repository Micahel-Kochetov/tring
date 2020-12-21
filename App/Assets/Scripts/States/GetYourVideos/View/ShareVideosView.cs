using Common.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.States.GetYourVideos.View
{
    public class ShareVideosView : ModalView
    {
        public enum ShareType { Email, AirDrop, WhatsApp, WhatsApp_Purchase }
        
        public event Action<ShareType> OnSubmit;
        public event Action OnClose;
        [SerializeField]
        RawImage qrCodeImage;
        [SerializeField]
        ShareOptionToggle airDropToggle;
        [SerializeField]
        ShareOptionToggle whatsAppToggle;
        [SerializeField]
        ShareOptionSubToggle purchaseConfirmationToggle;
        [SerializeField]
        GameObject submitDisabledButtonGO;
        [SerializeField]
        ModalButton submitEnabledButton;
        [SerializeField]
        ModalButton closeButton;

        private ShareType shareType;

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(submitEnabledButton, OnSubmitHandler);
            AddButton(closeButton, OnCloseHandler);
        }

        public override void Show()
        {
            base.Show();

            airDropToggle.ForceSet(false);
            whatsAppToggle.ForceSet(false);
            purchaseConfirmationToggle.ForceSet(false, false);

            SetSubmitButtonActive(false);
        }

        private void SetSubmitButtonActive(bool isActive)
        {
            submitDisabledButtonGO.SetActive(!isActive);
            submitEnabledButton.gameObject.SetActive(isActive);
        }

        private void OnCloseHandler()
        {
            OnClose?.Invoke();
        }

        private void OnSubmitHandler()
        {
            OnSubmit?.Invoke(shareType);
        }

        public void OnEmailToggleChangeHandler(bool isOn)
        {
            if(isOn)
            {
                airDropToggle.ForceSet(false);
                whatsAppToggle.ForceSet(false);
                purchaseConfirmationToggle.ForceSet(false, false);

                shareType = ShareType.Email;
            }

            SetSubmitButtonActive(isOn);
        }

        public void OnAirDropToggleChangeHandler(bool isOn)
        {
            if (isOn)
            {
                whatsAppToggle.ForceSet(false);
                purchaseConfirmationToggle.ForceSet(false, false);

                shareType = ShareType.AirDrop;
            }

            SetSubmitButtonActive(isOn);
        }

        public void OnWhatAppToggleChangeHandler(bool isOn)
        {
            if (isOn)
            {
                airDropToggle.ForceSet(false);

                shareType = ShareType.WhatsApp;
            }
            purchaseConfirmationToggle.ForceSet(isOn, false);

            SetSubmitButtonActive(isOn);
        }

        public void OnPurchaseConfirmToggleChangeHandler(bool isOn)
        {

            shareType = isOn? ShareType.WhatsApp_Purchase : ShareType.WhatsApp;
        }

        public void SetQRCodeTexture(Texture2D qrCodeTexture)
        {
            qrCodeImage.texture = qrCodeTexture;
        }
    }
}
