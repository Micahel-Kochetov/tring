using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private Main main;

        public override void InstallBindings()
        {
            Container.Bind<Main>().FromInstance(main);
        }

    }
}
