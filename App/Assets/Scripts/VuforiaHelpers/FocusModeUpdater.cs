using UnityEngine;
using Vuforia;

public class FocusModeUpdater : MonoBehaviour
{
    [SerializeField] private FocusMode focusMode;
    [SerializeField] private bool isSetOnStart;

    private void Start()
    {
        UpdateFocusMode(focusMode);
    }

    public void UpdateToContinuosAutoFocus()
    {
        UpdateFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    public void UpdateToTriggerAutoFocus()
    {
        UpdateFocusMode(FocusMode.FOCUS_MODE_TRIGGERAUTO);
    }

    public void UpdateFocusMode(FocusMode newFocusMode)
    {
        focusMode = newFocusMode;
        CameraDevice.Instance.SetFocusMode(focusMode);
    }
}