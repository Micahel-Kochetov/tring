using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Common.AppVersion
{
    public class AppVersionService
    {
        public void IncrementVersion(string branchName)
        {
            AppVersionModel model = GetModel();
            model.BranchName = branchName;
            model.BuildNumber++;
            string data = JsonUtility.ToJson(model);
            string path = GetAppVersionModelPath();
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(data);
            }
            UnityEngine.Debug.Log("Version incremented");
        }

        public string GetAppFullVersionSync()
        {
            var model = GetModel();
            return model.GetAppFullVersion();
        }

        public string GetAppVersion()
        {
            var model = GetModel();
            return model.GetAppVersion();
        }

        public int GetBuildNumber()
        {
            var model = GetModel();
            return model.GetBuildNumber();
        }

        public IEnumerator GetAppFullVersionAsync(Action<string> callback)
        {
            yield return null;
#if UNITY_WEBGL && !UNITY_EDITOR
            yield return ReadModelVersionFromURL(callback);
#else
            callback(GetAppFullVersionSync());
#endif
        }

        private AppVersionModel GetModel()
        {
            AppVersionModel model;
            try
            {
                model = ReadModelFromDisk();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogWarning("Cannot read existing data :\n" + ex.ToString());
                model = new AppVersionModel();
            }
            return model;
        }

        private AppVersionModel ReadModelFromDisk()
        {
            string path = GetAppVersionModelPath();
            using (StreamReader reader = new StreamReader(path))
            {
                string data = reader.ReadToEnd();
                var model = JsonUtility.FromJson<AppVersionModel>(data);
                return model;
            }
        }

        private IEnumerator ReadModelVersionFromURL(Action<string> callback)
        {
            string path = Path.Combine(Application.streamingAssetsPath, "appVersion.txt");
            Debug.Log($"www path:{path}");
            WWW www = new WWW(path);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                var data = www.text;
                var model = JsonUtility.FromJson<AppVersionModel>(data);
                Debug.Log("loaded model: " + data);
                callback(model.GetAppFullVersion());
            }
            else
            {
                Debug.Log("www erorr:" + www.error);
                callback(string.Empty);
            }
        }

        private string GetAppVersionModelPath()
        {
            string path = Path.Combine(Path.GetFullPath(Application.streamingAssetsPath), "appVersion.txt");
            return path;
        }
    }
}
