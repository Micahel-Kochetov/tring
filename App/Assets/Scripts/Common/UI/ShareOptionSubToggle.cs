using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI
{
    public class ShareOptionSubToggle : MonoBehaviour, IPointerClickHandler
    {
        [Serializable] public class ToggleEvent : UnityEvent<bool> { }

        [SerializeField] private Graphic[] graphics;
        [SerializeField] private Graphic handle;
        [SerializeField] private bool isActive;
        [SerializeField] private bool isOn;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        public bool IsOn
        {
            get
            {
                return isOn;
            }
        }

        public ToggleEvent OnValueChanged;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isActive) return;
            isOn = !isOn;
            UpdateGraphics();

            OnValueChanged?.Invoke(isOn);
        }

        private void OnValidate()
        {
            if (!isActive)
            {
                isOn = false;
            }
            UpdateGraphics();
        }

        public void ForceSet(bool isActive, bool isOn)
        {
            this.isActive = isActive;
            this.isOn = isOn;
            if (!isActive)
            {
                isOn = false;
            }
            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            handle.enabled = isOn;
            foreach (var graphic in graphics)
            {
                Color color = graphic.color;
                color.a = isActive ? 1f : .5f;
                graphic.color = color;
            }
        }
    }
}
