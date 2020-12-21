using Assets.Scripts.States.StartTheMagic.Controller;
using Assets.Scripts.States.StartTheMagic.View;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI.Installers
{
    public class StartTheMagicInstaller : MonoInstaller
    {
        [SerializeField]
        StartTheMagicView startTheMagicView;

        public override void InstallBindings()
        {
            Container.Bind<StartTheMagicView>().FromInstance(startTheMagicView).AsSingle();
            Container.Bind<StartTheMagicController>().AsSingle();
        }
    }
}
