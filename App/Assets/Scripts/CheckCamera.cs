using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCamera : MonoBehaviour {


    enum CamCheckStatus
    {
        NotRequested,
        Failed,
        Passed
    }

    [SerializeField]  CamCheckStatus camCheckStatus = CamCheckStatus.NotRequested;
    WebCamTexture webcamTexture = null;
    [SerializeField] GameObject settingsText;
    // Use this for initialization
    void Start () {
        settingsText.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		if(camCheckStatus == CamCheckStatus.Failed)
            settingsText.SetActive(true);
        else
            settingsText.SetActive(false);
    }

    public void OnOkButtonClick()
    {
        CheckCam();
        if (!webcamTexture.isPlaying)
            camCheckStatus = CamCheckStatus.Failed;
        else
        {
            camCheckStatus = CamCheckStatus.Passed;
            webcamTexture.Stop();
            webcamTexture = null;
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    void CheckCam()
    {
        webcamTexture = new WebCamTexture();
        {
            Debug.Log(" anisoLevel: " + webcamTexture.anisoLevel);
            Debug.Log("deviceName" + webcamTexture.deviceName);
            Debug.Log("didUpdateThisFrame " + webcamTexture.didUpdateThisFrame);
            Debug.Log("dimension " + webcamTexture.dimension);
            Debug.Log("filterMode " + webcamTexture.filterMode);
            Debug.Log("height " + webcamTexture.height);
            Debug.Log("hideFlags " + webcamTexture.hideFlags);
            Debug.Log("isPlaying " + webcamTexture.isPlaying);
            Debug.Log("mipMapBias " + webcamTexture.mipMapBias);
            Debug.Log("name " + webcamTexture.name);
            Debug.Log("texelSize " + webcamTexture.texelSize);
            
        }

        try
        {

            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
        catch(System.Exception e)
        {
            webcamTexture = null;
        }

    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = Application.LoadLevelAsync("ARMobile");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
