namespace Assets.Scripts.CoreStates
{
    public class SceneData
    {
        public string SceneName { get; private set; }
        public string ContextGoName { get; private set; }

        public SceneData(string sceneName, string contextGoName)
        {
            SceneName = sceneName;
            ContextGoName = contextGoName;
        }

        public SceneData(string contextGoName)
        {
            ContextGoName = contextGoName;
        }
    }
}
