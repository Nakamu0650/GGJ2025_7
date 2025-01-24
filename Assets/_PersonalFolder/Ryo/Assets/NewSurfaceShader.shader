Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BlurStrength ("Blur Strength", Range(0, 1)) = 0.5
        _DarkenStrength ("Darken Strength", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Fog { Mode Off }
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _BlurStrength;
            float _DarkenStrength;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // Blur effect
                float blur = smoothstep(0.3, 0.8, dist) * _BlurStrength;
                float2 offset = float2(blur, blur);
                fixed4 col = tex2D(_MainTex, i.uv + offset);

                // Darken effect
                float darken = smoothstep(0.3, 0.8, dist) * _DarkenStrength;
                col.rgb *= (1.0 - darken);

                return col;
            }
            ENDCG
        }
    }
}
