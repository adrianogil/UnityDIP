Shader "ColorSpace/RGB"
{
    Properties {
        _CubePosition ("Cube Position", Vector) = (1, 1, 1, 1) // (R, G, B, A)
    }

    Subshader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float3 _CubePosition;

            struct v2f {
                float4 pos : SV_POSITION;   // Clip space
                float3 wPos : TEXCOORD1;    // World position
            };

            // Vertex function
            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }


            float4 frag(v2f i) : COLOR
            {
                // float3 rgbPos = 0.5 + 0.5 * normalize(i.wPos - _CubePosition);
                float3 rgbPos = i.wPos;

                float4 color = 1;
                color.rgb = rgbPos;

                return color;
            }

            ENDCG
        }
    }
}