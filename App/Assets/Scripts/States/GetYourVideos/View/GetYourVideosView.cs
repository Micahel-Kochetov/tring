using Assets.Scripts.Common;
using Common.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.States.GetYourVideos.View
{
    public class GetYourVideosView : ModalView
    {
        public event Action<string, string> OnSendVideos;
        public event Action OnClose;
        [SerializeField]
        GameObject sendVideosDisabled;
        [SerializeField]
        ModalButton sendVideos;
        [SerializeField]
        ModalButton closeBtn;
        [SerializeField]
        InputField emailTxt;
        [SerializeField]
        InputField phoneTxt;
        [SerializeField]
        GameObject emailInvalidFrame;
        [SerializeField]
        GameObject phoneInvalidFrame;
        [SerializeField]
        GameObject validationDetailsContainer;
        [SerializeField]
        Text validationDetailsText;
        private readonly string emailInvalidDetails = "Email has some mistakes. Please check it and try again.";
        private readonly string phoneInvalidDetails = "Phone Number has some mistakes. Please check it and try again.";
        private readonly int correctFontColorHex = 0x5D5360;
        private readonly int incorrectFontColorHex = 0xE23171;
        private Color correctFontColor;
        private Color incorrectFontColor;

        public override void Init()
        {
            base.Init();
            correctFontColor = Scripts.Common.Utility.HexToColor(correctFontColorHex);
            incorrectFontColor = Scripts.Common.Utility.HexToColor(incorrectFontColorHex);
        }

        public override void Show()
        {
            base.Show();
            EmailIsValid = true;
            PhoneIsValid = true;
        }

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(sendVideos, OnSendVideosHandler);
            AddButton(closeBtn, OnCloseHandler);
        }

        public override void Subscribe()
        {
            base.Subscribe();
            emailTxt.onValueChanged.AddListener(OnEmailInputChanged);
            phoneTxt.onValueChanged.AddListener(OnPhoneInputChanged);
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            emailTxt.onValueChanged.RemoveListener(OnEmailInputChanged);
            phoneTxt.onValueChanged.RemoveListener(OnPhoneInputChanged);
        }

        private void OnCloseHandler()
        {
            OnClose?.Invoke();
        }

        private void OnSendVideosHandler()
        {
            if (string.IsNullOrWhiteSpace(emailTxt.text))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(phoneTxt.text))
            {
                return;
            }
            OnSendVideos?.Invoke(emailTxt.text, phoneTxt.text);
        }

        private void OnEmailInputChanged(string text)
        {
            ValidateEmail();
        }

        private void OnPhoneInputChanged(string text)
        {
            ValidatePassword();
        }

        private void ValidateEmail()
        {
            EmailIsValid = Validator.EmailIsValid(emailTxt.text);
            UpdateFontColor(emailTxt, EmailIsValid);
        }

        private void ValidatePassword()
        {
            PhoneIsValid = Validator.PhoneNumberIsValid(phoneTxt.text);
            UpdateFontColor(phoneTxt, PhoneIsValid);
        }

        void UpdateValidationState()
        {
            var isValid = EmailIsValid && PhoneIsValid;
            var sendBtnDataIsValid = isValid && 
                !string.IsNullOrEmpty(emailTxt.text) &&
                !string.IsNullOrEmpty(phoneTxt.text);
            sendVideosDisabled.SetActive(!sendBtnDataIsValid);
            sendVideos.gameObject.SetActive(sendBtnDataIsValid);
            validationDetailsContainer.SetActive(!isValid);
            if (!isValid)
            {
                if (!EmailIsValid)
                {
                    validationDetailsText.text = emailInvalidDetails;
                }
                else
                {
                    validationDetailsText.text = phoneInvalidDetails;
                }
            }
        }

        void UpdateFontColor(InputField field, bool isValid)
        {
            var color = isValid ? correctFontColor : incorrectFontColor;
            field.textComponent.color = color;
        }

        bool emailIsValid;
        private bool EmailIsValid
        {
            set
            {
                emailInvalidFrame.gameObject.SetActive(!value);
                emailIsValid = value;
                UpdateValidationState();
            }
            get
            {
                return emailIsValid;
            }
        }
        bool phoneIsValid;
        private bool PhoneIsValid
        {
            set
            {
                phoneInvalidFrame.gameObject.SetActive(!value);
                phoneIsValid = value;
                UpdateValidationState();
            }
            get
            {
                return phoneIsValid;
            }
        }
    }
}
