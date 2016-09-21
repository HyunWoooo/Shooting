Shader "Unlit/MotionBlur2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

	CGINCLUDE

	#include "UnityCG.cginc"

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

	sampler2D _MainTex;
	sampler2D _VelTex;
	sampler2D_float _CameraDepthTexture;

	float4 _MainTex_ST;

	float4 _MainTex_TexelSize;

	float4x4 _ToPrevViewProjCombined;

	float _VelocityScale;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);

		return o;
	}

	float4 CameraVelocity(v2f i) : SV_Target
	{
		float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);

		float3 clipPos = float3(i.uv.x*2.0 - 1.0, (i.uv.y)*2.0 - 1.0, depth);

		float4 prevClipPos = mul(_ToPrevViewProjCombined, float4(clipPos, 1.0));

		float2 vel = (clipPos.xy - prevClipPos.xy) / 2.f;

		return float4(vel, 0.0, 0.0);
	}

	float4 Blur(v2f i) : SV_Target
	{
		float2 uv = i.uv;

		float2 velocity = tex2D(_VelTex, i.uv).xy;

		float nSamples = 8.0;

		float4 sum = float4(0, 0, 0, 0);
		for (int i = 0; i<nSamples; i++)
		{
			float offset = velocity * ((float)i / (float)(nSamples - 1)- 0.5);
			sum += tex2D(_MainTex, uv + offset);
		}
		sum /= nSamples;
		return sum;
	}

	ENDCG

	Subshader {

		Pass{
			ZTest Always Cull Off ZWrite On Blend Off

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment CameraVelocity

			ENDCG
		}

		Pass{
			ZTest Always Cull Off ZWrite Off Blend Off

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment Blur

			ENDCG
		}
	}
	Fallback off
}
