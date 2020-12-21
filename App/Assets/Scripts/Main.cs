namespace Assets.Scripts
{
    using Assets.Scripts.CoreStates;
    using Assets.Scripts.DI.Factories;
 //   using Firebase.Crashlytics;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using Zenject;

    public class Main : MonoBehaviour
    {
        private Dictionary<EStateType, BaseState> states;
        private BaseState currentState;
        private Stack<BaseState> statesStack;
        [Inject]
        private AppStatesFactory statesFactory;
        private readonly int targetFPS = 60;
        [SerializeField]
        private GameObject loadingCanvas;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Application.targetFrameRate = targetFPS;
            statesStack = new Stack<BaseState>();
            InitStates();
#if UNITY_EDITOR
            SetState(EStateType.StartTheMagic);
#else
        SetState(EStateType.StartTheMagic);
#endif
        }

        public void SetPreviousState()
        {
            SetPreviousStateRoutine();
        }

        public void SetState(EStateType stateType, DeactivateStateParameters deactivateArgs = null, ActivateStateParameters activateArgs = null)
        {
            SetStateRoutine(stateType, deactivateArgs, activateArgs);
        }

        private async Task SetStateRoutine(EStateType stateType, DeactivateStateParameters deactivateArgs = null, ActivateStateParameters activateArgs = null)
        {
            loadingCanvas.SetActive(true);
            if (currentState != null)
            {
                Task deactivateTask = null;
                await currentState.Deactivate(deactivateArgs).ContinueWith(task => deactivateTask = task);
                if (deactivateTask.IsFaulted)
                {
                    Debug.LogError(deactivateTask.Exception);
                    //Crashlytics.LogException(deactivateTask.Exception);
                }
            }
            currentState = states[stateType];
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            System.GC.Collect();
            Task activateTask = null;
            await currentState.Activate(activateArgs).ContinueWith(task => activateTask = task);
            if (activateTask.IsFaulted)
            {
                Debug.Log(activateTask.Exception);
                //Crashlytics.LogException(activateTask.Exception);
            }
            statesStack.Push(currentState);
            loadingCanvas.SetActive(false);
        }

        private void InitStates()
        {
            states = new Dictionary<EStateType, BaseState>();
            AddState(EStateType.StartTheMagic);
            AddState(EStateType.ARRing);
            AddState(EStateType.GetVideos);
        }

        private void AddState(EStateType stateType)
        {
            states.Add(stateType, statesFactory.Create(stateType));
        }

        private async Task SetPreviousStateRoutine()
        {
            if (statesStack == null || statesStack.Count < 2)
            {
                return;
            }
            loadingCanvas.SetActive(true);
            statesStack.Pop();
            BaseState previousState = statesStack.Peek();
            if (currentState != null)
            {
                await currentState.Deactivate();
            }
            currentState = previousState;
            await currentState.Activate();
            loadingCanvas.SetActive(false);
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }

        public static Main Instance { get; private set; }
    }

}
