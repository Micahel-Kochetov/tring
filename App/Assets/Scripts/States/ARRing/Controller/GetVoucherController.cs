using Assets.Scripts.States.ARRing.View;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class GetVoucherController
    {
        [Inject]
        GetVoucherView view;

        public void Init()
        {
            view.Init();
            view.OnClose += OnCloseHandler;
        }

        public void Dispose()
        {
            view.Dispose();
            view.OnClose -= OnCloseHandler;

        }

        public void Activate(Sprite selectedRingSprite)
        {
            view.Show();
            view.RingImage = selectedRingSprite;
        }

        public void Deactivate()
        {
            view.Hide();
        }

        private void OnCloseHandler()
        {
            Deactivate();
        }
    }
}
