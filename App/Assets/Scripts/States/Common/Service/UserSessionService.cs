using Assets.Scripts.States.Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.States.Common.Service
{
    public class UserSessionService : IInitializable, IDisposable
    {
        string format = "yyyy_MM_dd_H_mm";
        string sessionName;
        int sessionID;
        int interval = 3;
        float time;

        public void StartSession()
        {
            sessionName = DateTime.Now.ToString(format);
            sessionID = DateTime.Now.GetHashCode();
            CleanFiles(Videos);
            Videos.Clear();
        }

        public void AddVideo(string videoFilePath, string thumbnailPath)
        {
            Videos.Add(new VideoRecordModel(videoFilePath, thumbnailPath));
            VideoConvertService.Instance.RunVideoConversion(videoFilePath);
        }

        public string SessionName
        {
            get { return sessionName; }
        }

        public List<VideoRecordModel> Videos
        {
            get; private set;
        }


        public int GetSessionId()
        {
            return sessionID;
        }

        public void Initialize()
        {
            VideoConvertService.OnVideoConvertSuccess += VideoConvertService_OnVideoConvertSuccess;
            VideoConvertService.OnVideoConvertFail += VideoConvertService_OnVideoConvertFail;
            Videos = new List<VideoRecordModel>();
        }

        private void VideoConvertService_OnVideoConvertFail(string obj)
        {

        }

        private void VideoConvertService_OnVideoConvertSuccess(string input, string output)
        {
            var videoData = Videos.Find(item => item.FilePath == input);
            if (videoData != null)
            {
                UnityEngine.Debug.Log("video data found");
                videoData.ConvertedVideoPath = output;
            }
            else
            {
                UnityEngine.Debug.Log("video data not found");
            }
        }

        public void Dispose()
        {
            VideoConvertService.OnVideoConvertSuccess -= VideoConvertService_OnVideoConvertSuccess;
            VideoConvertService.OnVideoConvertFail -= VideoConvertService_OnVideoConvertFail;
        }

        void CleanFiles(List<VideoRecordModel> videoModels)
        {
            try
            {
                for (int i = 0; i < videoModels.Count; i++)
                {
                    var model = videoModels[i];
                    if (File.Exists(model.ThumbnailPath))
                    {
                        File.Delete(model.ThumbnailPath);
                    }
                    DeleteFileAndDirectory(model.ConvertedVideoPath);
                    DeleteFileAndDirectory(model.FilePath);
                    videoModels[i] = null;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }

        }

        void DeleteFileAndDirectory(string path)
        {
            if (File.Exists(path))
            {
                Debug.Log($"deleting file at:{path}");
                File.Delete(path);
                var dir = Path.GetDirectoryName(path);
                if (IsDirectoryEmpty(dir))
                {
                    Directory.Delete(dir);
                }
            }
        }

        public bool IsDirectoryEmpty(string path)
        {
            IEnumerable<string> items = Directory.EnumerateFileSystemEntries(path);
            using (IEnumerator<string> en = items.GetEnumerator())
            {
                return !en.MoveNext();
            }
        }
    }
}
