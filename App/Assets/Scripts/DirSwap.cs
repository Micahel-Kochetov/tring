using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine;
using System.Collections;
using Vuforia;

public class DirSwap : MonoBehaviour
{
    //private QCARAbstractBehaviour mQCAR;

    //void Start()
    //{
    //    mQCAR = (QCARAbstractBehaviour)FindObjectOfType(typeof(QCARAbstractBehaviour));

    //}

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(50, 50, 200, 50), "Swap Camera"))
    //    {
    //        CameraDevice.CameraDirection currentDir = CameraDevice.Instance.GetCameraDirection();
    //        if (currentDir == CameraDevice.CameraDirection.CAMERA_BACK || currentDir == CameraDevice.CameraDirection.CAMERA_DEFAULT)
    //            RestartCamera(CameraDevice.CameraDirection.CAMERA_FRONT, true);
    //        else
    //            RestartCamera(CameraDevice.CameraDirection.CAMERA_BACK, false);
    //    }

    //    if (GUI.Button(new Rect(50, 100, 200, 50), "Mirror OFF"))
    //    {
    //        RestartCamera(CameraDevice.Instance.GetCameraDirection(), false);
    //    }

    //    if (GUI.Button(new Rect(50, 150, 200, 50), "Mirror ON"))
    //    {
    //        var config = QCARRenderer.Instance.GetVideoBackgroundConfig();
    //        config.reflection = QCARRenderer.VideoBackgroundReflection.ON;
    //        QCARRenderer.Instance.SetVideoBackgroundConfig(config);

    //        RestartCamera(CameraDevice.Instance.GetCameraDirection(), true);
    //    }
    //}

    //private void RestartCamera(CameraDevice.CameraDirection newDir, bool mirror)
    //{
    //    CameraDevice.Instance.Stop();
    //    CameraDevice.Instance.Deinit();

    //    CameraDevice.Instance.Init(newDir);

    //    // Set mirroring 
    //    var config = QCARRenderer.Instance.GetVideoBackgroundConfig();
    //    config.reflection = mirror ? QCARRenderer.VideoBackgroundReflection.ON : QCARRenderer.VideoBackgroundReflection.OFF;
    //    QCARRenderer.Instance.SetVideoBackgroundConfig(config);

    //    CameraDevice.Instance.Start();
    //}
}