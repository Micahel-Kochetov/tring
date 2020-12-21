using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IphoneXBand : MonoBehaviour {

    public Canvas canvas;
    public GameObject bandImg;
    string deviceModel;
    private RectTransform rect;
	void Start ()
    {
        rect = bandImg.GetComponent<RectTransform>();
        deviceModel = SystemInfo.deviceModel;
        ShowBand();
	}
	
	void Update ()
    {
		
	}

    private void ShowBand()
    {
        float bandHeight;
        bandHeight = rect.sizeDelta.y;

        Vector3 a = new Vector3(0, bandHeight, 0) * canvas.scaleFactor;

        if (deviceModel.Contains("iPhone10,3") || deviceModel.Contains("iPhone10,6"))
        {
            gameObject.transform.position += a;
            bandImg.SetActive(true);

        }

#if UNITY_EDITOR
        gameObject.transform.position += a;
        bandImg.SetActive(true);
#endif
    }
}
