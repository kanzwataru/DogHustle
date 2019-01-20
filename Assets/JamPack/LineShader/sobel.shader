Shader "ImageEffect/sobel"
{
	Properties
	{
		_MainTex ("Screen RT", 2D) = "white" {}
		_FilterTex ("Sobel Filter Source RT", 2D) = "blue" {}
		_Threshold ("Split", Range(0.01, 5)) = 0.5
		_LineCol ("Line Colour", Color) = (0,0,0)
		//_Offset ("Split", Range(0.01, 1.0)) = 0.00333
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.0

			#include "UnityCG.cginc"

			//float _Offset;
			#define OFFSET 0.0005333 // 1 / 300

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _FilterTex;
			sampler2D _CameraDepthTexture;
			float _Threshold;
			fixed3 _LineCol;

			fixed4 frag (v2f i) : SV_Target
			{
				//return fixed4(tex2D(_FilterTex, i.uv).rgb);
				float2 offsets[9] = {
					float2(-OFFSET,  OFFSET), // top-left
					float2( 0.0,    OFFSET), // top-center
					float2( OFFSET,  OFFSET), // top-right
					float2(-OFFSET,  0.0),   // center-left
					float2( 0.0,     0.0),   // center-center
					float2( OFFSET,  0.0),   // center-right
					float2(-OFFSET, -OFFSET), // bottom-left
					float2( 0.0,   -OFFSET), // bottom-center
					float2( OFFSET, -OFFSET)  // bottom-right    
				};

				float kernel_x[9] = {
					-1, 0, 1,
					-2, 0, +2,
					-1, 0, 1
				};

				float kernel_y[9] = {
					-1, -2, -1,
					0, 0, 0,
					1, 2, 1
				};

				float3 sample_tex[9];
				float2 uv = i.uv;

				//#define DEPTH_TEX(n) clamp(pow(tex2D(_CameraDepthTexture, uv + offsets[n]).r, 15), 0.0, 1.0)
				//#define FILL_SAMPLE_TEX(n) sample_tex[n] = float3(tex2D(_FilterTex, uv + offsets[n]).rgb + DEPTH_TEX(n))
				#define FILL_SAMPLE_TEX(n) sample_tex[n] = float3(tex2D(_FilterTex, uv + offsets[n]).rgb)
				FILL_SAMPLE_TEX(0);
				FILL_SAMPLE_TEX(1);
				FILL_SAMPLE_TEX(2);
				FILL_SAMPLE_TEX(3);
				FILL_SAMPLE_TEX(4);
				FILL_SAMPLE_TEX(5);
				FILL_SAMPLE_TEX(6);
				FILL_SAMPLE_TEX(7);
				FILL_SAMPLE_TEX(8);

				float3 conv_x = 0.0f;
				float3 conv_y = 0.0f;

				for(int i = 0; i < 9; ++i) {
					conv_x += sample_tex[i] * kernel_x[i];
					conv_y += sample_tex[i] * kernel_y[i];
					//sobel += sample_tex[i] * kernel_x[i] * kernel_y[i];
				}

				float3 sobel = sqrt((conv_x * conv_x) + (conv_y * conv_y));
				float graysobel = dot(sobel, float3(0.2126, 0.7152, 0.0722));
				float lines = step(sobel, _Threshold);

				//return fixed4(graysobel,graysobel,graysobel, 1.0);
				return fixed4(lerp(_LineCol, tex2D(_MainTex, uv).rgb, lines), 1.0);
				//return fixed4(lines, lines, lines, 1.0f);
				//float depth = ;

				//return fixed4(depth, depth, depth, 1.0);
			}
			ENDCG
		}
	}
}
