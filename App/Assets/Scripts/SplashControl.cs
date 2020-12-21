using UnityEngine;
using System.Collections;

public class SplashControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadARScene(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("AR");	
	}

	public 	void LoadVRScene(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("VR");	
	}
}
