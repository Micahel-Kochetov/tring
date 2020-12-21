using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchJewelryMaterialDebug : MonoBehaviour {

    public RingInfo ringInfo;

    public bool switchMat = false;
    public int idx = 0;


    // Update is called once per frame
    void Update () {
	if(switchMat)
        {
            switchMat = false;
            ringInfo.Change(idx);
        }
	}
}
