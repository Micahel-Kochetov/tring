using Assets.Scripts.CoreStates;
using Assets.Scripts.States.ARRing;
using Assets.Scripts.States.GetYourVideos;
using Assets.Scripts.States.StartTheMagic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI.Factories
{
    public class AppStatesFactory
    {
        [Inject]
        DiContainer container;

        public BaseState Create(EStateType type)
        {
            BaseState state = null;
            switch (type)
            {
                case EStateType.StartTheMagic:
                    state = new StartTheMagicState();
                    break;
                case EStateType.ARRing:
                    state = new ARRingState();
                    break;
                case EStateType.GetVideos:
                    state = new GetYourVideosState();
                    break;
                default:
                    break;
            }
            if (state == null)
            {
                Debug.LogError($"Cannot create state for state type {type}");
            }
            else
            {
                container.Inject(state);
            }
            return state;
        }
    }
}
