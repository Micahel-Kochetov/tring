using Assets.Scripts.States.Common.Controller;
using Assets.Scripts.States.GetYourVideos.View;
using Zenject;

namespace Assets.Scripts.States.GetYourVideos.Controller
{
    public class SendVideosConfirmationController : AnalyticsController
    {
        public event System.Action OnDeactivated;
        [Inject]
        SendVideosConfirmationView view;

        public override void Init(string screenID)
        {
            base.Init(screenID);
            view.Init();
            view.OnFinish += OnFinishHandler;
            view.OnBackToRingsSelector += OnBackToRingsSelectorHandler;
        }

        public override void Dispose()
        {
            view.Dispose();
            view.OnFinish -= OnFinishHandler;
            view.OnBackToRingsSelector -= OnBackToRingsSelectorHandler;
        }

        public override void Activate()
        {
            view.Show();
        }

        public override void Deactivate()
        {
            view.Hide();
            OnDeactivated?.Invoke();
        }

        private void OnFinishHandler()
        {
            analyticsService.FinishSession(false);
            Main.Instance.SetState(CoreStates.EStateType.StartTheMagic);
        }

        private void OnBackToRingsSelectorHandler()
        {
            Main.Instance.SetState(CoreStates.EStateType.ARRing);
        }
    }
}
