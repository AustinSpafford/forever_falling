Shader "Custom/PortalStencilEnableShader"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue"="Geometry" }

		// Avoid generating visible effects, as this shader only writes to the stencil.
		ColorMask 0
		ZWrite off

		Stencil
		{
			Ref 1
			Comp always
			Pass replace
		}
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				// NOTE: The color is just used for debugging by disabling the ColorMask.
				return half4(1, 1, 0, 1);
			}
		ENDCG
		}
	}
}
