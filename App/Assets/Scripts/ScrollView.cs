using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour {

    public int type;
    public ScrollRect scrollRect;
    public Transform scrollContent;
    public GameObject leftButton;
    public GameObject rightButton;

    public bool isSelect = false;

    private bool isLeft = false;
    private bool isRight = false;
    private ScreenOrientation prevOrt = ScreenOrientation.Unknown;

    // Use this for initialization
    void Start () {
        //scrollRect.horizontalNormalizedPosition = 1f;
        OnScrollValueChanged();
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetMouseButtonUp(0))
        {
            isLeft = isRight = false;
        }
        if (isLeft)
            scrollRect.content.localPosition = new Vector3(scrollRect.content.localPosition.x + 5f, scrollRect.content.localPosition.y, scrollRect.content.localPosition.z);
        if (isRight)
            scrollRect.content.localPosition = new Vector3(scrollRect.content.localPosition.x - 5f, scrollRect.content.localPosition.y, scrollRect.content.localPosition.z);

        //if (Screen.orientation == ScreenOrientation.Landscape && prevOrt != ScreenOrientation.Landscape)
        //{
        //    RectTransform rt = this.GetComponent<RectTransform>();
        //    if (type == 1)
        //        rt.sizeDelta = new Vector2(220, 40);
        //    scrollRect.horizontalNormalizedPosition = 1;
        //    OnScrollValueChanged();
        //}
        //if (Screen.orientation == ScreenOrientation.Portrait && prevOrt != ScreenOrientation.Portrait)
        //{
        //    RectTransform rt = this.GetComponent<RectTransform>();
        //    if (type == 1)
        //        rt.sizeDelta = new Vector2(160, 40);
        //    scrollRect.horizontalNormalizedPosition = 1;
        //    OnScrollValueChanged();
        //}
        //prevOrt = Screen.orientation;
    }

    public void OnLeftArrowButton()
    {
        isLeft = true;
    }

    public void OnRightArrowButton()
    {
        isRight = true;
    }

    public void OnScrollValueChanged()
    {
        //int size = (type == 0) ? 2 : ((Screen.orientation == ScreenOrientation.Landscape) ? 4 : 3);
        int size = (type == 0) ? 2 : 5;
        if (scrollRect.content.childCount > size)
        {
            if (scrollRect.horizontalNormalizedPosition > 0.99f)
            {
                scrollRect.horizontalNormalizedPosition = 1f;
                rightButton.SetActive(false);
                isRight = false;
            }
            else
                rightButton.SetActive(true);
            if (scrollRect.horizontalNormalizedPosition < 0.01f)
            {
                scrollRect.horizontalNormalizedPosition = 0f;
                leftButton.SetActive(false);
                isLeft = false;
            }
            else
                leftButton.SetActive(true);
        }
        else
        {
            leftButton.SetActive(false);
            rightButton.SetActive(false);
            if (scrollRect.content.childCount - (isSelect ? 1 : 0) == size)
                scrollRect.horizontalNormalizedPosition = 0f;
        }
    }

    public void OnSelectButton(int cmd)
    {
        //Transform ts = scrollContent.GetChild(cmd);
        //foreach (Transform child in scrollContent)
        //    child.gameObject.SetActive(true);
        //ts.gameObject.SetActive(false);
        isSelect = true;
    }
}
