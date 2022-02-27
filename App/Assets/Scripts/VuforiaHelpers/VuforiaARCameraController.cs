using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaARCameraController : MonoBehaviour
{
    public static VuforiaARCameraController Instance { private set; get; }

    [SerializeField] private GameObject ARCameraContainer;
    [SerializeField] private Camera camera;

    public Camera Camera
    {
        get
        {
            return camera;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Instance = this;
    }

    public void SetVuforiaActive(bool isActive)
    {
        ARCameraContainer.SetActive(isActive);

        if(isActive)
        {
            //TODO: find correct migrated method in Vuforia 10.
            //TrackerManager.Instance.GetStateManager().ReassociateTrackables();
        }
    }
}
