Shader "Custom/FireOverlay"
{
    Properties
    {
        _SparkIntensity ("Spark Intensity", Range(0,1)) = 1
        _SparkSpeed ("Spark Speed", Float) = 2
        _SparkDensity ("Spark Density", Float) = 120
        _SparkLength ("Spark Length", Float) = 0.08

        _SparkColor ("Spark Color", Color) = (1,0.45,0,1)
        _GlowColor ("Glow Color", Color) = (1,0.8,0.3,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Blend SrcAlpha One
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

            float _SparkIntensity;
            float _SparkSpeed;
            float _SparkDensity;
            float _SparkLength;

            float4 _SparkColor;
            float4 _GlowColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float rand(float2 co)
            {
                return frac(
                    sin(dot(co.xy, float2(12.9898,78.233)))
                    * 43758.5453
                );
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float time = _Time.y * _SparkSpeed;
                float sparks = 0;
                float3 finalColor = 0;

                for (int layer = 0; layer < 3; layer++)
                {
                    float density = _SparkDensity * (1 + layer);
                    float2 suv = uv;
                    suv.y += time * (0.7 + layer * 0.3);
                    float2 grid = floor(suv * density);
                    float r = rand(grid);
                    float2 cellUV = frac(suv * density);
                    float xPos = frac(r * 13.7);

                    float2 sparkPos = float2(
                        xPos,
                        frac(r * 7.9)
                    );

                    float vertical =
                        abs(cellUV.y - sparkPos.y);

                    float horizontal =
                        abs(cellUV.x - sparkPos.x);

                    float streak =
                        smoothstep(
                            _SparkLength,
                            0.0,
                            vertical
                        ) *
                        smoothstep(
                            0.02,
                            0.0,
                            horizontal
                        );

                    streak *= (0.5 + r);
                    sparks += streak;

                    float glow =
                        smoothstep(
                            0.08,
                            0.0,
                            distance(cellUV, sparkPos)
                        );

                    finalColor +=
                        (_SparkColor.rgb * streak) +
                        (_GlowColor.rgb * glow * 0.5);
                }

                sparks *= _SparkIntensity;
                return float4(finalColor, sparks);
            }

            ENDCG
        }
    }
}