using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollCircledListController : MonoBehaviour, IPointerDownHandler
{
    [Header("Layout Settings")]
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform contentHolder;
    [SerializeField] private RectTransform contentPosCorrection;
    [SerializeField] private ScrollCircledListItem[] items;
    [SerializeField] private RectTransform[] itemRectTransforms;
    [SerializeField] private Toggle[] itemToggles;
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    [Tooltip("Width of small item plus spacing")]
    [SerializeField] private float itemXOffset;

    [Header("Centering Settings")]
    [SerializeField] private RectTransform centerHolder;
    [SerializeField] private RectTransform centerPoint;
    [SerializeField] private float scrollToIndexDuration;
    [SerializeField] private AnimationCurve scrollToIndexCurve;

    public event Action<int> OnScrollItemChanged;

    private float itemStep;
    private int itemSelectedIndex;
    private int itemRightIndex;

    private bool isAutoCentering;

    private float startX;
    private float targetX;

    private float velocityX;

    private float prevX;
    private float startTime;

    private float layoutDelta;

    private void Start()
    {
        itemSelectedIndex = 0;
        itemRightIndex = itemRectTransforms.Length / 2;
        float itemWidth = itemRectTransforms[1].sizeDelta.x;
        itemStep = horizontalLayoutGroup.spacing + itemWidth;
        prevX = contentHolder.anchoredPosition.x;

        foreach(var item in items)
        {
            item.OnScrollItemChanged += HandleScrollItemChanged;
        }
    }

    public void HandleScrollChanged()
    {
        UpdateItemsLayout(contentHolder.anchoredPosition.x - prevX);
        prevX = contentHolder.anchoredPosition.x;
    }

    public void HandleScrollItemChanged(int index)
    {
        itemSelectedIndex = index;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentPosCorrection);
        ScrollToIndex(itemSelectedIndex);
        OnScrollItemChanged?.Invoke(index);
    }

    public void ForceSelectIndex(int index)
    {
        itemToggles[index].isOn = true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isAutoCentering = false;
    }

    private void Update()
    {
        if (isAutoCentering)
        {
            float normalizedTime = (Time.time - startTime) / scrollToIndexDuration;

            contentHolder.anchoredPosition = new Vector2(Mathf.Lerp(startX, targetX, scrollToIndexCurve.Evaluate(normalizedTime)), contentHolder.anchoredPosition.y);

            UpdateItemsLayout(contentHolder.anchoredPosition.x - prevX);
            prevX = contentHolder.anchoredPosition.x;

            if (normalizedTime >= 1)
            {
                isAutoCentering = false;
            }
        }
    }

    private void UpdateItemsLayout(float deltaX)
    {
        layoutDelta += deltaX;
        int deltaIndex = (int)(layoutDelta / itemStep);
        if (deltaIndex != 0)
        {
            layoutDelta %= itemStep;

            float curOffset = itemXOffset * deltaIndex;
            contentPosCorrection.anchoredPosition -= new Vector2(curOffset, 0);
            if(isAutoCentering)
            {
                //startX -= curOffset;
                //targetX -= curOffset;
            }

            if (deltaIndex > 0)
            {
                //if deltaIndex positive it means we need move elements from right to left
                int index = itemRightIndex;
                //Vector2 leftPos = itemRectTransforms[(itemRightIndex + 1) % itemRectTransforms.Length].anchoredPosition;
                for (int i = 0; i < deltaIndex; i++)
                {
                    //leftPos -= new Vector2(itemStep, 0);
                    //itemRectTransforms[index].anchoredPosition = leftPos;
                    itemRectTransforms[index].SetAsFirstSibling();
                    index--;
                    if (index < 0) index = itemRectTransforms.Length - 1;
                }
                itemRightIndex -= deltaIndex;
                if (itemRightIndex < 0) itemRightIndex += itemRectTransforms.Length;
            }
            else
            {
                //if deltaIndex negative it means we need move elements from left to right
                int index = (itemRightIndex + 1) % itemRectTransforms.Length;
                //Vector2 rightPos = itemRectTransforms[itemRightIndex].anchoredPosition;
                for (int i = 0; i > deltaIndex; i--)
                {
                    //rightPos += new Vector2(itemStep, 0);
                    //itemRectTransforms[index].anchoredPosition = rightPos;
                    itemRectTransforms[index].SetAsLastSibling();
                    index++;
                    if (index >= itemRectTransforms.Length) index = 0;
                }
                itemRightIndex -= deltaIndex;
                itemRightIndex %= itemRectTransforms.Length;
            }
        }
    }

    private void ScrollToIndex(int index)
    {
        isAutoCentering = true;
        startX = contentHolder.anchoredPosition.x;
        prevX = startX;
        centerPoint.SetParent(contentPosCorrection, true);
        targetX = startX + centerPoint.anchoredPosition.x - itemRectTransforms[index].anchoredPosition.x;
        centerPoint.SetParent(centerHolder, true);
        startTime = Time.time;
    }

    //here is feture to configure list automaticly
#if UNITY_EDITOR

    [Header("Only editor features")]
    /// <summary>
    /// Only editor feature! Template item.
    /// </summary>
    public ScrollCircledListItem templateRingItem;
    /// <summary>
    /// Only editor feature! List of sprites for rings to configure.
    /// </summary>
    public Sprite[] ringsSprites;

    public void UpdateList()
    {
        //remove old ring items
        int childCount = contentPosCorrection.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(contentPosCorrection.GetChild(0).gameObject);
        }

        //create new ring items
        int ringsCount = ringsSprites.Length;
        int centerIndex = ringsCount / 2;
        int index = centerIndex;

        items = new ScrollCircledListItem[ringsCount];
        itemRectTransforms = new RectTransform[ringsCount];
        itemToggles = new Toggle[ringsCount];

        ToggleGroup toggleGroup = contentPosCorrection.GetComponent<ToggleGroup>();

        do
        {
            ScrollCircledListItem newItem = (ScrollCircledListItem)UnityEditor.PrefabUtility.InstantiatePrefab(templateRingItem);
            newItem.transform.SetParent(contentPosCorrection, false);
            newItem.name = "RingItem " + index;
            newItem.SetupItem(index, ringsSprites[index]);

            //newItem.transform.localScale = index == 0 ? new Vector3(1f, 1f, 1f) : new Vector3(.76f, .76f, 1f);
            RectTransform rectT = newItem.GetComponent<RectTransform>();
            if (index != 0)
            {
                rectT.sizeDelta = new Vector2(160f, 160f);
            }

            Toggle toggle = newItem.GetComponent<Toggle>();
            toggle.isOn = index == 0;
            toggle.group = toggleGroup;

            items[index] = newItem;
            itemRectTransforms[index] = rectT;
            itemToggles[index] = toggle;

            index = (index + 1) % ringsCount;
        }
        while (index != centerIndex);

        //update layout

        LayoutRebuilder.ForceRebuildLayoutImmediate(contentPosCorrection);

        contentPosCorrection.anchoredPosition = Vector2.zero;
        contentPosCorrection.position += centerPoint.position - itemRectTransforms[0].position;
    }
#endif
}
