using Assets.Scripts.ColorCorrection;
using Assets.Scripts.CoreStates;
using Assets.Scripts.States.ARRing.Controller;
using Assets.Scripts.States.Common.Service;
using Common;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Vuforia;
using Zenject;

namespace Assets.Scripts.States.ARRing
{
    class ARRingState : BaseState
    {
        VuforiaMarkersController vuforiaMarkersController;
        ShowRingController showRingsController;
        [Inject]
        ScreenRecordingService screenRecordingController;
        ARRingController arringController;
        StopTheFunController stopTheFunController;
        GetVoucherController getVoucherController;
        RecordVideoController recordVideoController;
        [Inject]
        ApplicationFocusService appFocusService;
        PreshareController preshareController;

        public ARRingState() : base(new BaseStateData(
            new SceneData("ARRing3D", "ARRing3DContext"),
            new SceneData("ARRingUIContext")))
        { }

        protected override async Task Initialize(SceneContext scene3DContext, SceneContext sceneUIContext, ActivateStateParameters args = null)
        {
            await base.Initialize(scene3DContext, sceneUIContext, args);
            vuforiaMarkersController = scene3DContext.Container.Resolve<VuforiaMarkersController>();
            showRingsController = scene3DContext.Container.Resolve<ShowRingController>();
            arringController = sceneUIContext.Container.Resolve<ARRingController>();
            vuforiaMarkersController.Init();
            vuforiaMarkersController.Activate();
            showRingsController.Init(arringController);
            var skinColorMonitor = scene3DContext.Container.Resolve<SkinColorMonitor>();
            arringController.Init(showRingsController, Constants.CARRingScreen, skinColorMonitor);
            arringController.Activate();
            //due to issues with lighting, we need to set 3d scene as active scene
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("ARRing3D"));
            stopTheFunController = sceneUIContext.Container.Resolve<StopTheFunController>();
            stopTheFunController.Init();
            getVoucherController = sceneUIContext.Container.Resolve<GetVoucherController>();
            getVoucherController.Init();
            recordVideoController = sceneUIContext.Container.Resolve<RecordVideoController>();
            recordVideoController.Init(Constants.CRecordVideoScreen);

            preshareController = sceneUIContext.Container.Resolve<PreshareController>();
            preshareController.Init(Constants.CPreshareScreen);
        }

        protected override async Task Deinitialize(DeactivateStateParameters args = null)
        {
            await base.Deinitialize(args);
            vuforiaMarkersController.Deactivate();
            vuforiaMarkersController.Dispose();
            showRingsController.Dispose();
            screenRecordingController.Deinitialize();
            arringController.Deactivate();
            arringController.Dispose();
            stopTheFunController.Deactivate();
            stopTheFunController.Dispose();
            getVoucherController.Deactivate();
            getVoucherController.Dispose();
            recordVideoController.Deactivate();
            recordVideoController.Dispose();
            preshareController.Deactivate();
            preshareController.Dispose();
        }
    }
}
