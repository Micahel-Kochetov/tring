using System.Collections;
using UnityEngine;

public class OrientationHandler : MonoBehaviour
{

    private void Awake()
    {

        //if (SystemInfo.deviceModel.Contains("iPad"))
        //{
        //    StartCoroutine(setOrientationLandscape());
        //}
        //else
        //    StartCoroutine(setOrientationPortrait());
        StartCoroutine(setOrientationPortrait());
    }

    public static IEnumerator setOrientationLandscape()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = true;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

    }
    public static IEnumerator setOrientationPortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

    }
}


