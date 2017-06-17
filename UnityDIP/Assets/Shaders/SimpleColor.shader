Shader "DIP/SimpleColor"
{
    Properties {
        _Color ("Color", Color) = (0, 0, 0, 0)
    }
    Subshader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 _Color;

            struct vert_input
            {
                float4 vertex : POSITION;    
            };

            struct vert_output
            {
                float4 vertex : SV_POSITION;
            };

            vert_output vert(vert_input i)
            {
                vert_output o;

                o.vertex = UnityObjectToClipPos(i.vertex);

                return o;
            }

            half4 frag(vert_output o) : COLOR
            {
                return _Color;
            }

            ENDCG
        }
    }
}