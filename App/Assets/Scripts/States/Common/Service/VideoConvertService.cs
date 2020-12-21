using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.States.Common.Service
{

    public class VideoConvertService : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern float ConvertVideo(string sourcePath, string outputPath);
        public static event Action<string,string> OnVideoConvertSuccess;
        public static event Action<string> OnVideoConvertFail;
        private static VideoConvertService instance;
        string sourcePath;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RunVideoConversion(string sourcePath)
        {
            this.sourcePath = sourcePath;
            var tmpDirectory = Path.Combine(Application.persistentDataPath, Math.Abs(GetHashCode()).ToString());
            if (!Directory.Exists(tmpDirectory))
            {
                Directory.CreateDirectory(tmpDirectory);
            }
            var prefix = DateTime.Now.Ticks.ToString();
            var destinationPath = Path.Combine(tmpDirectory, prefix + Path.GetFileName(sourcePath));
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
#if UNITY_IOS && !UNITY_EDITOR
    ConvertVideo(sourcePath, destinationPath);
#else
            File.Copy(sourcePath, destinationPath);
            OnVideoConvertSuccess?.Invoke(sourcePath, destinationPath);
#endif
        }
        public void OnVideoConvertSuccessHandler(string outputPath)
        {
            OnVideoConvertSuccess?.Invoke(sourcePath, outputPath);
        }

        public void OnVideoConvertFailHandler(string sourcePath)
        {
            OnVideoConvertFail?.Invoke(sourcePath);
        }

        public static VideoConvertService Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
