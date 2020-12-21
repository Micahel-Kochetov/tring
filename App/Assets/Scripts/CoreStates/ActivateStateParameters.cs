namespace Assets.Scripts.CoreStates
{
    public class ActivateStateParameters
    {
        public bool LoadScene;
        public EStateType PreviousStateType;
        public object[] Parameters;

        public ActivateStateParameters(bool loadScene, EStateType previousState,
            object[] parameters = null)
        {
            LoadScene = loadScene;
            PreviousStateType = previousState;
            Parameters = parameters;
        }

        public ActivateStateParameters(bool loadScene, object[] parameters = null)
        {
            LoadScene = loadScene;
            Parameters = parameters;
        }

        public ActivateStateParameters(object[] parameters)
        {
            LoadScene = true;
            Parameters = parameters;
        }
    }
}
