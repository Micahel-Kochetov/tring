using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController_old : MonoBehaviour {

    [SerializeField] TutorialStep firstStep;
    [SerializeField] GameObject pdfParent;

    [SerializeField] bool tutorialDone = false;
    [SerializeField] bool pdfDone = false;
    [SerializeField] bool cleanOnStart = false;
    // Use this for initialization
    void Start () {

        if(cleanOnStart)
        {
            PlayerPrefs.SetInt("pdfDone", 0);
            PlayerPrefs.SetInt("tutorialDone", 0);
        }

        tutorialDone = System.Convert.ToBoolean(PlayerPrefs.GetInt("tutorialDone"));
        pdfDone = System.Convert.ToBoolean(PlayerPrefs.GetInt("pdfDone"));

        if (!pdfDone)
            ActivatePDFDialog();
        else if (pdfDone && !tutorialDone)
            ActivateTutorial();

    }
	
    void ActivatePDFDialog()
    {

        pdfParent.SetActive(true);
    }
    void ActivateTutorial()
    {
        firstStep.Activate();

    }

    public void ClosePDFDialog(bool finish)
    {
        pdfParent.SetActive(false);

        int pdfCompleted = System.Convert.ToInt32(finish);
        PlayerPrefs.SetInt("pdfDone", pdfCompleted);
        pdfDone = finish;

        tutorialDone = System.Convert.ToBoolean(PlayerPrefs.GetInt("tutorialDone"));
        if(!tutorialDone)
            ActivateTutorial();

    }

    public void CloseTutorial(bool finish)
    {

        firstStep.Activate();

        int tutorialCompleted = System.Convert.ToInt32(finish);
        PlayerPrefs.SetInt("tutorialDone", tutorialCompleted);
        tutorialDone = finish;
    }
    

    // Update is called once per frame
    void Update () {
		
	}
}
