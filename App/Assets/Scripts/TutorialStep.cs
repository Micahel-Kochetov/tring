using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep : MonoBehaviour {

    [SerializeField] float displayTime = 5f;
    [SerializeField] bool stepIsActivated = false;
    [SerializeField] List<GameObject> tutorialBubbles = new List<GameObject>();
    [SerializeField] List<GameObject> backupBubbles = new List<GameObject>();
    [SerializeField] TutorialStep nextStep;
    [SerializeField] TutorialController_old tutorialController;
    bool stepIsCompleted = false;
    float timePassed = 0;
    [SerializeField] bool lastStep = false;

    private void Start()
    {
        if(stepIsActivated)
            ToggleBubbles(true);
        else
            ToggleBubbles(false);

    }

    public void Activate()
    {
        stepIsActivated = true;
        ToggleBubbles(true);
    }

    public void ActivateNext()
    {
        ToggleBubbles(false);
        stepIsCompleted = true;
        if (nextStep != null && !lastStep)
            nextStep.Activate();
        else if (lastStep && tutorialController != null)
            tutorialController.CloseTutorial(true);

    }

    void ToggleBubbles(bool on)
    {
        foreach (GameObject tutorialBubble in tutorialBubbles)
        {
            tutorialBubble.SetActive(on);
        }
    }
    void ToggleBackupBubbles(bool on)
    {
        foreach (GameObject backupBubble in backupBubbles)
        {
            backupBubble.SetActive(on);
        }
    }

    bool DispayIsVisible()
    {
        bool bubblesActive = false;
        foreach(GameObject tutorialBubble in tutorialBubbles)
        {
            if(tutorialBubble.activeInHierarchy)
            {
                bubblesActive = true;
                break;
            }
        }


        return bubblesActive;
    }
    
	
	// Update is called once per frame
	void Update () {


		if(stepIsActivated)
        {
            ToggleBackupBubbles(!DispayIsVisible());

            if (DispayIsVisible() && !stepIsCompleted)
            {
                timePassed += Time.deltaTime;

                if (timePassed >= displayTime)
                    ActivateNext();
            }

        }
        else
            ToggleBackupBubbles(false);

        if (stepIsCompleted)
            ToggleBackupBubbles(false);
    }
}
