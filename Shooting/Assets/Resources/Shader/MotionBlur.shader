Shader "Unlit/MotionBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : POSITION0;
				float4 Pos : POSITION1;
				float4 PrePos : POSITION2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4x4 _uModelViewProjectionMat;
			float4x4 _uPrevModelViewProjectionMat;
			
			v2f vert (appdata v)
			{
				v2f o;
				//o.Pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.Pos = mul(_uModelViewProjectionMat, v.vertex);
				//o.PrePos = mul(_uPrevModelViewProjectionMat, v.vertex);
				o.vertex = o.Pos;
				//o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f v) : COLOR
			{
				float a = (v.Pos.xy / v.Pos.w) * 2 + 0.5;
				float b = (v.PrePos.xy / v.PrePos.w) * 2 + 0.5;

				float2 oVelocity = a - b;

				fixed4 col = tex2D(_MainTex, v.uv);

				float nSamples = 8.0;

				for (int i = 1; i < nSamples; ++i) {
					float2 offset = oVelocity * (float(i) / float(nSamples - 1) - 0.5);
					col += tex2D(_MainTex, v.uv + offset);
				}
				col /= float(nSamples);

				return col;
			}
			ENDCG
		}
	}
}
