/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
===============================================================================*/
using UnityEngine;
using System.Collections;
using Vuforia;

public class CustomInitiErrorHandler : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES
    //public UnityEngine.UI.Text errorText;
    public GameObject errorCanvas;
    #endregion //PUBLIC_MEMBER_VARABLES


    #region PRIVATE_MEMBER_VARIABLES
    private string key;
    #endregion //PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Awake()
    {

        VuforiaRuntime.Instance.RegisterVuforiaInitErrorCallback(OnInitError);
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void OnInitError(VuforiaUnity.InitError error)
    {
        if(error == VuforiaUnity.InitError.INIT_NO_CAMERA_ACCESS)
        {
            errorCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("Error text:" + error.ToString());
            errorCanvas.SetActive(true);
        }
        
    }

   
    #endregion //PRIVATE_METHODS


    #region PUBLIC_METHODS
    public void OnErrorDialogClose()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion //PUBLIC_METHODS
}
