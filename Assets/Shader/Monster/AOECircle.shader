Shader "Custom/AOE_PulseFromCenter"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color Tint", Color) = (1, 0, 0, 1)
        _Progress ("Progress", Range(0, 1)) = 0.0
        _GlowStrength ("Glow Strength", Range(0, 5)) = 1.0
        _Width ("Ring Width", Range(0.001, 1)) = 0.1
        _MinAlpha ("MinAlpha", Range(0, 1)) = 0.2
        _MaxAlpha ("MaxAlpha", Range(0, 1)) = 0.6
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            float _Progress;
            float _GlowStrength;
            float _Width;
            float _MinAlpha;
            float _MaxAlpha;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);
            
                float ringStart = _Progress - _Width * 0.5;
                float ringEnd = _Progress + _Width * 0.5;
            
                float fade = smoothstep(ringStart, ringEnd, dist); // 중심부터 fade
            
                fixed4 texColor = tex2D(_MainTex, i.uv);
            
                // 알파가 0인 픽셀은 완전 무시 (투명 영역 유지)
                if (texColor.a < 0.01) discard;
            
                fixed4 col;
                col.rgb = texColor.rgb;
                col.a = texColor.a * (1.0 - fade); // 중심부터 사라짐
                col.a = clamp(col.a,_MinAlpha,_MaxAlpha);
                return col;
            }
            ENDCG
        }
    }
}
