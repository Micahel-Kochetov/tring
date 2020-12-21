namespace Assets.Scripts.Common.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public static class UIExtensions
    {
        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }

        public static void SetZeroPaddings(this RectTransform rt)
        {
            SetLeft(rt, 0);
            SetRight(rt, 0);
            SetTop(rt, 0);
            SetBottom(rt, 0);
        }

        public static void SetHeight(this RectTransform rt, float height)
        {
            var sizeDelta = rt.sizeDelta;
            sizeDelta.y = height;
            rt.sizeDelta = sizeDelta;
        }

        public static void SetWidth(this RectTransform rt, float width)
        {
            var sizeDelta = rt.sizeDelta;
            sizeDelta.x = width;
            rt.sizeDelta = sizeDelta;
        }

        public static void Refresh(this LayoutGroup layoutGroup)
        {
            layoutGroup.CalculateLayoutInputHorizontal();
            layoutGroup.CalculateLayoutInputVertical();
            layoutGroup.SetLayoutHorizontal();
            layoutGroup.SetLayoutVertical();
        }
    }
}
