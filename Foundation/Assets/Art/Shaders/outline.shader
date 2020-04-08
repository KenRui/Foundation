Shader "Custom/OutLine"
{
    Properties
    {
        _OutLineColor("OutLineColor", Color) = (1,0,0,1)
    }
    SubShader
    {
        // pass{
            
        // }

        Pass{
            
            ZWrite Off
            Cull Front
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            

            struct v2f{
                float4 vertex : SV_POSITION;
            };

            fixed4 _OutLineColor;

            v2f vert(appdata_full v)
            {
                v2f o;
                v.vertex.xyz += v.normal * 0.02;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                return _OutLineColor;
            }
            

            ENDCG
        }

        // pass{
            
        // }
    }
    FallBack "Diffuse"
}
