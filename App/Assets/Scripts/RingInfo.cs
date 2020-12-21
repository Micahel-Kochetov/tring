using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingInfo : MonoBehaviour {
	public Transform[] ringObj;
	public Transform[] gem1Objs;
	public Transform[] gem2Objs;
	public Transform[] gem3Objs;

	public Material[] ringMats;
	public Material[] gem1Mats;
	public Material[] gem2Mats;
	public Material[] gem3Mats;

    public string shopifyURL = string.Empty;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Change(int idx){

        Debug.Log("Change color: "+idx);

		//foreach (Transform g in ringObj) {
		//	g.GetComponent<Renderer> ().material = ringMats [idx];
		//}

		//foreach (Transform g in gem1Objs) {
		//	g.GetComponent<Renderer> ().material = gem1Mats[idx];
		//}

		//foreach (Transform g in gem2Objs) {
		//	g.GetComponent<Renderer> ().material = gem2Mats[idx];
		//}

		//foreach (Transform g in gem3Objs) {
		//	g.GetComponent<Renderer> ().material = gem3Mats[idx];
		//}
	}
}
