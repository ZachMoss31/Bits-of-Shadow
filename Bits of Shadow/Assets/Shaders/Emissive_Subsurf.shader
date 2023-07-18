Shader "Emissive_Subsurf"
{
    //Source :: https://www.youtube.com/watch?v=Nd4vKyDGidY
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Albedo("Albedo (RGB), Alpha(A)", 2D) = "white" {}
        _Normal("Normal(RGB)", 2D) = "bump" {}
        _MaskMap("Mask Map(Metallic, Occlusion, Detail Mask, Smoothness)", 2D) = "black" {}
        //IF YOU DONT HAVE MASK MAP, then you need to separate these inputs like they are commented below.
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0

        _Emission("Emission", 2D) = "black" {}
        _EmisionColor("Color", Color) = (1,1,1,1)
        _EmissionIntensity("Intensity", Float) = 1.0
        _EmissionGlow("Glow", Float) = 1.0
        _EmissionGlowDuration("GlowDuration", Float) = 5.0
    }
    SubShader
    {
        Tags 
        { 
            "Queue" = "Geometry" 
            "RenderType"="Opaque" 
        }
        LOD 200

        CGPROGRAM


        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        #include "UnityPBSLighting.cginc"
        #pragma surface surf Standard

        struct Input
        {
            float2 uv_Albedo;
        };

        //half _Glossiness;
        //half _Metallic;
        sampler2D _Albedo;
        float4 _Color;
        sampler2D _Normal;
        sampler2D _MaskMap;
        sampler2D _Emission;
        float4 _EmissionColor;
        float _EmissionIntensity;
        float _EmissionGlow;
        float _EmissionGlowDuration;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 albedo = tex2D (_Albedo, IN.uv_Albedo);
            fixed4 mask = tex2D(_MaskMap, IN.uv_Albedo);
            fixed3 normal = UnpackScaleNormal(tex2D(_Normal, IN.uv_Albedo), 1);
            fixed4 emission = tex2D(_Emission, IN.uv_Albedo);

            o.Albedo = albedo.rgb * _Color;
            o.Alpha = albedo.a;
            o.Normal = normal;
            
            //Mask map variables here
            o.Metallic = mask.r;
            o.Occlusion = mask.g;
            //o.DetailMask = mask.b;
            o.Smoothness = mask.a;


            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;

            //To get the emission color 
            o.Emission = emission.rgb * _EmissionColor * (_EmissionIntensity + abs(frac(_Time.y * (1 / _EmissionGlowDuration)) -0.5) * _EmissionGlow);
        ENDCG
    }
    FallBack "Diffuse"
}
