Shader "Custom/ThunderFlash"
{
    Properties
    {
        _FlashStrength ("Flash Strength", Range(0,1)) = 0
        _Tint ("Tint", Color) = (0.8,0.9,1,1)
        _NoiseScale ("Noise Scale", Float) = 20
        _FlickerSpeed ("Flicker Speed", Float) = 40
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

            float _FlashStrength;
            float4 _Tint;
            float _NoiseScale;
            float _FlickerSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float random(float2 st)
            {
                return frac(sin(dot(st.xy,
                    float2(12.9898,78.233))) *
                    43758.5453123);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float noise = random(floor(uv * _NoiseScale + _Time.y * _FlickerSpeed));
                float flash = _FlashStrength * (0.7 + noise * 0.3);
                return fixed4(_Tint.rgb, flash);
            }
            ENDCG
        }
    }
}