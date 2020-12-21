Shader "Custom/RingSkinReplacement" {
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _MaskTex("Mask", 2D) = "white" {}
        _XUVBlend("Blend", Range(0, 1)) = 0
        _XOffset("X blend offset", Range(0, 0.25)) = 0
        _BlurPower("Blur Power", Range(0, 0.25)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent"  }
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                sampler2D _MaskTex;
                float4 _MaskTex_ST;
                fixed _XUVBlend;
                fixed _BlurPower;
                fixed _XOffset;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed4 maskCol = tex2D(_MaskTex, i.uv);
                    // apply fog
                    UNITY_APPLY_FOG(i.fogCoord, col);
                    col.a = maskCol.a;
                    fixed a = i.uv.x % 1;
                    if (a < _XUVBlend) {//a > 1 - _XUVBlend
                        /*fixed4 c1 = tex2D(_MainTex, i.uv + (fixed2(1, 0) * _BlurPower));
                        fixed4 c2 = tex2D(_MainTex, i.uv - (fixed2(1, 0) * _BlurPower));
                        fixed4 c3 = tex2D(_MainTex, i.uv + (fixed2(0, 1) * _BlurPower));
                        fixed4 c4 = tex2D(_MainTex, i.uv - (fixed2(0, 1) * _BlurPower));
                        fixed4 cAverage = (c1 + c2 + c3 + c4) / 4;*/
                        fixed blend = 0;
                            blend = a / _XUVBlend;
                            fixed2 uv = fixed2(i.uv.x - 2*a, i.uv.y);
                            fixed4 sourceCol = tex2D(_MainTex, uv);
                            fixed4 lerpRes = lerp(sourceCol, col, blend* blend);
                                //return fixed4(blend, 0, 0, 1);
                            lerpRes.a = maskCol.a;
                        return lerpRes;
                    }
                    return col;
                }
                ENDCG
            }
        }
}