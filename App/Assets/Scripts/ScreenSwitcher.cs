using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour {


    public GameObject tabletPanel;
    public GameObject mobilePanel;

    private void Awake()
    {
        if (SystemInfo.deviceModel.Contains("iPad"))
        {
            tabletPanel.SetActive(true);
            mobilePanel.SetActive(false);
        }
        else
        {

            tabletPanel.SetActive(false);
            mobilePanel.SetActive(true);
        }
    }
}
