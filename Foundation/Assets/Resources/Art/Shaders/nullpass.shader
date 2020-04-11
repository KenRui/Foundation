Shader "Custom/NullPass"
{
    Properties
    {
        
    }
    SubShader
    {
        Stencil{
            Ref 128
            Comp Always
            pass Replace
        }

        Pass{
            
            
        }

    }
    FallBack "Diffuse"
}
