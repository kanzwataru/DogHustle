Shader "Custom/VertexColDepthShader"
{
	Properties
	{
		//_MainTex ("Texture", 2D) = "white" {}
		_Contrast ("Contrast", Range(1, 500)) = 300.0
		_Factor ("Factor", Range(0.01, 10.0)) = 7.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			//sampler2D _MainTex;
			//float4 _MainTex_ST;
			float _Contrast;
			float _Factor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 depth = float4(i.vertex.z, i.vertex.z, i.vertex.z, 1.0);
				float4 final_depth = 1 - (pow(depth, _Contrast) * _Factor);
				//return final_depth * i.color;
				return i.color;
			}
			ENDCG
		}
	}
}
