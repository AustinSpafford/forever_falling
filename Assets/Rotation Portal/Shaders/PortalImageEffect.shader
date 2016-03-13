Shader "Custom/PortalImageEffect"
{
	Properties
	{
		_LeftEyeTexture ("Left Eye (RGB)", 2D) = "white" {}
		_RightEyeTexture ("Right Eye (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		
		ZTest off
		ZWrite off

		Stencil
		{
			Ref 1
			Comp equal
			Pass keep
			Fail keep
		}
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _LeftEyeTexture;
			uniform sampler2D _RightEyeTexture;

			float4 frag(v2f_img input) : COLOR
			{
				// BROKEN! This value is always 0.
				if (unity_CameraProjection[0][2] < 0)
				{
					return tex2D(_LeftEyeTexture, input.uv);
				}
				else
				{
					return tex2D(_RightEyeTexture, input.uv);
				}
			}
		ENDCG
		}
	}
	FallBack "Diffuse"
}
