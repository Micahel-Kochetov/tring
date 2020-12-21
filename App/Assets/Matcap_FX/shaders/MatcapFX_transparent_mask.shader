Shader "MatcapFX/MatcapFX_transparent_mask" {

 Properties {
      _MainTex ("Matcap", 2D) = "white" {}
      _Emission ("Emission", Range (0,1)) = 1
      _MaskTex ("Mask", 2D) = "white" {}
      _AlphaCutOffMask ("Alpha mask cutoff", Range (0,1)) = 0
      _AlphaCutOffSmooth ("Cutoff smooth", Range (0,1)) = 0
      _AlphaMask ("Alpha Mask", Range (0,1)) = 1
      _Alpha ("Alpha", Range (0,1)) = 1
    }
    SubShader {
    
     Tags { "Queue" = "Transparent" }
    Cull Back
	ZWrite On
	ZTest LEqual
	ColorMask RGBA
	AlphaTest Greater 0
	Lighting Off  
 	Blend SrcAlpha OneMinusSrcAlpha 

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
      sampler2D _MaskTex;
      float _AlphaCutOffMask;
      float _AlphaCutOffSmooth;
      float _AlphaMask;
      float _Alpha;
      float _Emission;
      
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.customUV).rgb;
          
          float smoothThreshold = _AlphaCutOffSmooth / 10;
          float col = tex2D (_MaskTex, IN.customUV).r;
          float p = _AlphaCutOffMask - ( smoothThreshold );

          if( tex2D (_MaskTex, IN.customUV).r > _AlphaCutOffMask || _AlphaCutOffMask ==  0)
          	o.Alpha =  (col+ 1-_AlphaMask) * _Alpha ;
          	
          else if( col > ( p ) )
          {
         	o.Alpha =  ( tex2D (_MaskTex, IN.customUV).r - p ) / ( smoothThreshold ) * (tex2D (_MaskTex, IN.customUV).r+ 1-_AlphaMask) * _Alpha;
         	o.Alpha = o.Alpha * o.Alpha;
          }
          else
         	o.Alpha = 0;         	
         o.Emission = tex2D (_MainTex, IN.customUV).rgb * _Emission;
      }
      ENDCG
     
      
      
    } 
    
    
    Fallback "Diffuse"
  }

