using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class SelectableModalButton : ModalButton, ISelectable
    {
        [SerializeField]
        private Color selectedColor = Color.white;
        [SerializeField]
        private Sprite selectedSprite;
        [SerializeField]
        private Color selectedTextColor = Color.white;
        [SerializeField]
        private Color initialColor = Color.white;
        [SerializeField]
        private Color initialTextColor = Color.white;
        [SerializeField]
        private Sprite initialSprite;
        private bool isSelected;
        private Text textComponent;

        protected override void ClickHandler()
        {
            if (isSelected)
            {
                base.ClickHandler();
            }
            else
            {
                if (isEnabled)
                {
                    handler?.Invoke();
                }
            }
        }

        public override void Init(Action handler)
        {
            base.Init(handler);
            if (initialSprite == null)
            {
                initialSprite = image.sprite;
            }
            initialColor = targetGraphic.color;
            isSelected = false;
            if (transform.childCount > 0)
            {
                textComponent = transform.GetChild(0).GetComponent<Text>();
            }
        }

        public virtual void SetSelected()
        {
            targetGraphic.color = selectedColor;
            if (selectedSprite != null)
            {
                image.sprite = selectedSprite;
            }
            isSelected = true;
            if (textComponent != null)
            {
                textComponent.color = selectedTextColor;
            }
        }

        public virtual void SetDeselected()
        {
            targetGraphic.color = initialColor;
            if (initialSprite != null)
            {
                image.sprite = initialSprite;
            }
            isSelected = false;
            if (textComponent != null)
            {
                textComponent.color = initialTextColor;
            }
        }

        public void UpdateUIState(bool selected)
        {
            if (selected)
            {
                SetSelected();
            }
            else
            {
                SetDeselected();
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
        }

    }
}
