using Assets.Scripts.FirebaseSDK.Storage;
using Assets.Scripts.States.ARRing.Controller;
using Assets.Scripts.States.Common.Controller;
using Assets.Scripts.States.Common.Service;
using Assets.Scripts.States.GetYourVideos.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.GetYourVideos.Controller
{
    public class GetYourVideosController : AnalyticsController
    {
        [Inject]
        GetYourVideosView view;
        [Inject]
        ShareVideosController shareVideosController;
        [Inject]
        StopTheFunController stopTheFunController;
        [Inject]
        IFileUploadService fileUploadService;
        [Inject]
        EmailShareFinalizeService emailShareFinalizeService;
        [Inject]
        WhatsAppShareFinalizeService whatsAppShareFinalizeService;
        [Inject]
        UserSessionService userSessionService;
        [Inject]
        ScreenRecordingService screenRecordingService;
        string[] videos;
        string timeFormat = "HH_mm_ss";
        readonly string videoContainer = ".mp4";

        public void Init(string[] videos, string screenID)
        {
            base.Init(screenID);
            view.Init();
            view.OnSendVideos += OnSendVideosHandler;
            view.OnClose += OnCloseHandler;
            this.videos = videos;
            fileUploadService.Init();
        }

        public override void Dispose()
        {
            view.Dispose();
            view.OnSendVideos -= OnSendVideosHandler;
            view.OnClose -= OnCloseHandler;
        }

        public override void Activate()
        {
            base.Activate();
            view.Show();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            view.Hide();
        }

        private void OnCloseHandler()
        {
            stopTheFunController.Activate();
        }

        private void OnSendVideosHandler(string email, string phoneNumber)
        {
            Deactivate();
            analyticsService.RegisterEmail(email);
            analyticsService.RegisterPhone(phoneNumber);
            analyticsService.RegisterPersonalInfo();
            shareVideosController.Activate();
            shareVideosController.SetUserData(email, phoneNumber);
            UploadData(userSessionService.GetSessionId(), email).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Upload data task faluted: " + task.Exception);
                }
            });
        }

        string[] CopyVideos(string[] source)
        {
            var copy = new string[source.Length];
            var tmpPath = GetHashCode().ToString();
            for (int i = 0; i < source.Length; i++)
            {
                var newFilePath = Path.Combine(Application.persistentDataPath, tmpPath, Path.GetFileName(source[i]));
                var directoryPath = Path.GetDirectoryName(newFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.Copy(source[i], newFilePath, true);
                copy[i] = newFilePath;
            }
            return copy;
        }

        void DeleteVideos(string[] source)
        {
            foreach (var item in source)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        File.Delete(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.ToString());
                }
            }
            try
            {
                var dir = Path.GetDirectoryName(source[0]);
                Directory.Delete(dir);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }

        private async Task UploadData(int sessionId, string email)
        {
            var data = new Dictionary<string, string>();
            var timeStamp = DateTime.Now.ToString(timeFormat);
            var index = 0;
            var cloudBasePath = email + "/" + userSessionService.SessionName + "/" + timeStamp;
            var videosCopy = CopyVideos(videos);
            var videosPathReference = videos;
            var analyticsEventID = analyticsService.CurrentEventID;
            foreach (var item in videosCopy)
            {
                var itemPath = cloudBasePath + "_" + index.ToString() + videoContainer;
                index++;
                if (!data.ContainsKey(item))
                {
                    data.Add(item, itemPath);
                }
                else
                {
                    Debug.Log("data already contains key " + item);
                }
            }
            Task<string[]> videoUploadTask = null;
            Debug.Log("Starting video upload...");
            await fileUploadService.UploadFiles(data).ContinueWith(task => videoUploadTask = task);
            if (videoUploadTask.IsFaulted)
            {
                Debug.LogError("Error uploading videos:\n" + videoUploadTask.Exception);
            }
            else
            {
                Debug.Log("video upload task status " + videoUploadTask.Status);
            }
            DeleteVideos(videosCopy);
            if (videoUploadTask.Status == TaskStatus.RanToCompletion)
            {
                analyticsService.RegisterVideoRecords(analyticsEventID, videoUploadTask.Result);
                //emailShareFinalizeService.SetVideoUrls(sessionId, videoUploadTask.Result);
                emailShareFinalizeService.SetVideoUrls(sessionId, videoUploadTask.Result);
            }
        }
    }
}
