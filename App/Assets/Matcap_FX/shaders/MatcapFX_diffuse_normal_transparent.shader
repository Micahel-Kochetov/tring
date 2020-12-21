Shader "MatcapFX/MatcapFX_diffuse_normal_transparent" {

 Properties {
      _Diffuse ("Diffuse", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
      _MainTex ("Matcap", 2D) = "white" {}
      _Matcap ("Matcap", Range (0,1)) = 1
      _Emission ("Emission", Range (0,1)) = 1
      _Alpha ("Alpha", Range (0,1)) = .5
    }
    SubShader {
    Tags { "Queue" = "Transparent" }
    Cull Back
	ZWrite On
	ZTest LEqual
	ColorMask RGBA
	Lighting Off  
	AlphaTest Greater 0
	Blend SrcAlpha OneMinusSrcAlpha 

      CGPROGRAM
      #pragma surface surf Lambert vertex:vert
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 customUV;
      };
      void vert (inout appdata_full v, out Input o) {
      	  
          UNITY_INITIALIZE_OUTPUT(Input, o);
          o.customUV =mul((float3x3)UNITY_MATRIX_IT_MV,v.normal) * 0.5 + 0.5;
      }
      sampler2D _MainTex;
      sampler2D _Diffuse;
      sampler2D _BumpMap;
      float _Alpha;
      float _Emission;
      float _Matcap;
      float3 _customUV;
      void surf (Input IN, inout SurfaceOutput o) {
		  _customUV = tex2D(_BumpMap, IN.uv_BumpMap).xyz;//* IN.customUV;
          o.Albedo = ( tex2D (_MainTex, _customUV).rgb*_Matcap + tex2D (_Diffuse, IN.uv_MainTex).rgb *(1-_Matcap) ) ;
          o.Emission = o.Albedo * _Emission;
          o.Alpha = _Alpha;
      }
      ENDCG     
      
    } 
   
    
    Fallback "Diffuse"
  }

