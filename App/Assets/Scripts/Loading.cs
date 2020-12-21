using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    public string nextMobileSceneName = "ARMobile";
    public string nextTabletSceneName = "ARMobile";
    public GameObject splashPanel;
    public GameObject loadingText;
    public bool auto = true;
    string nextSceneName;
    float timer = 0f;
    int maxDots = 3;
    int dot = 0;
    string dotString = "";
    [SerializeField] Text dotText;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

        if (SystemInfo.deviceModel.Contains("iPad"))
        {
            nextSceneName = nextTabletSceneName;
        }
        else
        {
            nextSceneName = nextMobileSceneName;
        }
        StartCoroutine(LoadingDotrs());
        StartCoroutine(DelayedLoad());
    }

    public void OnLoadButtonClick()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    
    IEnumerator LoadingDotrs()
    {
        while (true)
        {
            dotString = "";
            for (int i = 0; i < dot; i++)
            {
                dotString += ".";
            }
            dotText.text = dotString;
            dot = (int)Mathf.Repeat(dot + 1, maxDots + 1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(nextSceneName);
    }
}
