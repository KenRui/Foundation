Shader "Moba/OutlineFilter"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OutLineColor("OutLineColor", Color) = (0,1,0,0)
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
				float4 uv2 : TEXCOORD1;
				float4 uv3 : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _TexelOffset0;
			float4 _TexelOffset1;
			fixed _BlendFactor;
			fixed4 _OutLineColor;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv2 = float4(o.uv, o.uv) + _TexelOffset0;
				o.uv3 = float4(o.uv, o.uv) + _TexelOffset1;
				return o;
			}					

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 colOffset;
				colOffset.x = tex2D(_MainTex, i.uv2.xy).w;
				colOffset.y = tex2D(_MainTex, i.uv2.zw).w;
				colOffset.z = tex2D(_MainTex, i.uv3.xy).w;
				colOffset.w = tex2D(_MainTex, i.uv3.zw).w;
				fixed4 diff = colOffset - col.w;
				fixed distance = clamp(sqrt(dot(diff, diff)) * 1000.0, 0.0, 1.0);
				col.xyz = lerp(_OutLineColor.xyz, col.xyz, (1.0 - distance * _BlendFactor));
				return col;
			}
			ENDCG
		}
	}	
}
