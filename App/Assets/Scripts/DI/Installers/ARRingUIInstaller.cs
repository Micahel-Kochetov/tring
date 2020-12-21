using Assets.Scripts.States.ARRing.Controller;
using Assets.Scripts.States.ARRing.View;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI.Installers
{
    public class ARRingUIInstaller : MonoInstaller
    {
        [SerializeField]
        ARRingView arringView;
        [SerializeField]
        GetVoucherView getVoucherView;
        [SerializeField]
        StopTheFunView stopTheFunView;
        [SerializeField]
        RecordVideoView recordVideoView;
        [SerializeField]
        PreshareView preshareView;

        public override void InstallBindings()
        {
            Container.Bind<ARRingView>().FromInstance(arringView).AsSingle();
            Container.Bind<GetVoucherView>().FromInstance(getVoucherView).AsSingle();
            Container.Bind<StopTheFunView>().FromInstance(stopTheFunView).AsSingle();
            Container.Bind<RecordVideoView>().FromInstance(recordVideoView).AsSingle();
            Container.Bind<PreshareView>().FromInstance(preshareView).AsSingle();
            Container.BindInterfacesAndSelfTo<RecordVideoController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ARRingController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PreshareController>().AsSingle();
            Container.Bind<GetVoucherController>().AsSingle();
            Container.Bind<StopTheFunController>().AsSingle();
        }

    }
}
