using System;  
using UnityEngine;  
using System.Collections;  
using System.IO;  
using Image = UnityEngine.UI.Image;  

public class InterceptTexture : MonoBehaviour {  

	public Image scanTexture;  
	public bool isrealRender = false;  
	private Camera scanCamera;  
	private RenderTexture renderTexture;  
	private string pipingID;  
	private string scanPath;  
	private Rect scanRect;  
	private bool isScanTexture = false;//是否开启实时渲染  
	//ImageTargetBaseBehaviour targetBehaviour;  


	// Use this for initialization  
	void Start()  
	{  
		scanPath = Application.dataPath + "/StreamingAssets/Drwaing/";  
		scanRect = new Rect(scanTexture.rectTransform.position.x - scanTexture.rectTransform.rect.width / 2, scanTexture.rectTransform.position.y - scanTexture.rectTransform.rect.height / 2,  
			(int)scanTexture.rectTransform.rect.width, (int)scanTexture.rectTransform.rect.height);  
		//targetBehaviour = GetComponentInParent<ImageTargetBaseBehaviour>();  
		gameObject.layer = 31;  

	}  

	void Renderprepare()  
	{  
		if (!scanCamera)  
		{  
			GameObject obj = new GameObject("ScanCamera");  
			scanCamera = obj.AddComponent<Camera>();  
			obj.transform.parent = transform.parent;  
			scanCamera.hideFlags = HideFlags.HideAndDontSave;  
		}  
		scanCamera.CopyFrom(Camera.main);  
		scanCamera.depth = 0;  
		scanCamera.cullingMask = 31;  
		if (!renderTexture)  
		{  
			renderTexture = new RenderTexture(Screen.width, Screen.height, -50);  
		}  
		if (!isScanTexture)  
		{  
			scanCamera.targetTexture = renderTexture;  
			scanCamera.Render();  
		}  
		if(isrealRender)  
			GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);  
		//RenderTexture.active = renderTexture;  
		//StartCoroutine(ImageCreate());  

	}  

	void OnWillRenderObject()  
	{  
		Renderprepare();  
	}  

	void OnDestroy()  
	{  
		if (renderTexture)  
			DestroyImmediate(renderTexture);  
		if (scanCamera)  
			DestroyImmediate(scanCamera.gameObject);  
	}  

	public void ScanTextureClick()  
	{  
		StartCoroutine(ImageCreate());  
	}  

	IEnumerator ImageCreate()  
	{  
		isScanTexture = true;  
		if (isScanTexture)  
		{  
			scanCamera.targetTexture = renderTexture;  
			scanCamera.Render();  
		}  
		RenderTexture.active = renderTexture;  
		Texture2D scantTexture2D = new Texture2D((int)scanRect.width, (int)scanRect.height, TextureFormat.RGB24, false);  
		yield return new WaitForEndOfFrame();  
		scantTexture2D.ReadPixels(scanRect, 0, 0, false);  
		scantTexture2D.Apply();  

		scanCamera.targetTexture = null;  
		RenderTexture.active = null;  
		GameObject.Destroy(renderTexture);  

		byte[] bytes = scantTexture2D.EncodeToPNG();  
		string savePath = scanPath + gameObject.name + ".png";  
		File.WriteAllBytes(savePath,bytes);  
		isScanTexture = false;  
		isrealRender = false;        ///关闭实时渲染  
		this.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", scantTexture2D);  
		Debug.Log("截图完成！");  
	}  
}  