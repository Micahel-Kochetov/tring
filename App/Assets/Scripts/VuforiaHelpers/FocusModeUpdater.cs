using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FocusModeUpdater : MonoBehaviour
{
    [SerializeField] private CameraDevice.FocusMode focusMode;
    [SerializeField] private bool isSetOnStart;

    void Start()
    {
        UpdateFocusMode(focusMode);
    }

    public void UpdateToContinuosAutoFocus()
    {
        UpdateFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    public void UpdateToTriggerAutoFocus()
    {
        UpdateFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
    }

    public void UpdateFocusMode(CameraDevice.FocusMode newFocusMode)
    {
        focusMode = newFocusMode;
        CameraDevice.Instance.SetFocusMode(focusMode);
    }
}
