Shader "MatcapFX/MatcapFX_transparent" {

 Properties {
      _MainTex ("Matcap", 2D) = "white" {}
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
	Blend SrcAlpha OneMinusSrcAlpha 
	AlphaTest Greater 0
	

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
      float _Alpha;
      float _Emission;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.customUV).rgb;
          o.Emission = tex2D (_MainTex, IN.customUV).rgb * _Emission;
          o.Alpha = _Alpha;
      }
      ENDCG
     
      
    } 
    
    
    Fallback "Diffuse"
  }

