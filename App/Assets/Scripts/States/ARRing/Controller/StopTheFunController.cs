using Assets.Scripts.States.ARRing.View;
using Assets.Scripts.States.Common.Interface;
using System;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class StopTheFunController
    {
        public event Action OnStopTheFun;
        [Inject]
        StopTheFunView view;
        [Inject]
        IAnalyticsService analyticsService;

        public void Init()
        {
            view.Init();
            view.OnYes += OnYesHandler;
            view.OnNo += OnNoHandler;
        }

        public void Dispose()
        {
            view.Dispose();
            view.OnYes -= OnYesHandler;
            view.OnNo -= OnNoHandler;
        }

        public void Activate()
        {
            view.Show();
        }

        public void Deactivate()
        {
            view.Hide();
        }

        private void OnYesHandler()
        {
            OnStopTheFun?.Invoke();
            analyticsService.FinishSession();
            Main.Instance.SetState(CoreStates.EStateType.StartTheMagic);
        }

        private void OnNoHandler()
        {
            Deactivate();
        }
    }
}
