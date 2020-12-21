using Assets.Scripts.CoreStates;
using Assets.Scripts.States.StartTheMagic.Controller;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.States.StartTheMagic
{
    public class StartTheMagicState : BaseState
    {
        StartTheMagicController startTheMagicController;


        public StartTheMagicState() : base(new BaseStateData(null,
            new SceneData("StartTheMagicUIContext")))
        { }

        protected override async Task Initialize(SceneContext scene3DContext, SceneContext sceneUIContext, ActivateStateParameters args = null)
        {
            await base.Initialize(scene3DContext, sceneUIContext, args);
            startTheMagicController = sceneUIContext.Container.Resolve<StartTheMagicController>();
            startTheMagicController.Activate();
        }

        protected override async Task Deinitialize(DeactivateStateParameters args = null)
        {
            await base.Deinitialize(args);
            startTheMagicController.Deactivate();
        }

    }
}
