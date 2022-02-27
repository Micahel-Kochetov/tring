using UnityEngine;
using Vuforia;

public class CustomInitErrorHandler : MonoBehaviour
{
    public GameObject errorCanvas;

    private void Awake()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += OnInitError;
    }

    private void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized -= OnInitError;
    }

    private void OnInitError(VuforiaInitError error)
    {
        if (error != VuforiaInitError.NONE)
        {
            errorCanvas.SetActive(true);
            Debug.Log("VuforiaInitError:" + error);
        }
    }
}