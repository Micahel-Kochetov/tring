using Assets.Scripts.States.ARRing.Interface;
using System;
using UnityEngine;
using VoxelBusters.ReplayKit;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{
    public class ScreenRecordingPermissionService : ITickable, IInitializable, ILateDisposable
    {
        [Inject]
        IScreenRecordingDispose screenRecorder;
        float requestPermissionTime;
        readonly float requestInterval = 10 * 60;//every 10 minutes
        ReplayKitInitialisationState initState;
        readonly int initAttempts = 3;
        int initCounter;
        float initTime;
        readonly float initInterval = 1f;
        bool subscribedToRelayKitStateChangeEvent;
        float subscribeTime;
        readonly float unsubscribeDelay = 0.5f;

        public void Tick()
        {
            if (initState == ReplayKitInitialisationState.Success)
            {
                if ((Time.time - requestPermissionTime) > requestInterval)
                {
                    RequestPermission();
                }
            }
            else if (initState == ReplayKitInitialisationState.Failed)
            {
                if ((Time.time - initTime) > initInterval)
                {
                    initCounter = 0;
                    initTime = Time.time;
                    ReplayKitManager.Initialise();
                }
            }
            //if user does not allow to reacord the video via system pop up, there is no Relaykit callback on that case.
            //to unsubscribe from event we need to handle this case in the way below
            if (subscribedToRelayKitStateChangeEvent)
            {
                if ((Time.time - subscribeTime) > unsubscribeDelay)
                {
                    ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
                    subscribedToRelayKitStateChangeEvent = false;
                    Debug.Log("Unsubscribed from ReplayKitManager.DidRecordingStateChange event");
                }
            }
        }

        public void Initialize()
        {
            initState = ReplayKitInitialisationState.Failed;
            initCounter = 0;
            requestPermissionTime = Time.time;
            ReplayKitManager.DidInitialise += OnInitializeHandler;
            initTime = 0;
            subscribedToRelayKitStateChangeEvent = false;
        }

        public void LateDispose()
        {
            ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
            ReplayKitManager.DidInitialise -= OnInitializeHandler;
        }

        void OnInitializeHandler(ReplayKitInitialisationState state, string message)
        {
            initState = state;
            switch (state)
            {
                case ReplayKitInitialisationState.Success:
                    ReplayKitManager.DidInitialise -= OnInitializeHandler;
                    RequestPermission();
                    break;
                case ReplayKitInitialisationState.Failed:
                    initCounter++;
                    if (initCounter < initAttempts)
                    {
                        ReplayKitManager.Initialise();
                    }
                    else
                    {
                        Debug.LogError("Failed to initialize ReplayKitManager");
                    }
                    break;
                default:
                    break;
            }
        }

        void OnStateChangeEvent(ReplayKitRecordingState state, string message)
        {
            Debug.Log($"Permission service state:{state} message:{message}");
            switch (state)
            {
                case ReplayKitRecordingState.Started:
                    ReplayKitManager.StopRecording();
                    break;
                case ReplayKitRecordingState.Stopped:
                    ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
                    subscribedToRelayKitStateChangeEvent = false;
                    break;
                case ReplayKitRecordingState.Failed:
                    break;
                case ReplayKitRecordingState.Available:
                    break;
                default:
                    break;
            }
        }

        void RequestPermission()
        {
            if (screenRecorder.IsRecording)
            {
                return;
            }
            Debug.Log("Requesting permission");
            ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
            ReplayKitManager.DidRecordingStateChange += OnStateChangeEvent;
            subscribedToRelayKitStateChangeEvent = true;
            subscribeTime = Time.time;
            try
            {
                ReplayKitManager.StartRecording(enableMicrophone: false);
            }
            catch (Exception ex)
            {
                //Crashlytics.LogException(ex);
            }
            requestPermissionTime = Time.time;
        }
    }
}
