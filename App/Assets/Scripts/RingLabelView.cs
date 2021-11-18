using Assets.Scripts.States.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingLabelView : MonoBehaviour
{
    [SerializeField]
    private Image logoImage;

    [SerializeField]
    private Text priceLabel;

    [SerializeField]
    private float fadingSpeed = 1f;

    [SerializeField]
    private ApplicationSettingsSO applicationSettingsSO;

    private Coroutine fadeInCoroutine;

    private Coroutine fadeOutCoroutine;

    public string Price { get; set; }

    public Sprite Logo { get; set; }

    public void ShowRingValues(int ringId)
    {
        this.SetRingValues(ringId);
        this.logoImage.sprite = this.Logo;
        this.priceLabel.text = this.Price;
    }

    public void SetRingValues(int ringId)
    {
        var id = ringId % this.applicationSettingsSO.RingsSetConfigSO.RingModelDatas.Count;
        var ring = this.applicationSettingsSO.RingsSetConfigSO.RingModelDatas[id];
        this.Price = ring.Price.ToString();
        this.Logo = ring.Logo;
    }

    public void FadeInAndFadeOut(int ringId)
    {
        this.SetRingValues(ringId);
        this.StartFadeIn(this.StartFadeOut);
    }

    private void StartFadeOut()
    {
        this.fadeOutCoroutine = this.StartCoroutine(this.FadeOut());
    }

    private void StartFadeIn(Action onFadedIn)
    {
        this.StopFadeOut();
        this.fadeInCoroutine = this.StartCoroutine(this.FadeIn(onFadedIn));
    }

    private void StopFadeOut()
    {
        if (this.fadeOutCoroutine != null)
        {
            this.StopCoroutine(this.fadeOutCoroutine);
            this.fadeOutCoroutine = null;
        }
    }

    private IEnumerator FadeIn(Action onFadingInEnded = null)
    {
        for (float fadingIndex = fadingSpeed * this.priceLabel.color.a; fadingIndex >= 0; fadingIndex -= Time.deltaTime)
        {
            this.SetAlpha(fadingIndex / fadingSpeed);
            yield return null;
        }

        this.fadeInCoroutine = null;
        onFadingInEnded?.Invoke();
    }

    private IEnumerator FadeOut()
    {
        while (this.fadeInCoroutine != null)
        {
            yield return null;
        }

        this.logoImage.sprite = this.Logo;
        this.priceLabel.text = this.Price;

        for (float fadingIndex = fadingSpeed * this.priceLabel.color.a; fadingIndex <= fadingSpeed; fadingIndex += Time.deltaTime)
        {
            this.SetAlpha(fadingIndex / fadingSpeed);
            yield return null;
        }

        this.fadeOutCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        this.priceLabel.color = new Color(this.priceLabel.color.r, this.priceLabel.color.g, this.priceLabel.color.b, alpha);
        this.logoImage.color = new Color(this.logoImage.color.r, this.logoImage.color.g, this.logoImage.color.b, alpha);
    }

}
