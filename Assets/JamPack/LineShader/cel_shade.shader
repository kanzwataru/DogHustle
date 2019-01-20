Shader "Custom/CelShaded"
{
   Properties
   {
       _LitCol("Lit", Color) = (0.4,0.5,0.6)
       _DarkCol("Dark", Color) = (0.2,0.2,0.3)
       _Split ("Split", Range(0.01, 1.0)) = 0.5
       //_MainTex ("Texture", 2D) = "white" {}
   }
   SubShader
   {
       Pass
       {
       	   Tags {
       	       "LightMode" = "ForwardBase"
       	   }
           CGPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           #pragma multi_compile_fwdbase
           #pragma target 3.0

           #include "UnityCG.cginc"
           #include "Lighting.cginc"
           #include "AutoLight.cginc"


           struct v2f {
           	   float4 pos : SV_POSITION;
               float2 uv : TEXCOORD0;
               float3 normal : TEXCOORD3;
               float3 screenPos : TEXCOORD4;
               LIGHTING_COORDS(1,2)
           };

		   fixed  _Split;
           fixed4 _LitCol;
           fixed4 _DarkCol;
            
           v2f vert (appdata_base v)
           {
               v2f o;
               //o.uv = uv;
               o.pos = UnityObjectToClipPos(v.vertex);
               o.normal = UnityObjectToWorldNormal(v.normal);
               o.screenPos = o.pos.xyw;
               o.screenPos.y *= _ProjectionParams.x;

               TRANSFER_VERTEX_TO_FRAGMENT(o);

               return o;
           }
 
           fixed4 frag (v2f i) : SV_Target
           {
               //screenPos.xy = floor(screenPos.xy * 0.25) * 0.5;
               //float checker = -frac(screenPos.r + screenPos.g);
 
               //clip(checker);
               float2 screenUV = (i.screenPos.xy / i.screenPos.z) * 0.5f + 0.5f;
               // diffuse
               float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
               half attenuation = LIGHT_ATTENUATION(i);
               half lambert = dot(i.normal, lightDir);

               half halflamb = pow((lambert * attenuation) * 0.5 + 0.5, 2);

               float4 result;
               if(halflamb > _Split) {
               		result = _LitCol;
               }
               else {
                    result = _DarkCol;
               }
               //fixed4 c = fixed4(checker, checker, checker, 1.0);
               return result;
               //return attenuation;
               //return fixed4(1.0,1.0,1.0,1.0) * attenuation;
           }
           ENDCG
       }
   }

   Fallback "VertexLit"
}