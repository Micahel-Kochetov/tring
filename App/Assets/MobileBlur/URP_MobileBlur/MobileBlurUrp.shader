Shader "RufatShaderlab/BlurUrp"
{
	Properties
	{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "" {}
		[HideInInspector]_MaskTex("Base (RGB)", 2D) = "white" {}
	}
	HLSLINCLUDE

	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


	TEXTURE2D_X(_MainTex);
	SAMPLER(sampler_MainTex);
	TEXTURE2D_X(_BlurTex);
	SAMPLER(sampler_BlurTex);
	TEXTURE2D_X(_MaskTex);
	SAMPLER(sampler_MaskTex);

	half _BlurAmount;
	half4 _MainTex_TexelSize;
	half4 _MainTex_ST;
	half4 _MaskTex_ST;

	static const half4 curve[6] = {
		half4(3.0h, 3.0h, 3.0h, 3.0h),
		half4(2.0h, 2.0h, 2.0h, 2.0h),
		half4(0.0667h, 0.0667h, 0.0667h, 0.0667h),
		half4(0.091h, 0.091h, 0.091h, 0.091h),
		half4(0.1111h, 0.1111h, 0.1111h, 0.1111h),
		half4(0.2h, 0.2h, 0.2h, 0.2h)
	};

	struct appdata
	{
		half4 vertex : POSITION;
		half2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv  : TEXCOORD0;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	struct v2fb
	{
		half4 pos  : SV_POSITION;
		half4  uv : TEXCOORD0;
		half2  uv1 : TEXCOORD1;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	v2f vert(appdata i)
	{

		v2f o = (v2f)0;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); 
		o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, half4(i.vertex.xyz, 1.0h)));
		o.uv.xy = UnityStereoTransformScreenSpaceTex(i.uv);
		o.uv.zw = i.uv;
		return o;
	}

	v2fb vertb(appdata i)
	{
		v2fb o = (v2fb)0;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, half4(i.vertex.xyz, 1.0h)));
		o.uv1 = UnityStereoTransformScreenSpaceTex(i.uv);
		half2 offset = _MainTex_TexelSize.xy * _BlurAmount.xx * (1.0h / _MainTex_ST.xy);
		o.uv = half4(UnityStereoTransformScreenSpaceTex(i.uv - offset), UnityStereoTransformScreenSpaceTex(i.uv + offset));
		return o;
	}

	half4 fragb(v2fb i) : SV_Target
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		half4 c = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1);
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv1.x, i.uv.y));
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv.x, i.uv1.y));
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv1.x, i.uv.w));
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv.z, i.uv1.y));
#ifdef KERNEL
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xy);
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xw);
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.zy);
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.zw);
		return c * curve[4];
#endif
		return c * curve[5];
	}

	half4 fragg(v2fb i) : SV_Target
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		half4 c = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1) * curve[0];
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv1.x, i.uv.y)) * curve[1];
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv.x, i.uv1.y)) * curve[1];
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv1.x, i.uv.w)) * curve[1];
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, half2(i.uv.z, i.uv1.y)) * curve[1];
#ifdef KERNEL
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xy);
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xw);
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.zy);
		c += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.zw);
		return c * curve[2];
#endif
		return c * curve[3];
	}

	half4 frag(v2f i) : SV_Target
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		half4 c = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xy);
		half4 m = SAMPLE_TEXTURE2D_X(_MaskTex, sampler_MaskTex, i.uv.zw);
		half4 b = SAMPLE_TEXTURE2D_X(_BlurTex, sampler_BlurTex, i.uv.xy);
		return lerp(c, b, m.r);
	}

	float2 TransformStereoScreenSpace(float2 uv, float w)
	{
		float4 scaleOffset = unity_StereoScaleOffset[unity_StereoEyeIndex];
		return uv.xy * scaleOffset.xy + scaleOffset.zw * w;
	}

	ENDHLSL

	Subshader
	{
		Pass //0
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma shader_feature KERNEL
		  #pragma vertex vertb
		  #pragma fragment fragb
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDHLSL
		}
		Pass //1
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma shader_feature KERNEL
		  #pragma vertex vertb
		  #pragma fragment fragg
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDHLSL
		}
		Pass //2
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma vertex vert
		  #pragma fragment frag
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDHLSL
		}
	}
}
