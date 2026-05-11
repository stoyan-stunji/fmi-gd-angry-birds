Shader "Custom/SnowOverlay"
{
    Properties
    {
        _SnowIntensity ("Snow Intensity", Range(0,1)) = 0.5
        _SnowSpeed ("Snow Speed", Float) = 1
        _SnowScale ("Snow Scale", Float) = 80
        _SnowColor ("Snow Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _SnowIntensity;
            float _SnowSpeed;
            float _SnowScale;
            float4 _SnowColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898,78.233))) * 43758.5453);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float time = _Time.y * _SnowSpeed;
                float snow = 0;

                for (int layer = 0; layer < 3; layer++)
                {
                    float scale = _SnowScale * (1 + layer * 0.5);
                    float2 suv = uv;
                    suv.y += time * (0.3 + layer * 0.2);
                    suv.x += sin(suv.y * 10 + layer * 20) * 0.02;
                    float2 grid = floor(suv * scale);
                    float n = rand(grid);
                    float2 cellUV = frac(suv * scale);

                    float2 offset = float2(
                        frac(n * 13.7),
                        frac(n * 7.3)
                    );

                    float dist = distance(cellUV, offset);
                    snow += smoothstep(
                        0.12,
                        0.0,
                        dist
                    ) * (0.4 / (layer + 1));
                }

                snow *= _SnowIntensity;
                return float4(_SnowColor.rgb, snow);
            }

            ENDCG
        }
    }
}