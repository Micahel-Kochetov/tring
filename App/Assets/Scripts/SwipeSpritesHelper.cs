using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeSpritesHelper : MonoBehaviour
{
    public Image imageTarget;
    public Sprite spriteFirst;
    public Sprite spriteSecond;
    public bool isFirst;

    public void SwipeSprites()
    {
        isFirst = !isFirst;
        UpdateSprite();
    }

    public void SetSprite(bool isFirst)
    {
        this.isFirst = isFirst;
        UpdateSprite();
    }

    private void OnValidate()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        imageTarget.sprite = isFirst ? spriteFirst : spriteSecond;
    }
}
