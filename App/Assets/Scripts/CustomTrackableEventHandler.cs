using System;
using System.Collections;
using UnityEngine;
using Vuforia;

namespace Assets.Scripts
{
    public class CustomTrackableEventHandler : DefaultTrackableEventHandler
    {
        public event Action OnTrackingFoundEvent;
        public event Action OnTrackingLostEvent;
        public GameObject[] typeButton;
        [SerializeField] GameObject defaultRing;
        [SerializeField] GameObject defaultBracelet;
        [SerializeField] GameObject optionnalPanel;

        protected override void OnTrackingFound()
        {
            //if (gameObject.name == "0")
            //{
            //    GlobalScript.trackingObjIndex = 0;
            //    GlobalScript.СurSel = GlobalScript.braceletSel;
            //    if (optionnalPanel.activeSelf)
            //        typeButton[0].SetActive(true);
            //    Transform content = typeButton[0].transform.Find("ScrollView/Viewport/Content");
            //    foreach (Transform child in content)
            //    {
            //        child.gameObject.SetActive(true);
            //    }
            //    Transform ts = typeButton[0].transform.Find("ScrollView");
            //    ScrollView sv = ts.GetComponent<ScrollView>();
            //    sv.isSelect = false;
            //    sv.OnScrollValueChanged();
            //}
            //else if (gameObject.name == "1")
            //{
            //    GlobalScript.trackingObjIndex = 1;
            //    //if (optionnalPanel.activeSelf)
            //    //    typeButton[1].SetActive(true);
            //    Transform content = typeButton[1].transform.Find("ScrollView/Viewport/Content");
            //    foreach (Transform child in content)
            //    {
            //        child.gameObject.SetActive(true);
            //    }
            //    Transform ts = typeButton[1].transform.Find("ScrollView");
            //    ScrollView sv = ts.GetComponent<ScrollView>();
            //    sv.isSelect = false;
            //    sv.OnScrollValueChanged();
            //}
            //transform.SendMessage("ShowHideHand", true);
            //if (GlobalScript.pickToneState == true)
            //{
            //    Show();
            //}
            //Show();
            if (OnTrackingFoundEvent != null)
            {
                OnTrackingFoundEvent.Invoke();
            }
        }

        protected override void OnTrackingLost()
        {
            //typeButton[0].SetActive(false);
            //typeButton[1].SetActive(false);
            //GlobalScript.trackingObjIndex = -1;
            //transform.SendMessage("HideSelectedModel");
            //ControlButton.curObj = null;
            //transform.SendMessage("ShowHideHand", false);
            if (OnTrackingLostEvent != null)
            {
                OnTrackingLostEvent.Invoke();
            }
        }

        public void Show()
        {
            Debug.Log("Show");
            transform.SendMessage("Init");
            transform.SendMessage("ShowCurrentModel");
            ControlButton.curObj = transform.gameObject;
        }
    }
}
