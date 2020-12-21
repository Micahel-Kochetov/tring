using Assets.Scripts.States.ARRing.Controller;
using Assets.Scripts.States.Common.Service;
using Assets.Scripts.States.GetYourVideos.View;
using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.GetYourVideos.Controller
{
    public class ShareVideosController
    {
        [Inject]
        private ShareVideosView view;
        [Inject]
        private SendVideosConfirmationController sendVideosConfirmationController;
        [Inject]
        private StopTheFunController stopTheFunController;
        [Inject]
        private EmailShareFinalizeService emailShareFinalizeService;
        [Inject]
        private UserSessionService userSessionService;

        private string[] videos;
        private string[] videoUrls;
        private string email;
        private string phoneNumber;
        private bool isEmailSendRequired;
        private bool isAirDropRequired;

        public void Init(string[] videos)
        {
            view.Init();
            view.OnClose += OnCloseHandler;
            view.OnSubmit += OnSubmitHandler;
            this.videos = CopyVideos(videos);
        }

        private string[] CopyVideos(string[] videos)
        {
            string[] newVideos = new string[videos.Length];
            var tmpPath = GetHashCode().ToString();
            for (int i = 0; i < videos.Length; i++)
            {
                var newFilePath = Path.Combine(Application.persistentDataPath, tmpPath, Path.GetFileName(videos[i]));
                var directoryPath = Path.GetDirectoryName(newFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.Copy(videos[i],newFilePath, true);
                newVideos[i] = newFilePath;
            }
            return newVideos;
        }

        private void RemoveVideos()
        {
            Debug.Log("Remove videos");
            if (videos == null)
            {
                Debug.Log("videos is null");
            }
            foreach (var path in videos)
            {
                if (path == null)
                {
                    Debug.Log("Path is null");
                }
                else if (!File.Exists(path))
                {
                    Debug.Log("file does not exist:" + path);
                }
                else
                {
                    File.Delete(path);
                    Debug.Log("file delete complete:" + path);
                }
            }
            if (videos.Length > 0)
            {
                var dir = Path.GetDirectoryName(videos[0]);
                try
                {
                    Directory.Delete(dir);
                }
                catch (Exception ex) {
                    Debug.Log(ex);
                }
            }
            videos = null;
        }

        private void InvokedRemoveVideos()
        {
            sendVideosConfirmationController.OnDeactivated -= InvokedRemoveVideos;
            RemoveVideos();
        }

        public void Activate()
        {
            view.Show();

            isEmailSendRequired = false;
            isAirDropRequired = false;
        }

        public void Deactivate()
        {
            emailShareFinalizeService.SetUserData(userSessionService.GetSessionId(), isEmailSendRequired, email);

            view.Hide();
            if (!isAirDropRequired)
            {
                RemoveVideos();
            }
            videoUrls = null;
        }

        public void SetUserData(string email, string phoneNumber)
        {
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        private void OnCloseHandler()
        {
            stopTheFunController.Activate();
        }

        private void OnSubmitHandler(ShareVideosView.ShareType shareType)
        {
            switch (shareType)
            {
                case ShareVideosView.ShareType.Email:
                    Debug.Log("Mark email send required");
                    isEmailSendRequired = true;
                    break;
                case ShareVideosView.ShareType.AirDrop:
                    Debug.Log("Prepearing airdrop...");
                    NativeShare nativeShare = new NativeShare();
                    foreach (var video in videos)
                    {
                        Debug.Log("add file: " + video);
                        nativeShare.AddFile(video);
                    }
                    Debug.Log("Share videos");
                    nativeShare.Share();
                    sendVideosConfirmationController.OnDeactivated += InvokedRemoveVideos;
                    isAirDropRequired = true;
                    break;
                case ShareVideosView.ShareType.WhatsApp:
                    break;
                case ShareVideosView.ShareType.WhatsApp_Purchase:
                    break;
                default:
                    break;
            }

            Deactivate();
            sendVideosConfirmationController.Activate();
        }
    }
}
