using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationController : MonoBehaviour {

    [SerializeField] TutorialController tutorialController;
    [SerializeField] GameObject orientationHandler;

    private void Update()
    {
        if(tutorialController.IsTutorialShown() && !orientationHandler.activeSelf)
        {
            orientationHandler.SetActive(true);
        }
    }
}
