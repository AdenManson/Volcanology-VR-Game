Shader"LowPolyWater/WaterShadedURP" {
    Properties { 
        _BaseColor ("Base color", Color) = (0.54, 0.95, 0.99, 0.5)
        _SpecColor ("Specular Material Color", Color) = (1, 1, 1, 1)
        _Shininess ("Shininess", Range(0.01, 1)) = 0.5
        _ShoreTex ("Shore & Foam texture", 2D) = "black" {}
        _BumpTiling ("Foam Tiling", Vector) = (1.0, 1.0, -2.0, 3.0)
        _BumpDirection ("Foam movement", Vector) = (1.0, 1.0, -1.0, 1.0)
        _Foam ("Foam (intensity, cutoff)", Vector) = (0.1, 0.375, 0.0, 0.0)
        _isInnerAlphaBlendOrColor("Fade inner to color or alpha?", Float) = 0
    }

    SubShader {
        Tags {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

LOD500
        ColorMask
RGB

        GrabPass
{"_RefractionTex"
}

        Pass {
            Tags {"LightMode"="ForwardBase"}
Blend SrcAlpha
OneMinusSrcAlpha
            ZWrite
Off
            Cull
Off
            Fog
{
    ModeOff
}
            
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : TEXCOORD1;
};

sampler2D _ShoreTex;
float4 _BaseColor;
float4 _SpecColor;
float _Shininess;
float4 _BumpTiling;
float4 _BumpDirection;
float4 _Foam;
float _isInnerAlphaBlendOrColor;

v2f vert(appdata_t v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.vertex.xy;
    o.normal = UnityObjectToWorldNormal(v.normal);
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 baseColor = _BaseColor;

                // Perform your calculations here

    return baseColor;
}
            ENDCG
        }
    }
}
