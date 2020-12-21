using Assets.Scripts.States.Common.Service;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.CoreStates
{
    public abstract class BaseState
    {
        private ActivateStateParameters args;
        protected readonly BaseStateData stateData;
        protected List<IUpdatable> controllers;
        private DiContainer sceneContainer;
        ScenePathProvider scenePathProvider;

        public BaseState(BaseStateData stateData)
        {
            this.stateData = stateData;
            controllers = new List<IUpdatable>();
            scenePathProvider = new ScenePathProvider();
        }

        public async Task Activate(ActivateStateParameters args = null)
        {
            Debug.Log("Activating state " + this.GetType());
            this.args = args;
            Task<SceneContext> load3DSceneTask = null;
            Task<SceneContext> scene3DContext = null;
            if (stateData.SceneData3D != null)
            {
                scene3DContext = await LoadScene(stateData.SceneData3D).ContinueWith(task => load3DSceneTask = task);
                if (load3DSceneTask.Status == TaskStatus.Faulted)
                {
                    Debug.Log(load3DSceneTask.Exception);
                }
            }
            Task<SceneContext> loadUISceneTask = null;
            Task<SceneContext> sceneUIContext = null;
            if (stateData.SceneDataUI != null)
            {
                string uiScenePathBasedOnAspect;
                if (TryGetScenePath(out uiScenePathBasedOnAspect))
                {
                    var uiSceneData = new SceneData(uiScenePathBasedOnAspect, stateData.SceneDataUI.ContextGoName);
                    stateData.SceneDataUI = uiSceneData;
                    sceneUIContext = await LoadScene(uiSceneData).ContinueWith(task => loadUISceneTask = task);
                }
            }
            await Initialize(scene3DContext?.Result, sceneUIContext?.Result, args);
        }

        public async Task Deactivate(DeactivateStateParameters args = null)
        {
            await Deinitialize(args);
            if (args == null || (args != null && args.UnloadScene))
            {
                if (stateData.SceneDataUI != null)
                {
                    await UnloadScene(stateData.SceneDataUI);
                }
                if (stateData.SceneData3D != null)
                {
                    await UnloadScene(stateData.SceneData3D);
                }
                sceneContainer = null;
            }
        }

        public virtual void Update()
        {
            foreach (var item in controllers)
            {
                if (item != null)
                {
                    item.Update();
                }
            }
        }

        public virtual void OnGUI() { }

        protected virtual async Task Initialize(SceneContext scene3DContext, SceneContext sceneUIContext,
            ActivateStateParameters args = null)
        {
            await Task.Delay(1);
        }

        protected virtual async Task Deinitialize(DeactivateStateParameters args = null)
        {
            await Task.Delay(1);
        }

        private async Task<SceneContext> LoadScene(SceneData data)
        {
            bool loadScene = true;
            if (data.SceneName == string.Empty || !loadScene)
            {
                return null;
            }
            else
            {
                var operation = SceneManager.LoadSceneAsync(data.SceneName, LoadSceneMode.Additive);
                while (!operation.isDone)
                {
                    await Task.Delay(1);
                }
                var go = GameObject.Find(data.ContextGoName);
                if (go == null)
                {
                    Debug.LogError("Cannot find context go: " + data.ContextGoName);
                }
                var context = go.GetComponent<SceneContext>();
                return context;
            }
        }

        private bool TryGetScenePath(out string newPath)
        {
            return scenePathProvider.TryGetUIScenePath(this.GetType(), out newPath);
        }

        private async Task UnloadScene(SceneData data)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(data.SceneName);
            while (!operation.isDone)
            {
                await Task.Delay(1);
            }
        }


    }
}