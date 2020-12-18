using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class VideoConvertService : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern float ConvertVideo(string sourcePath, string outputPath);
    public event Action<string> OnVideoConvertSuccess;
    public event Action<string> OnVideoConvertFail;
    private static VideoConvertService instance;

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
        OnVideoConvertSuccess?.Invoke(destinationPath);
#endif
    }
    public void OnVideoConvertSuccessHandler(string outputPath)
    {
        OnVideoConvertSuccess?.Invoke(outputPath);
    }

    public void OnVideoConvertFailHandler(string desiredPath)
    {
        OnVideoConvertFail?.Invoke(desiredPath);
    }

    public static VideoConvertService Instance {
        get {
            return instance;
        }
    }
}
