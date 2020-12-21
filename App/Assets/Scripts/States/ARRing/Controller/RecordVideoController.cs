using Assets.Scripts.States.ARRing.View;
using Assets.Scripts.States.Common.Controller;
using Assets.Scripts.States.Common.Service;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.ARRing.Controller
{
    public class RecordVideoController : AnalyticsController
    {
        [Inject]
        ARRingController arRingController;
        [Inject]
        RecordVideoView view;
        [Inject]
        PreshareController preshareController;
        [Inject]
        ScreenRecordingService screenRecordingController;
        [Inject]
        UserSessionService userSessionService;
#if UNITY_EDITOR
        float videoDuration = 2f;
#else
        float videoDuration = 15;
#endif
        int videoCount = 1;
        int currentVideoIndex = 0;
        float timerValue;
        float Timer
        {
            get { return timerValue; }
            set
            {
                timerValue = value;
                var time = Mathf.Clamp(value, 0, videoDuration);
                view.SetRecordProgress(time);
            }
        }
        bool videoIsRecording = false;

        public override void Activate()
        {
            currentVideoIndex = 0;
            base.Activate();
            view.Show();
            screenRecordingController.StartRecording();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            view.Hide();
        }

        public override void Init(string id)
        {
            base.Init(id);
            currentVideoIndex = 0;
            screenRecordingController.OnVideoRecordStarted += OnRecordingStartedHandler;
            screenRecordingController.OnVideoRecorded += OnVideoRecordedHandler;
            screenRecordingController.OnRecordingFailed += OnRecordingFailedHandler;
            view.Init();
        }

        private void OnRecordingStartedHandler()
        {
            SetVideoDuration(videoDuration).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
            });
            ResetTimer();
            videoIsRecording = true;
        }

        public override void Dispose()
        {
            screenRecordingController.OnVideoRecordStarted -= OnRecordingStartedHandler;
            screenRecordingController.OnVideoRecorded -= OnVideoRecordedHandler;
            screenRecordingController.OnRecordingFailed -= OnRecordingFailedHandler;
            base.Dispose();
            screenRecordingController.Deinitialize();
        }

        private void OnRecordingFailedHandler()
        {
            Deactivate();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (videoIsRecording)
            {
                Timer -= Time.deltaTime;
            }
        }

        async Task SetVideoDuration(float duration)
        {
            await Task.Delay((int)(1000 * duration));
            videoIsRecording = false;
            screenRecordingController.StopRecording();
        }

        private void OnVideoRecordedHandler(string path, string videoThumbnailPath)
        {
            string newPath;
#if UNITY_EDITOR
            var editorVideoPath = Application.streamingAssetsPath + "/mockvideo.mp4";
            newPath = Application.streamingAssetsPath + "/"+DateTime.Now.Ticks.ToString()+"mockvideo.mp4";
            if (File.Exists(newPath)) {
                File.Delete(newPath);
            }
            File.Copy(editorVideoPath, newPath);
#else
            newPath=path;
#endif
            userSessionService.AddVideo(newPath, videoThumbnailPath);
            currentVideoIndex++;
            if (currentVideoIndex >= videoCount)
            {
                Deactivate();
                preshareController.Activate();
            }
            else
            {
                screenRecordingController.ContinueRecording();
            }
        }

        void ResetTimer()
        {
            Timer = videoDuration;
        }
    }
}

