Shader "Custom/Fan_AOE_Expand"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,0,0,1)
        _Progress ("Progress", Range(0,1)) = 0
        _AlphaLimit ("Alpha Limit", Range(0,1)) = 1
        _MinAlpha ("MinAlpha", Range(0, 1)) = 0.2
        _MaxAlpha ("MaxAlpha", Range(0, 1)) = 0.6
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
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
            float _AlphaLimit;
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
                float2 worldUV : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // 좌표를 (0.5,0.5) 기준으로 바꾸기
                o.worldUV = v.uv - 0.5;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 origin = float2(0.0, 0.0);  // 꼭짓점 위치
                float dist = length(i.uv - origin); // 꼭짓점에서 거리
                float fade = smoothstep(_Progress, _Progress - 0.1, dist); // 퍼짐 정도
            
                fixed4 tex = tex2D(_MainTex, i.uv);
            
                // 텍스처 알파가 0인 경우는 아예 출력도 0으로
                if (tex.a < 0.01)
                    discard;
            
                tex.rgb = _Color.rgb;              // 붉은색으로 덮기
                tex.a *= fade * _AlphaLimit;       // 기존 알파에 fade 곱해서 부드럽게 사라지게
                tex.a = clamp(tex.a, _MinAlpha, _MaxAlpha);
                return tex;
            }
            ENDCG
        }
    }
}
