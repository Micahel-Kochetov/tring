using Assets.Scripts.ColorCorrection;
using Assets.Scripts.States.ARRing.Controller;
using Assets.Scripts.States.ARRing.View;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI.Installers
{
    class ARRing3DInstaller : MonoInstaller
    {
        [SerializeField]
        VuforiaMarkersView vuforiaMarkersView;
        [SerializeField]
        ShowRingView showRingView;
        [SerializeField]
        SkinColorMonitor skinColorMonitor;

        public override void InstallBindings()
        {
            Container.Bind<VuforiaMarkersView>().FromInstance(vuforiaMarkersView).AsSingle();
            Container.Bind<SkinColorMonitor>().FromInstance(skinColorMonitor).AsSingle();
            Container.Bind<VuforiaMarkersController>().AsSingle();
            Container.Bind<ShowRingView>().FromInstance(showRingView).AsSingle();
            Container.Bind<ShowRingController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SwipeController>().AsSingle();
        }
    }
}
