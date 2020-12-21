using Common.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.States.ARRing.View
{
    public class VideoPlaceholderView : ModalView
    {
        public event Action OnAdd;
        [SerializeField]
        ModalButton addBtn;

        protected override void AddButtons()
        {
            base.AddButtons();
            AddButton(addBtn, OnAddBtnClickHandler);
        }

        private void OnAddBtnClickHandler()
        {
            OnAdd?.Invoke();
        }
    }
}
