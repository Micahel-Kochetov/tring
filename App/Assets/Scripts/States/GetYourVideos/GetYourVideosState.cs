using Assets.Scripts.CoreStates;
using Assets.Scripts.States.ARRing.Controller;
using Assets.Scripts.States.Common.Service;
using Assets.Scripts.States.GetYourVideos.Controller;
using Common;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.States.GetYourVideos
{
    public class GetYourVideosState : BaseState
    {

        GetYourVideosController getVideosController;
        ShareVideosController shareVideosController;
        SendVideosConfirmationController sendVideosConfirmationController;
        StopTheFunController stopTheFunController;
        [Inject]
        ScreenRecordingService screenRecordingController;

        public GetYourVideosState() : base(new BaseStateData(null,
            new SceneData("GetVideosUIContext")))
        { }

        protected override async Task Initialize(SceneContext scene3DContext, SceneContext sceneUIContext, ActivateStateParameters args = null)
        {
            await base.Initialize(scene3DContext, sceneUIContext, args);
            var videos = (string[])args.Parameters[0];
            UnityEngine.Debug.Log("videos="+videos);
            getVideosController = sceneUIContext.Container.Resolve<GetYourVideosController>();
            getVideosController.Init(videos, Constants.CGetVideosScreen);
            getVideosController.Activate();
            shareVideosController = sceneUIContext.Container.Resolve<ShareVideosController>();
            shareVideosController.Init(videos);
            sendVideosConfirmationController = sceneUIContext.Container.Resolve<SendVideosConfirmationController>();
            sendVideosConfirmationController.Init(Constants.CFinishScreen);
            stopTheFunController = sceneUIContext.Container.Resolve<StopTheFunController>();
            stopTheFunController.Init();
        }

        protected override async Task Deinitialize(DeactivateStateParameters args = null)
        {
            await base.Deinitialize(args);
            screenRecordingController.Deinitialize();
            getVideosController.Deactivate();
            getVideosController.Dispose();
            sendVideosConfirmationController.Deactivate();
            sendVideosConfirmationController.Dispose();
            stopTheFunController.Deactivate();
            stopTheFunController.Dispose();
        }
    }
}
