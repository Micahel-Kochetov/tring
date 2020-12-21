using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {
	public GameObject[] selectedObj;
	private int currentState = 0;

	private int matIdx = 0;
	private int gemIdx = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Next(){
		selectedObj [currentState].SetActive (false);
		currentState++;
		if (currentState > 3)
			currentState = 0;
		selectedObj [currentState].SetActive (true);

		Init (currentState);
	}

	public void Prev(){
		selectedObj [currentState].SetActive (false);
		currentState--;
		if (currentState < 0)
			currentState = 3;
		selectedObj [currentState].SetActive (true);

		Init (currentState);
	}

	public void NextColor(){
		matIdx++;
		if (matIdx >= 3)
			matIdx = 0;

		selectedObj [currentState].GetComponent<RingInfo> ().Change (matIdx);
	}

	public void PrevColor(){
		matIdx--;
		if (matIdx < 0)
			matIdx = 2;

		selectedObj [currentState].GetComponent<RingInfo> ().Change (matIdx);
	}

	public void Init(int idx){
		selectedObj [idx].GetComponent<RingInfo> ().Change (0);
	}

	public void Return(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("splash");
	}
}
