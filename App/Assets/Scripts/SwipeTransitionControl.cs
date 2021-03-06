﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#region Custom editor
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SwipeTransitionControl))]
public class SwipeTransitionControlEditor : Editor
{
    private SwipeTransitionControl swipeTransitionControl;

    private void OnEnable()
    {
        swipeTransitionControl = (SwipeTransitionControl)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(10);
        if (GUILayout.Button("Update Layout"))
        {
            swipeTransitionControl.UpdateLayout();
        }
    }
}
#endif
#endregion

public class SwipeTransitionControl : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private HorizontalLayoutGroup contentLayoutGroup;
    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private RectTransform[] rings;
    [SerializeField] private float ringScaleMin;
    [SerializeField] private float idleStateLength;
    [SerializeField] private float transitionStateLength;
    [SerializeField] private AnimationCurve transitionCurve;

    private Vector2 transitionStep;
    private int ringIndex;
    Coroutine animationCoroutine;

    private void Start()
    {
        RecalculateLayout();
    }

    #if UNITY_EDITOR
    public void UpdateLayout()
    {
        RecalculateLayout();
    }
    #endif

    private void RecalculateLayout()
    {
        int layoutOffest = (int)((rectTransform.rect.width - rings[0].rect.width) / 2);
        contentLayoutGroup.padding.left = layoutOffest;
        contentLayoutGroup.spacing = layoutOffest;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);

        transitionStep = new Vector2(-(rectTransform.rect.size.x + rings[0].rect.size.x) / 2, 0);
    }

    public void StartAnimationCoroutine()
    {
        animationCoroutine = StartCoroutine(Animate());
    }

    public void StopAnimationCoroutine()
    {
        StopCoroutine(animationCoroutine);
    }

    IEnumerator Animate()
    {
        int index = 0;
        float time = 0;
        int repeatCount = contentRectTransform.childCount - 1;

        Vector2 startPos = contentRectTransform.anchoredPosition;
        Vector2 currentPos = startPos;
        Vector2 nextPos = currentPos + transitionStep;

        WaitForSeconds waitIdleState = new WaitForSeconds(idleStateLength);
        while (true)
        {
            do
            {
                //wait before next transition start
                yield return waitIdleState;
                int nextRingIndex = (ringIndex + 1) % rings.Length;

                do
                {
                    //make transition
                    time += Time.deltaTime / transitionStateLength;
                    contentRectTransform.anchoredPosition = Vector2.Lerp(currentPos, nextPos, transitionCurve.Evaluate(time));

                    rings[ringIndex].localScale = Vector2.one * Mathf.Lerp(1, ringScaleMin, transitionCurve.Evaluate(time));
                    rings[nextRingIndex].localScale = Vector2.one * Mathf.Lerp(ringScaleMin, 1, transitionCurve.Evaluate(time));

                    //wait till next frame
                    yield return null;
                }
                while (time < 1);

                //reset time and find next pos
                time = 0;
                currentPos = nextPos;
                nextPos = currentPos + transitionStep;
                ringIndex = nextRingIndex;
                index++;
            }
            while (index < repeatCount);

            //restart again
            index = 0;
            currentPos = startPos;
            nextPos = currentPos + transitionStep;
            ringIndex = 0;
        }
    }
}
