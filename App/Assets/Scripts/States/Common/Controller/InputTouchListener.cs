using Common;
using System;
using UnityEngine;

namespace Assets.Scripts.States.Common.Controller
{
    public class InputTouchListener : IUpdatable
    {
        public Action<Vector2, Vector2> OnTouch;
        Vector2 touchStartPos;
        public void Update()
        {
#if !UNITY_EDITOR
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    touchStartPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended ||
                    Input.GetTouch(0).phase == TouchPhase.Canceled)
                {
                    OnTouch?.Invoke(touchStartPos, Input.GetTouch(0).position);
                }
            }
        
#else
            if (Input.GetMouseButtonDown(0))
            {
                touchStartPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnTouch?.Invoke(touchStartPos, Input.mousePosition);
            }
#endif

        }
    }
}
