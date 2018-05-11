Shader "Perceptron/CornerDetection"
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

            float greater_than(float a, float b)
            {
                return max(0, sign(a - b));
            }

            half4 frag(vert_output o) : COLOR
            {
                float4 texColors[9];
                float _KernelDistance = 1;

                float perceptronWeights[9];
                perceptronWeights[0] = -1.36416912745; 
                perceptronWeights[1] = -5.44191188293; 
                perceptronWeights[2] = -2.51271426944; 
                perceptronWeights[3] = -3.12708165557; 
                perceptronWeights[4] = 4.69739593234; 
                perceptronWeights[5] = 0.376031565301; 
                perceptronWeights[6] = -2.48486765578; 
                perceptronWeights[7] = -0.969443054146; 
                perceptronWeights[8] = -2.94478824045; 


                #define GRABPIXEL(px,py) tex2D( _MainTex, o.uv + _KernelDistance * float2(px * _MainTex_TexelSize.x, py * _MainTex_TexelSize.y))

                texColors[0] = perceptronWeights[0] * greater_than(length(GRABPIXEL(-1,-1) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[1] = perceptronWeights[1] * greater_than(length(GRABPIXEL(-1, 0) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[2] = perceptronWeights[2] * greater_than(length(GRABPIXEL(-1, 1) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[3] = perceptronWeights[3] * greater_than(length(GRABPIXEL( 0,-1) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[4] = perceptronWeights[4] * greater_than(length(GRABPIXEL( 0, 0) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[5] = perceptronWeights[5] * greater_than(length(GRABPIXEL( 0, 1) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[6] = perceptronWeights[6] * greater_than(length(GRABPIXEL( 1,-1) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[7] = perceptronWeights[7] * greater_than(length(GRABPIXEL( 1, 0) - float4(0.1,0.1,0.1,1)), 0.5);
                texColors[8] = perceptronWeights[8] * greater_than(length(GRABPIXEL( 1, 1) - float4(0.1,0.1,0.1,1)), 0.5);


                float n = texColors[0] +
                        texColors[1] +
                        texColors[2] +
                        texColors[3] +
                        texColors[4] +
                        texColors[5] +
                        texColors[6] +
                        texColors[7] +
                        texColors[8];


                if (n > 0)
                    return float4(1,1,1,1);

                return float4(0,0,0,1);

                // return tex2D(_MainTex, o.uv);
            }

            ENDCG
        }
    }
}