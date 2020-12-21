using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenExternalLink : MonoBehaviour {

    public string url = "";
    public string urlFallback = "";
    //[SerializeField] List<ShowModel> models;
    //public GameObject currentItem;

    public void OnButtonBuyClick()
    {
        //url = ControlButton.curObj.GetComponent<RingInfo>().shopifyURL;
        if (string.IsNullOrEmpty(url))
        {
            Debug.Log(transform.name + " opening URL(urlFallback): " + urlFallback);
            Application.OpenURL(urlFallback);
        }
        else
        {
            Debug.Log(transform.name + " opening URL: " + url);
            Application.OpenURL(url);
        }

        //if (currentItem != null)
        //    Application.OpenURL(currentItem.GetComponent<RingInfo>().shopifyURL);
        //else
        //    Application.OpenURL(url);
    }

    //string GetUrl()
    //{
    //    string currentURL = null;
    //    foreach (ShowModel model in models)
    //    {
    //        if (model.handBlendObj.gameObject.active)
    //            currentURL = model.GetCurrentInfo().shopifyURL;
    //    }
    //    return currentURL;
    //}
}
