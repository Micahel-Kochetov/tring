using UnityEngine;
using System.Collections;

[System.Serializable]
public class Preset
{
	public string m_name;
	public int m_idTexture =0;
	public int m_idMask = 0;
	public float m_Emission = 1f;	
	public float m_Alpha = 1f;
	public float m_AlphaCutOffMask = 1f;
	public float m_AlphaCutOffSmooth = 1f;
	public float m_AlphaMask = 1f;
}


public class MatcapFXControl : MonoBehaviour {

	public Texture2D[] m_textureList;
	public Texture2D[] m_maskList;
	int m_idTexture = 0;
	int m_idMask = 0;
	public Preset[] m_presetlist;
	
	
	void Start()
	{
		ApplyPreset( m_presetlist[0] );
	}
	
	void OnGUI ()
	{		
		GUILayout.BeginArea( new Rect(0,0,Screen.width,Screen.height) );
			
		
		TextureSelection( "_MainTex", ref m_textureList, ref m_idTexture );
		Slider( "_Emission" );
		Slider( "_Alpha" );
		
		GUILayout.FlexibleSpace();
		
		foreach( Preset ps in m_presetlist )
			if( GUILayout.Button(ps.m_name, GUILayout.Width(200), GUILayout.Height(30)) )
				ApplyPreset(ps);
		
		GUILayout.FlexibleSpace();
		
		Slider( "_AlphaCutOffMask" );
		Slider( "_AlphaCutOffSmooth" );
		Slider( "_AlphaMask" );
		
		TextureSelection( "_MaskTex",ref m_maskList, ref m_idMask );
		
		GUILayout.EndArea();
	}
	
	
	void ApplyPreset( Preset ps )
	{
		m_idTexture = ps.m_idTexture;
		m_idMask = ps.m_idMask;
		GetComponent<Renderer>().material.SetTexture("_MainTex", m_textureList[m_idTexture]);
		GetComponent<Renderer>().material.SetTexture("_MaskTex", m_maskList[m_idMask]);
		
		GetComponent<Renderer>().material.SetFloat( "_Emission", ps.m_Emission );
		GetComponent<Renderer>().material.SetFloat( "_Alpha", ps.m_Alpha );
		GetComponent<Renderer>().material.SetFloat( "_AlphaCutOffMask", ps.m_AlphaCutOffMask );
		GetComponent<Renderer>().material.SetFloat( "_AlphaCutOffSmooth", ps.m_AlphaCutOffSmooth );
		GetComponent<Renderer>().material.SetFloat( "_AlphaMask", ps.m_AlphaMask );
	}
	
	void Slider( string property )
	{
		GUI.color = Color.black;
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label( property );
		GetComponent<Renderer>().material.SetFloat( property, GUILayout.HorizontalSlider( GetComponent<Renderer>().material.GetFloat(property), 0, 1, GUILayout.Width(300), GUILayout.Height(20) ) );
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUI.color = Color.white;
	}
	
	
	void TextureSelection( string textureName, ref Texture2D[] list, ref int id )
	{
		int prev = id-1<0?list.Length-1:id-1;	
		int next = (id+1)%list.Length;
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();		
		
		if( GUILayout.Button( "<", GUILayout.Width(30), GUILayout.Height(80) ) )
		{
			id = id-1<0?list.Length-1:id-1;	
			GetComponent<Renderer>().material.SetTexture(textureName, list[id]);
		}
		
		GUILayout.Label( list[prev], GUILayout.Width(80), GUILayout.Height(80) );
		GUILayout.Label( list[id], GUILayout.Width(100), GUILayout.Height(100) );
		GUILayout.Label( list[next], GUILayout.Width(80), GUILayout.Height(80) );
		
		if( GUILayout.Button( ">", GUILayout.Width(30), GUILayout.Height(80) ) )
		{
			id = (id+1)%list.Length;
			GetComponent<Renderer>().material.SetTexture(textureName, list[id]);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
	}

}
