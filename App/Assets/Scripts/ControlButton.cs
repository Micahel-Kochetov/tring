using UnityEngine;
using System.Collections;

public class ControlButton : MonoBehaviour {
	public GameObject nextButton;
	public GameObject ringRootObj;
	public static GameObject curObj = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PressedBackButton(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("splash");
	}

	public void PressedNextButton(){
		curObj.SendMessage("ShowSelectedModel");	
	}

	public void ShowHideButton(bool isShow){
		nextButton.SetActive (isShow);
	}
}
