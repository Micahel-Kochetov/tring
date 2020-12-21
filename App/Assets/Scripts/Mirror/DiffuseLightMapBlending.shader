Shader "Blend 2 Textures, Simply Lit" { 
     
 Properties {
     _Color ("Color Tint (A = Opacity)", Color) = (1,1,1,1)
     _Blend ("Blend", Range (0,1)) = 0.0 
     _MainTex1 ("Texture1", 2D) = "" 
     _MainTex2 ("Texture2", 2D) = ""
 }
 Category {
     Material {
         Ambient[_Color]
         Diffuse[_Color]
     }
     // iPhone 3GS and later
     SubShader {
     Tags {Queue = Transparent}
     ZWrite On
     Cull Off
     Blend SrcAlpha OneMinusSrcAlpha
     Pass {
         Lighting On
         SetTexture[_MainTex1]
         SetTexture[_MainTex2] { 
             ConstantColor (0,0,0, [_Blend]) 
             Combine texture Lerp(constant) previous
         }
         SetTexture[_] {Combine previous * primary Double}
     }}
     
     // pre-3GS devices, including the September 2009 8GB iPod touch
     SubShader 
     {
         Pass {
             SetTexture[_MainTex1]
             SetTexture[_MainTex2] {
                 ConstantColor (0,0,0, [_Blend])
                 Combine texture Lerp(constant) previous
             }
         }
         Pass {
             Lighting On
             Blend DstColor SrcColor
         }
     }
 }
 
 }