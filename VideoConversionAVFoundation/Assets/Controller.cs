using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Controller : MonoBehaviour
{
    private VideoPlayer player;
    public string videoPath;
    public RawImage videoImage;
    string VideoPath
    {
        get
        {
            return Path.Combine(Application.streamingAssetsPath, videoPath); ;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        VideoConvertService.Instance.OnVideoConvertSuccess += VideoConvertWrapper_OnVideoConverted;
        VideoConvertService.Instance.OnVideoConvertFail += VideoConvertWrapper_OnVideoConvertFail;
    }

    private void VideoConvertWrapper_OnVideoConvertFail(string obj)
    {
        Debug.Log("Video Convert Fail");
    }

    private void VideoPlayer_frameReady(VideoPlayer source, long frameIdx)
    {
        videoImage.texture = source.texture;
    }

    private void VideoConvertWrapper_OnVideoConverted(string obj)
    {
        Debug.Log("Video Convert Success");
        player.Stop();
        player.url = obj;
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Convert()
    {
        Debug.Log("On Convert");
        VideoConvertService.Instance.RunVideoConversion(VideoPath);
        Resources.UnloadUnusedAssets();
    }

    public void Play()
    {
        Debug.Log("On Play");
        player.url = VideoPath;
        player.Play();
    }

    public void Stop()
    {
        Debug.Log("On Stop");
        player.Stop();
    }

    public void LoadPlayer() {
        player = videoImage.gameObject.AddComponent<VideoPlayer>();
        player.source = VideoSource.Url;
        player.sendFrameReadyEvents = true;
        player.frameReady += VideoPlayer_frameReady;
    }

    public void UnloadPlayer() {
        player.Stop();
        player.frameReady -= VideoPlayer_frameReady;
        Destroy(player);
        Debug.Log("1212");
    }
}
