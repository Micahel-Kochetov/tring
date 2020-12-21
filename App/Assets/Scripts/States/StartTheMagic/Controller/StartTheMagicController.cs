using Assets.Scripts.States.Common.Interface;
using Assets.Scripts.States.Common.Service;
using Assets.Scripts.States.StartTheMagic.View;
using Zenject;

namespace Assets.Scripts.States.StartTheMagic.Controller
{
    public class StartTheMagicController
    {
        [Inject]
        StartTheMagicView view;
        [Inject]
        UserSessionService userSessionService;
        [Inject]
        IAnalyticsService analyticsService;

        public void Activate()
        {
            view.Init();
            view.Show();
            view.OnStartTheMagic += OnStartTheMagicHandler;
        }

        public void Deactivate()
        {
            view.Hide();
            view.OnStartTheMagic -= OnStartTheMagicHandler;
            view.Dispose();
        }

        private void OnStartTheMagicHandler()
        {
            userSessionService.StartSession();
            analyticsService.StartSession();
            analyticsService.SetSessionID(userSessionService.SessionName);
            Main.Instance.SetState(CoreStates.EStateType.ARRing);
        }
    }
}
