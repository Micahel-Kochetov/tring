using Assets.Scripts.States.Common.Service;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Common
{
    public class StreamingAssetsReaderService
    {
        private string relativeFilePath;
        [Inject]
        private CoroutineHandlerService coroutineStarterService;

        public event Action<string> onReadingFail;

        public void Initialize(string relativeFilePath)
        {
            this.relativeFilePath = relativeFilePath;
        }

        public void SaveModel<Type>(Type model)
        {
#if UNITY_EDITOR
            var data = JsonConvert.SerializeObject(model);
            string path = GetAppVersionModelPath();
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(data);
            }
            UnityEngine.Debug.Log("Data saved");
#endif
        }

        public void SaveModel<Type>(Type model, string relativePath)
        {
            var data = JsonConvert.SerializeObject(model);
            string path = Path.Combine(Path.GetFullPath(Application.streamingAssetsPath), relativePath);
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(data);
            }
        }

        public void GetAppModelAsync<Type>(Action<Type> callback) where Type : new()
        {
            coroutineStarterService.StartCoroutine(GetAppModelAsyncCoroutine<Type>(callback));
        }

        public string ReadJson(string path, bool relativeStreamingAssetsPath = true)
        {
            string absPath;
            if (relativeStreamingAssetsPath)
            {

                absPath = Path.Combine(Path.GetFullPath(Application.streamingAssetsPath), path);
            }
            else
            {
                absPath = path;
            }
            using (StreamReader reader = new StreamReader(absPath))
            {
                string data = reader.ReadToEnd();
                return data;
            }
        }

        public Model ReadJsonModel<Model>(string path, bool relativeStreamingAssetsPath = true)
        {
            string absPath;
            if (relativeStreamingAssetsPath)
            {
                absPath = Path.Combine(Path.GetFullPath(Application.streamingAssetsPath), path);
            }
            else
            {
                absPath = path;
            }
            try
            {
                using (StreamReader streamReader = new StreamReader(absPath))
                {
                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        var model = serializer.Deserialize<Model>(reader);
                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                onReadingFail?.Invoke($"Error occurred while parcing {absPath} {ex.Message}");
            }
            return default(Model);
        }

        private IEnumerator GetAppModelAsyncCoroutine<Type>(Action<Type> callback) where Type : new()
        {
            yield return null;
#if UNITY_WEBGL
            yield return ReadModelFromURL(callback);
#else
            callback(ReadModelFromDisk<Type>());
#endif
        }

        private Type ReadModelFromDisk<Type>() where Type : new()
        {
            try
            {
                string path = GetAppVersionModelPath();
                using (StreamReader reader = new StreamReader(path))
                {
                    string data = reader.ReadToEnd();
                    var model = JsonConvert.DeserializeObject<Type>(data);
                    return model;
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"Cannot read from disk:{ex.ToString()}");
                return new Type();
            }
        }

        private IEnumerator ReadModelFromURL<Type>(Action<Type> callback) where Type : new()
        {
            string path = Path.Combine(Application.streamingAssetsPath, relativeFilePath);
            UnityEngine.Debug.Log($"www path:{path}");
            WWW www = new WWW(path);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                var data = www.text;
                var model = JsonConvert.DeserializeObject<Type>(data);
                UnityEngine.Debug.Log("loaded model: " + data);
                callback(model);
            }
            else
            {
                UnityEngine.Debug.Log("www erorr:" + www.error);
                callback(new Type());
            }
        }

        private string GetAppVersionModelPath()
        {
            string path = Path.Combine(Path.GetFullPath(Application.streamingAssetsPath), relativeFilePath);
            return path;
        }
    }
}
