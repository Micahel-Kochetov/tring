using Assets.Scripts.States.ARRing.Controller;
using Assets.Scripts.States.ARRing.View;
using Assets.Scripts.States.GetYourVideos.Controller;
using Assets.Scripts.States.GetYourVideos.View;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI.Installers
{
    public class GetVideosInstaller : MonoInstaller
    {
        [SerializeField]
        GetYourVideosView getYourVideosView;
        [SerializeField]
        ShareVideosView shareVideosView;
        [SerializeField]
        SendVideosConfirmationView sendVideosConfirmationView;
        [SerializeField]
        StopTheFunView stopTheFunView;

        public override void InstallBindings()
        {
            Container.Bind<GetYourVideosView>().FromInstance(getYourVideosView).AsSingle();
            Container.Bind<ShareVideosView>().FromInstance(shareVideosView).AsSingle();
            Container.Bind<SendVideosConfirmationView>().FromInstance(sendVideosConfirmationView).AsSingle();
            Container.Bind<StopTheFunView>().FromInstance(stopTheFunView).AsSingle();
            Container.BindInterfacesAndSelfTo<GetYourVideosController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SendVideosConfirmationController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShareVideosController>().AsSingle();
            Container.Bind<StopTheFunController>().AsSingle();
        }
    }
}
