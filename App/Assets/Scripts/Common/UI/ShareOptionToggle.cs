using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI
{
    public class ShareOptionToggle : MonoBehaviour, IPointerClickHandler
    {
        [Serializable] public class ToggleEvent : UnityEvent<bool> { }

        private static Color enabledIconColor = new Color(1, 1, 1, 1);
        private static Color disabledIconColor = new Color(1, 1, 1, .5f);
        private static Color enabledTextColor = new Color(1, 1, 1, 1);
        private static Color disabledTextColor = new Color(0, 0, 0, .5f);

        [SerializeField] private Graphic icon;
        [SerializeField] private Graphic text;
        [SerializeField] private bool isOn;

        public ToggleEvent OnValueChanged;

        public bool IsOn
        {
            get
            {
                return isOn;
            }
        }

        public Color IconColor
        {
            set
            {
                icon.color = value;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isOn = !isOn;
            UpdateGraphics();

            OnValueChanged?.Invoke(isOn);
        }

        private void OnValidate()
        {
            UpdateGraphics();
        }

        public void ForceSet(bool isOn)
        {
            this.isOn = isOn;
            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            icon.color = IsOn ? enabledIconColor : disabledIconColor;
            text.color = IsOn ? enabledTextColor : disabledTextColor;
        }
    }
}
