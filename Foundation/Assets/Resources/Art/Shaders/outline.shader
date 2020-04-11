Shader "Custom/OutLine"
{
    Properties
    {
        _OutLineColor("OutLineColor", Color) = (0,0,0,1)
    }
    SubShader
    {

        Stencil{
            Ref 128
            Comp NotEqual
        }

        // Blend ONE ZERO

        Pass{
            
            // ZWrite Off
            // Cull Front
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            

            struct v2f{
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldViewDir : TEXCOORD1;  
            };

            fixed4 _OutLineColor;

            v2f vert(appdata_full v)
            {
                v2f o;  
                o.pos = UnityObjectToClipPos(v.vertex);   
                float3 vnormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);    
                float2 offset = TransformViewToProjection(vnormal.xy);   
                o.pos.xy += offset * 0.01;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);//    mul(v.normal, (float3x3)unity_WorldToObject);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;  
                o.worldViewDir = _WorldSpaceCameraPos.xyz - worldPos;
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
