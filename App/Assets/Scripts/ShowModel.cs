using UnityEngine;
using System.Collections;

public class ShowModel : MonoBehaviour
{
    public GameObject[] models;
    public OpenExternalLink buyButton;
    public int idx = 0;
    public int matIdx = 0;

    public GameObject handBlendObj;


    public void ShowSelectedModel(bool direction)
    {
        if (direction == true)
        {   //next
            idx++;
            if (idx >= models.Length)
                idx = 0;
        }
        else
        {                   //prev
            idx--;
            if (idx < 0)
                idx = models.Length - 1;
        }

        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }

        //models [idx].SetActive (true);
        ShowItem(idx);
    }

    public void ShowSelectedModel(int index)
    {
        idx = index;
        for (int i = 0; i < models.Length; i++)
            models[i].SetActive(false);
        //models[idx].SetActive(true);
        ShowItem(idx);
    }

    public void ShowCurrentModel()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }

        //models [idx].SetActive (true);
        ShowItem(idx);
    }


    public void HideSelectedModel()
    {
        for (int i = 0; i < models.Length; i++)
        {
            //models [i].SetActive (false);
            HideItem(i);
        }
    }

    public void ChangeColor(bool direction)
    {
        if (direction == true)
        {   //next
            matIdx++;
            if (matIdx >= 3)
                matIdx = 0;
        }
        else
        {                   //prev
            matIdx--;
            if (matIdx < 0)
                matIdx = 2;
        }

        models[idx].GetComponent<RingInfo>().Change(matIdx);
    }

    public void ShowHideHand(bool isShow)
    {
        handBlendObj.SetActive(isShow);
    }

    void ShowItem(int idx)
    {
        models[idx].SetActive(true);
        buyButton.url = models[idx].GetComponent<RingInfo>().shopifyURL;
        //buyButton.transform.gameObject.SetActive(true);
    }

    void HideItem(int idx)
    {
        models[idx].SetActive(false);
        //buyButton.url = "";
        //buyButton.currentItem = null;
        //buyButton.transform.gameObject.SetActive(false);
    }
}
