using UnityEngine;
using System.Collections;

public class ChangeMaterial : MonoBehaviour {
	public int idx = 0;
	public GameObject ring;

	public GameObject handBlendObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(){
//		idx = -1;
	}

	public void ShowSelectedModel(bool direction){
		if (direction == true) {	//next
			idx++;
			if (idx >= 3)
				idx = 0;	
		} else {					//prev
			idx--;
			if (idx < 0)
				idx = 2;
		}

		ring.SetActive (true);
		ring.GetComponent<RingInfo> ().Change (idx);
	}

	public void ShowCurrentModel(){
		ring.SetActive (true);
		ring.GetComponent<RingInfo> ().Change (idx);
	}

	public void HideSelectedModel(){
		ring.SetActive (false);
	}

	public void ChangeColor(bool direction){
		if (direction == true) {	//next
			idx++;
			if (idx >= 3)
				idx = 0;	
		} else {					//prev
			idx--;
			if (idx < 0)
				idx = 2;
		}

		ring.SetActive (true);
		ring.GetComponent<RingInfo> ().Change (idx);
	}

	public void ShowHideHand(bool isShow){
		handBlendObj.SetActive (isShow);
	}
}
