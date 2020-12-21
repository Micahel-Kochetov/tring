using Assets.Scripts;
using UnityEngine;
using Vuforia;

public class AllGUi : MonoBehaviour {

    public GameObject canvas;
    public GameObject skinPanel;
    public GameObject optionPanel;
    public GameObject torchButton;
    public Sprite[] torchImage;

    private GameObject tmpObj = null;
    public GameObject[] trackingObjs;
    public GameObject[] toneButtons;

    private ScreenOrientation prevOrt = ScreenOrientation.Landscape;
    private bool isTorch = false;
    private int curTone = 0;

    [SerializeField] GameObject defaultRing;
    [SerializeField] GameObject defaultBracelet;
    [SerializeField] GameObject braceletBut;
    [SerializeField] GameObject ringBut;

    ScrollView scrollView;
    [SerializeField]
    GameObject ringScroll;
    [SerializeField]
    GameObject bracerScroll;

    bool skinDoubleklicked = false;
    bool firstShowbracelet, firstShowRing = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (skinDoubleklicked)
        {
            if (defaultBracelet.activeSelf && firstShowbracelet == false)
            {
                SkinDoubleKlickedChecker();
                firstShowbracelet = true;
            }
            else if (defaultRing.activeSelf && firstShowRing == false)
            {
                SkinDoubleKlickedChecker();
                firstShowRing = true;
            }
        }
    }

    //if skin double klicked make default obj swipe 
    void SkinDoubleKlickedChecker()
    {
        skinPanel.SetActive(false);
        optionPanel.SetActive(true);

        //button bracelet or ring  activated after double klick
        if (defaultRing.activeSelf)
        {
            Debug.Log("----------RING ACTIVE ------------");
            ringBut.SetActive(true);
            OnSelectButton2(1);
            scrollView = ringScroll.GetComponent<ScrollView>();
            scrollView.OnSelectButton(1);
        }
        else if (defaultBracelet.activeSelf)
        {
            Debug.Log("braceletBut.SetActive(true)");
            braceletBut.SetActive(true);
            OnSelectButton2(0);
            scrollView = bracerScroll.GetComponent<ScrollView>();
            scrollView.OnSelectButton(1);
        }
        else 
        {
            if (GlobalScript.trackingObjIndex == 0)
            {
                if (defaultBracelet.activeSelf)
                {
                    OnSelectButton2(0);
                }
                braceletBut.SetActive(true);
            }
            else if (GlobalScript.trackingObjIndex == 1)
            {
                if (defaultRing.activeSelf)
                {
                    OnSelectButton2(1);
                }
                ringBut.SetActive(true);
            }
            else
            {
            }
        }
    }
    public void PressedToneButton(int cmd)
    {
        if(curTone == cmd)
        {
            skinDoubleklicked = true;
            SkinDoubleKlickedChecker();
        }
        else
        {
            Transform ts;
            ts = toneButtons[curTone].transform.Find("Check");
            ts.gameObject.SetActive(false);
            ts = toneButtons[cmd].transform.Find("Check");
            ts.gameObject.SetActive(true);
            curTone = cmd;
            tmpObj = toneButtons[cmd];
            Color c = new Color(tmpObj.GetComponent<UnityEngine.UI.Image>().color.r,
                                    tmpObj.GetComponent<UnityEngine.UI.Image>().color.g,
                                    tmpObj.GetComponent<UnityEngine.UI.Image>().color.b,
                                    1);
            GameObject.Find("Global").GetComponent<GlobalScript>().ChangeToneColor(c);
        }
    }

    public void OnSelectButton(int cmd)
    {
        Debug.Log("---------onSelect1----------");
        Debug.Log(GlobalScript.trackingObjIndex);
        if (GlobalScript.trackingObjIndex == 0)
        {
            trackingObjs[0].GetComponent<CustomTrackableEventHandler>().Show();
            if (ControlButton.curObj == null)
                return;
            if (GlobalScript.СurSel == cmd)
            {
                ControlButton.curObj.SendMessage("ChangeColor", true);
            }
            else
            {
                GlobalScript.СurSel = cmd;
                ControlButton.curObj.SendMessage("ShowSelectedModel", cmd);
            }
        }
        else if (GlobalScript.trackingObjIndex == 1)
        {
            trackingObjs[1].GetComponent<CustomTrackableEventHandler>().Show();

            if (ControlButton.curObj == null)
                return;
            if (GlobalScript.СurSel == cmd)
                ControlButton.curObj.SendMessage("ChangeColor", true);
            else
            {
                GlobalScript.СurSel = cmd;
                ControlButton.curObj.SendMessage("ShowSelectedModel", cmd);
            }

        }
    }

    public void OnSelectButton2(int cmd)
    {
        Debug.Log("---------onSelect2----------");
        Debug.Log(GlobalScript.trackingObjIndex);
        if (GlobalScript.trackingObjIndex == 0)
        {
            trackingObjs[0].GetComponent<CustomTrackableEventHandler>().Show();
            if (ControlButton.curObj == null)
                return;
            else
            {
                GlobalScript.СurSel = cmd;
                ControlButton.curObj.SendMessage("ShowSelectedModel", cmd);
            }
        }
        else if (GlobalScript.trackingObjIndex == 1)
        {
            trackingObjs[1].GetComponent<CustomTrackableEventHandler>().Show();

            if (ControlButton.curObj == null)
                return;
            else
            {
                GlobalScript.СurSel = cmd;
                ControlButton.curObj.SendMessage("ShowSelectedModel", cmd);
            }

        }
    }

    public void OnColorButton()
    {
        return;
        firstShowRing = false;
        firstShowbracelet = false;
        skinDoubleklicked = false;
        braceletBut.SetActive(false);
        ringBut.SetActive(false);
        skinPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void OnTorchButton()
    {
        isTorch = !isTorch;
        CameraDevice.Instance.SetFlashTorchMode(isTorch);
        torchButton.GetComponent<UnityEngine.UI.Image>().sprite = torchImage[isTorch ? 1 : 0];
    }
}
