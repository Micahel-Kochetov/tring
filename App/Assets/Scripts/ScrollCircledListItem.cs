using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollCircledListItem : MonoBehaviour
{
    [Serializable] private class ScrollItemChanged : UnityEvent<int> { }

    [SerializeField] private int index;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image itemImage;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Vector2 minSize;
    [SerializeField] private Vector2 maxSize;

    public event Action<int> OnScrollItemChanged;

        //#if UNITY_EDITOR
    /// <summary>
    /// Only Editor feature! Setup Item with new data.
    /// </summary>
    public void SetupItem(int index, Sprite sprite)
    {
        this.index = index;
        itemImage.sprite = sprite;
    }
//#endif

    public void OnToggleChanged()
    {
        if(toggle.isOn)
        {
            rectTransform.sizeDelta = maxSize;
            OnScrollItemChanged?.Invoke(index);
        }
        else
        {
            rectTransform.sizeDelta = minSize;
        }
    }
}
