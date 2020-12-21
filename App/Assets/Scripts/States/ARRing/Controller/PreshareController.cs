using Assets.Scripts.CoreStates;
using Assets.Scripts.States.ARRing.View;
using Assets.Scripts.States.Common.Controller;
using Assets.Scripts.States.Common.Service;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class PreshareController : AnalyticsController
    {
        [Inject]
        PreshareView view;
        [Inject]
        UserSessionService sessionService;
        [Inject]
        StopTheFunController stopTheFunController;
        [Inject]
        RecordVideoController recordVideoController;

        public override void Init(string id)
        {
            base.Init(id);
            view.Init();
        }

        public override void Activate()
        {
            base.Activate();
            var videos = sessionService.Videos;
            view.Show(videos);
            view.OnClose += OnCloseHandler;
            view.OnSubmit += OnSubmitHandler;
            view.OnContinueRecording += OnContinueRecordingHandler;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            view.Hide();
            view.OnClose -= OnCloseHandler;
            view.OnSubmit -= OnSubmitHandler;
            view.OnContinueRecording -= OnContinueRecordingHandler;
        }
        private void OnContinueRecordingHandler()
        {
            Deactivate();
            recordVideoController.Activate();
        }

        private void OnCloseHandler()
        {
            stopTheFunController.Activate();
        }

        private void OnSubmitHandler()
        {
            var selectedVideos = view.GetSelectedVidePathes();
            var activateParameters = new ActivateStateParameters(new object[1] { selectedVideos });
            Main.Instance.SetState(EStateType.GetVideos, activateArgs: activateParameters);
        }

        public override void Dispose()
        {
            base.Dispose();
            view.Dispose();
        }
    }
}
