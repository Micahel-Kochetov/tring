using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialController : MonoBehaviour {

    bool isTutorShown = false;
    [SerializeField] List<GameObject> tutorialPages;
    [SerializeField] bool emulateFirstRun;

    bool isOnTutorial = false;
    int currentPageIndex = -1;

    // Use this for initialization
    void Start () {
        isTutorShown = Convert.ToBoolean(PlayerPrefs.GetInt("isTutorial"));
        ShowTutorial();
    }

    public void ShowTutorial()
    {
        if (emulateFirstRun)
            isTutorShown = false;
        tutorialPages[0].SetActive(!isTutorShown);
        if (!isTutorShown)
        {
            currentPageIndex = 0;
            isOnTutorial = true;
        }
    }
	
    public void SetTutorShown()
    {
        PlayerPrefs.SetInt("isTutorial", 1);
        isTutorShown = true;
        isOnTutorial = false;
    }

    public void SkipTutor()
    {
        tutorialPages[currentPageIndex].SetActive(false);
        SetTutorShown();
    }

    public void ShowNext()
    {
        if (isOnTutorial && currentPageIndex >= 0)
        {
            tutorialPages[currentPageIndex].SetActive(false);
            if (++currentPageIndex < tutorialPages.Capacity)
            {
                tutorialPages[currentPageIndex].SetActive(true);
            }
            else SetTutorShown();
        }
    }

    public void ShowPrevious()
    {
        if (isOnTutorial && currentPageIndex >= 1)
        {
            tutorialPages[currentPageIndex--].SetActive(false);
            tutorialPages[currentPageIndex].SetActive(true);
        }
    }

    public bool IsTutorialShown()
    {
        return isTutorShown;
    }
}
