namespace Assets.Scripts.CoreStates
{
    public class BaseStateData
    {
        public BaseStateData(SceneData sceneData3D, SceneData sceneDataUI)
        {
            SceneData3D = sceneData3D;
            SceneDataUI = sceneDataUI;
        }

        public SceneData SceneData3D { get; private set; }

        public SceneData SceneDataUI { get; set; }
    }
}
