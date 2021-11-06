using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class SwipeController : ITickable
    {
        public event Action OnSwipeRight;
        public event Action OnSwipeLeft;
        public event Action OnSwipeUp;
        public event Action OnSwipeDown;
        public Transform[] ring;
        public int RingsCount;
        private bool isControl = false;
        private int prevTouchCount = 0;
        private Vector2 prevTouchPos0, prevTouchPos1;
        //inside class
        Vector2 firstPressPos;
        Vector2 secondPressPos;
        Vector2 prevPos;
        Vector2 currentSwipe;

        // Update is called once per frame
        void Update()
        {
            //Scale();
            SwipeAndRotate();
        }


        public void Scale()
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                if (prevTouchCount != 2)
                {
                    prevTouchPos0 = touch0.position;
                    prevTouchPos1 = touch1.position;
                }
                float scale = Mathf.Abs(touch0.position.x - touch1.position.x) / Mathf.Abs(prevTouchPos0.x - prevTouchPos1.x);
                //if (ControlButton.curObj != null)
                //    ControlButton.curObj.transform.localScale = new Vector3(scale, scale, scale);
                ring[0].transform.localScale = new Vector3(scale, scale, scale);
                ring[1].transform.localScale = new Vector3(scale, scale, scale);
            }
            prevTouchCount = Input.touchCount;
        }


        public void SwipeAndRotate()
        {
            bool isUIOverride = UnityEngine.EventSystems.EventSystem.current!= null? UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(): false;
            if (!isUIOverride && Input.GetMouseButtonDown(0))
            {
                //save began touch 2d point
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                prevPos = Input.mousePosition;
                if (firstPressPos.y > Screen.height * 0.15f && firstPressPos.y < Screen.height * 0.85f)
                    isControl = true;
                else
                    isControl = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (!isControl)
                    return;
                isControl = false;
                //save ended touch 2d point
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                if (currentSwipe.magnitude < 100)
                    return;

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && Mathf.Abs(currentSwipe.x)<MIN_SWIPE_DELTA)
                {
                    OnSwipeUp?.Invoke();
                }
                //swipe down
                if (currentSwipe.y < 0 && Mathf.Abs(currentSwipe.x)<MIN_SWIPE_DELTA)
                {
                    OnSwipeDown?.Invoke();
                }
                //swipe left
                if (currentSwipe.x < 0 && Mathf.Abs(currentSwipe.y)<MIN_SWIPE_DELTA)
                {
                    OnSwipeRight?.Invoke();
                }
                //swipe right
                if (currentSwipe.x > 0 && Mathf.Abs(currentSwipe.y)<MIN_SWIPE_DELTA)
                {
                    OnSwipeLeft?.Invoke();
                }
            }

        }

        private readonly float MIN_SWIPE_DELTA = 0.5f;

        public void Tick()
        {
            Update();
        }
    }
}
