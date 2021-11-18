using Assets.Scripts.Common;
using Assets.Scripts.DI.Factories;
using Assets.Scripts.FirebaseSDK.Storage;
using Assets.Scripts.States.Common.Service;
using Assets.Scripts.States.Common.View;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI.Installers
{
    public class ProjectServicesInstaller : MonoInstaller
    {
        [SerializeField]
        private CoroutineHandlerService coroutineStarterservice;
        [SerializeField]
        private ApplicationFocusService appFocusService;

        public override void InstallBindings()
        {
            Container.Bind<AppStatesFactory>().AsSingle();
            Container.Bind<CoroutineHandlerService>().FromInstance(coroutineStarterservice).AsTransient();
            Container.Bind<StreamingAssetsReaderService>().AsTransient();
            Container.Bind<ViewManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<UserSessionService>().AsSingle();
            Container.Bind<EmailSenderService>().AsSingle();
            Container.Bind<EmailShareFinalizeService>().AsSingle();
            Container.Bind<WhatsAppShareFinalizeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<FileUploadService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenRecordingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSwipeListenerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputTapListenerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenRecordingPermissionService>().AsSingle().NonLazy();
            Container.Bind<ApplicationFocusService>().FromInstance(appFocusService).AsSingle();
            Container.Bind<BulldogService>().AsSingle();
        }
    }
}
