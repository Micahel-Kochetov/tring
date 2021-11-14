using Assets.Scripts.States.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingLabelView : MonoBehaviour
{
    [SerializeField]
    private Image logo;
    [SerializeField]
    private Text priceLabel;
    [SerializeField]
    private float fadingSpeed = 1f;
    public void ShowRingInfo()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.FadeOut());
    }

    public void HideAndSetRingInfo(RingSO ring)
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.FadeInAndChangeInfo(ring));
    }

    private IEnumerator FadeInAndChangeInfo(RingSO ring)
    {
        for (float fadingIndex = fadingSpeed * this.priceLabel.color.a; fadingIndex >= 0; fadingIndex -= Time.deltaTime)
        {
            this.SetAlpha(fadingIndex / fadingSpeed);
            yield return null;
        }

        this.priceLabel.text = $"{ring.Price}$";
        this.logo.sprite = ring.Logo;
    }

    private IEnumerator FadeOut()
    {
        for (float fadingIndex = fadingSpeed * this.priceLabel.color.a; fadingIndex <= fadingSpeed; fadingIndex += Time.deltaTime)
        {
            this.SetAlpha(fadingIndex / fadingSpeed);
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        this.priceLabel.color = new Color(this.priceLabel.color.r, this.priceLabel.color.g, this.priceLabel.color.b, alpha);
        this.logo.color = new Color(this.logo.color.r, this.logo.color.g, this.logo.color.b, alpha);
    }

}
