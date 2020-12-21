using Assets.Scripts.States.Common.Controller;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{
    public class InputSwipeListenerService : ITickable, IInitializable, IDisposable
    {
        public event Action OnSwipe;
        InputTouchListener touchListener;
        readonly float delta = 0.05f;
        readonly float minSwipeLength;

        InputSwipeListenerService()
        {
            minSwipeLength = (new Vector2(Screen.width, Screen.height) * delta).magnitude;
        }

        public void Dispose()
        {
            touchListener.OnTouch -= OnTouchHandler;
        }

        public void Initialize()
        {
            touchListener = new InputTouchListener();
            touchListener.OnTouch += OnTouchHandler;
        }

        private void OnTouchHandler(Vector2 arg1, Vector2 arg2)
        {
            if ((arg1 - arg2).magnitude > minSwipeLength)
            {
                OnSwipe?.Invoke();
            }
        }

        public void Tick()
        {
            touchListener?.Update();
        }
    }
}
