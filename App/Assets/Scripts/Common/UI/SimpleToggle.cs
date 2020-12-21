using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI
{
    public class SimpleToggle : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Graphic graphic;
        [SerializeField] private bool isOn;

        public Action<bool> OnValueChanged;

        public bool IsOn
        {
            set
            {
                isOn = value;
                UpdateGraphic();
                OnValueChanged?.Invoke(isOn);
            }
            get
            {
                return isOn;
            }
        }

        public void ForceUpdate(bool isOn)
        {
            this.isOn = isOn;
            UpdateGraphic();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            IsOn = !IsOn;
        }

        private void UpdateGraphic()
        {
            graphic.enabled = isOn;
        }

        private void OnValidate()
        {
            UpdateGraphic();
        }
    }
}
