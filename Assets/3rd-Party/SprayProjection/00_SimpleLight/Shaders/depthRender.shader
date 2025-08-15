Shader "Unlit/depthRender"
{
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
				float4 position : POSITION;
				float4 color: COLOR;
			};

			struct v2f
			{
				float4 color: COLOR;
				float depth : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata i)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(i.position);
				o.depth = abs(UnityObjectToViewPos(i.position).z);
				o.color = i.color;
				return o;
			}
			
			float frag (v2f i) : SV_Target
			{
				return i.depth;
			}
			ENDCG
		}
	}
}
