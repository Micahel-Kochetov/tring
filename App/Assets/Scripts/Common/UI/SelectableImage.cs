using Common.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common.UI
{
    [Serializable]
    public class SelectableImage : Image, ISelectable
    {
        [SerializeField]
        private Sprite selectedSprite;
        [SerializeField]
        private Sprite initialSprite;
        private bool isSelected;

        public virtual void Init() {
            if (initialSprite == null)
            {
                initialSprite = sprite;
            }
            isSelected = false;
        }


        public void SetDeselected()
        {
            if (initialSprite != null)
            {
                sprite = initialSprite;
            }
            isSelected = false;
        }

        public void SetSelected()
        {
            if (initialSprite != null)
            {
                sprite = selectedSprite;
            }
            isSelected = true;
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
