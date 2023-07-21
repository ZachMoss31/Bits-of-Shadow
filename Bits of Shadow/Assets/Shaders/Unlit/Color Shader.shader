Shader "Unlit/Color Shader"
{
    Properties
    {
        _ColorA("ColorA", Color) = (1,1,1,1)
        _ColorB("ColorB", Color) = (1,1,1,1)
        _ColorStart("Color Start", Range(0,1)) = 0
        _ColorEnd("Color End", Range(0,1)) = 1
    }
        SubShader
    {
        //Writing after opaque for transparent shader
        Tags { "RenderType" = "Transparent"
               "Queue" = "Transparent"
             }
        Pass
        {
            //DEPTH BUFFER
            // Can allow character behind objects render shader, rain effect only in rain, etc.
            //Render both sides for transparent object
            Cull Off
            //Writing to depth buffer changes due to transparency
            ZWrite Off
            //Blending and other pre-CG Programs
            Blend One One //add blend

            //Blend DstColor Zero //mult blend

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define TAU 6.28318530718

            //Variables
            //This is accessible by both vertex and fragment shaders.
            float4 _ColorA;
            float4 _ColorB;
            float _ColorStart;
            float _ColorEnd;

            //UV/Positional Data
            struct MeshData
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv0 : TEXCOORD0;
            };

            //Interpolators (appdata)
            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float3 normal :TEXCOORD0;
                float2 uv : TEXCOORD1;
            };


            //Vertex Shader
            //!!! DO AS MUCH AS YOU CAN IN VERTEX SHADER BEFORE FRAG
            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv0;

                return o;
            }

            //Not usually built into shader languages for some reason
            float InverseLerp(float startVal, float endVal, float inputVal) {
                return (inputVal - startVal) / (endVal - startVal);
            }

            //Fragment Shader
            fixed4 frag(Interpolators i) : SV_Target
            {
                //_Time.xyzw have differing portions of time. Can just use _Time and scale it.
                //_Time

                float xOffset = cos(i.uv.x * TAU * 8) * 0.01;
                float t = cos( (i.uv.y + xOffset - _Time.y * .3) * TAU * 5) * 0.5 + 0.5;

                //this allows the gradient of opaqueness on the bottom
                t *= 1-i.uv.y;



                //this lil trick instead of returning t takes the y vals up and down off.
                float topBottomRemover = (abs(i.normal.y) < 0.999);
                float waves = t * topBottomRemover;

                float4 gradient = lerp(_ColorA, _ColorB, i.uv.y);

                return gradient * waves;
            }
            ENDCG
        }
    }
}
