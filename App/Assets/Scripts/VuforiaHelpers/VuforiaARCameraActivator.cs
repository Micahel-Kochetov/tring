using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaARCameraActivator : MonoBehaviour
{
    void OnEnable()
    {
        VuforiaARCameraController.Instance?.SetVuforiaActive(true);
    }

    void OnDisable()
    {
        VuforiaARCameraController.Instance?.SetVuforiaActive(false);
    }

    void OnDestroy()
    {
        VuforiaARCameraController.Instance?.SetVuforiaActive(false);
    }
}
