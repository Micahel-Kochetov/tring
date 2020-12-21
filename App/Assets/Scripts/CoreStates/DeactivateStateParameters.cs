namespace Assets.Scripts.CoreStates
{
    public class DeactivateStateParameters
    {
        public EStateType NextStateType;
        public object[] Parameters;
        public bool UnloadScene;

        public DeactivateStateParameters(EStateType nextState, bool unloadScene = true, object[] parameters = null)
        {
            NextStateType = nextState;
            Parameters = parameters;
            UnloadScene = unloadScene;
        }
    }
}
