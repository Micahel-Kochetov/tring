Shader "MatcapFX/MatcapFX_diffuse" {

 Properties {
      _Diffuse ("Diffuse", 2D) = "white" {}
      _MainTex ("Matcap", 2D) = "white" {}
      _Matcap ("Matcap", Range (0,1)) = 1
      _Emission ("Emission", Range (0,1)) = 1
    }
    SubShader {
    Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }
    Cull Back
	ZWrite On
	ZTest LEqual
	ColorMask RGBA
	Lighting Off  

      CGPROGRAM
      #pragma surface surf Lambert vertex:vert keepalpha
      struct Input {
          float2 uv_MainTex;
          float3 customUV;
      };
      void vert (inout appdata_full v, out Input o) {
          UNITY_INITIALIZE_OUTPUT(Input, o);
          o.customUV =( mul((float3x3)UNITY_MATRIX_IT_MV,v.normal) * 0.5 + 0.5) ;
      }
      sampler2D _MainTex;
      sampler2D _Diffuse;
      float _Alpha;
      float _Emission;
      float _Matcap;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = ( tex2D (_MainTex, IN.customUV).rgb*_Matcap + tex2D (_Diffuse, IN.uv_MainTex).rgb *(1-_Matcap) ) ;
          o.Emission = o.Albedo * _Emission;
      }
      ENDCG     
      
    } 
   
    
    Fallback "Diffuse"
  }

