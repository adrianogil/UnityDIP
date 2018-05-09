Shader "Custom/BWEdges"
{
    Properties
    {
        _MainTex("Image", 2D) = "white"
    }
    Subshader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler _MainTex;
            float4 _MainTex_TexelSize;

            struct vert_input
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vert_output
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            vert_output vert(vert_input i)
            {
                vert_output o;

                o.vertex = UnityObjectToClipPos(i.vertex);
                o.uv = i.uv;

                return o;
            }

            float4 frag(vert_output o) : COLOR
            {
            	float4 texColors[9];
            	float _KernelDistance = 1;
                #define GRABPIXEL(px,py) tex2D( _MainTex, o.uv + _KernelDistance * float2(px * _MainTex_TexelSize.x, py * _MainTex_TexelSize.y))

                texColors[0] = GRABPIXEL(-1,-1);
                texColors[1] = GRABPIXEL(-1, 0);
                texColors[2] = GRABPIXEL(-1, 1);
                texColors[3] = GRABPIXEL( 0,-1);
                texColors[4] = GRABPIXEL( 0, 0);
                texColors[5] = GRABPIXEL( 0, 1);
                texColors[6] = GRABPIXEL( 1,-1);
                texColors[7] = GRABPIXEL( 1, 0);
                texColors[8] = GRABPIXEL( 1, 1);



                float distance = length(texColors[4] - texColors[0]) + 
                					length(texColors[4] - texColors[1]) + 
                					length(texColors[4] - texColors[2]) + 
                					length(texColors[4] - texColors[3]) +
                					length(texColors[4] - texColors[5]) + 
                					length(texColors[4] - texColors[6]) + 
                					length(texColors[4] - texColors[7]) + 
                					length(texColors[4] - texColors[8]);

                if (distance > 0.1)
                {
                	return float4(1,1,1,1);
                }

                return float4(0.1,0.1,0.1,1);

                // return tex2D(_MainTex, o.uv);
                // return float4(1,0,0,1);
            }

            ENDCG
        }
    }
}