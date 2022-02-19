using UnityEngine;
using Vuforia;

public class FocusModeUpdater : MonoBehaviour
{
    [SerializeField] private FocusMode focusMode;

    private void Start()
    {
        UpdateFocusMode(focusMode);
    }

    void UpdateFocusMode(FocusMode newFocusMode)
    {
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(newFocusMode);
        focusMode = newFocusMode;
    }

    public void UpdateFocusModeToTriggerAuto()
    {
        UpdateFocusMode(FocusMode.FOCUS_MODE_TRIGGERAUTO);
    }
    
    public void UpdateFocusModeToContinuousAuto()
    {
        UpdateFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
}