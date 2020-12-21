using Assets.Scripts.States.ARRing.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using VoxelBusters.ReplayKit;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{

    public class ScreenRecordingService : IScreenRecordingDispose, ITickable, IInitializable
    {
        public event Action OnVideoRecordStarted;
        public event Action OnRecordingFailed;
        public event Action<string, string> OnVideoRecorded;
        public bool ReceivePermissionOnly;
        string videoThumbnailPath;
        bool waitingForRecordingToStart;
        float recordingStartTime;
        readonly float recordingStartCheckDelay = 0.5f;

        public void Initialize()
        {
        }

        public void StartRecording()
        {
            IsRecording = true;
            ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
            ReplayKitManager.DidRecordingStateChange += OnStateChangeEvent;
            Debug.Log("Try to start recording...");
            waitingForRecordingToStart = true;
            recordingStartTime = Time.time;
            try
            {
                ReplayKitManager.StartRecording(enableMicrophone: false);
            }
            catch (Exception ex)
            {
                //Crashlytics.LogException(ex);
            }
        }

        public void ContinueRecording()
        {
            ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
            ReplayKitManager.DidRecordingStateChange += OnStateChangeEvent;
            try
            {
                ReplayKitManager.StartRecording(enableMicrophone: false);
            }
            catch (Exception ex)
            {
                //Crashlytics.LogException(ex);
            }
        }

        public void StopRecording()
        {
            ReplayKitManager.StopRecording();
        }

        public void Preview()
        {
            ReplayKitManager.Preview();
        }

        public void Discard()
        {
            ReplayKitManager.Discard();
        }

        private void OnStateChangeEvent(ReplayKitRecordingState state, string message)
        {
            Debug.Log("ScrnRcrdService:: " + state + " " + message);
            switch (state)
            {
                case ReplayKitRecordingState.Started:
                    waitingForRecordingToStart = false;
                    OnVideoRecordStarted?.Invoke();
                    videoThumbnailPath = string.Empty;
                    CaptureScreenshot();
                    return;
                case ReplayKitRecordingState.Failed:
                    IsRecording = false;
                    OnRecordingFailed?.Invoke();
                    return;
                case ReplayKitRecordingState.Available:
                    ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
                    var filePath = ReplayKitManager.GetPreviewFilePath();
                    if (!string.IsNullOrEmpty(videoThumbnailPath))
                    {
#if !UNITY_EDITOR
                        var thumbnailPath = Path.Combine(Application.persistentDataPath, videoThumbnailPath);
                        var newFilePath = Path.Combine(Application.persistentDataPath, Path.GetFileName(filePath));
                        File.Copy(filePath, newFilePath);
                        OnVideoRecorded?.Invoke(newFilePath, thumbnailPath);
#else
                        Debug.Log(filePath);
                        OnVideoRecorded?.Invoke(filePath, videoThumbnailPath);
#endif
                    }
                    else
                    {
                        Debug.Log("currentPreviewPath is null");
                    }
                    IsRecording = false;
                    return;
                default:
                    return;
            }
        }

        public async Task CaptureScreenshot()
        {
            await Task.Delay(1000);
            videoThumbnailPath = $"preview{Guid.NewGuid()}.png";
            ScreenCapture.CaptureScreenshot(videoThumbnailPath);
        }

        public void DisposeVideos()
        {
        }

        public void Deinitialize()
        {
            Discard();
            ReplayKitManager.DidRecordingStateChange -= OnStateChangeEvent;
            OnVideoRecorded = null;
        }

        public void Tick()
        {
            if (waitingForRecordingToStart)
            {
                if (Time.time - recordingStartTime > recordingStartCheckDelay)
                {
                    waitingForRecordingToStart = false;
                    IsRecording = false;
                    OnRecordingFailed?.Invoke();
                }
            }
        }

        public bool IsRecording
        {
            get; private set;
        }
    }
}