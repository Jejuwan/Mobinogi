Shader "UI/SlidingHighlightFade"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Base Color", Color) = (0,0,0,1)
        _Speed ("Scroll Speed", Float) = 1
        _Width ("Line Width", Float) = 0.05
        _FadeTime ("Fade Duration", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            float _Speed;
            float _Width;
            float _FadeTime;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float smoothPulse(float x, float center, float width)
            {
                return smoothstep(center - width, center, x) * (1 - smoothstep(center, center + width, x));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float lineX = fmod(_Time.y * _Speed, 1.0); // �ݺ��ϴ� ��� �� ��ġ
                float fade = smoothPulse(i.uv.x, lineX, _Width); // ���� ����

                float trail = exp(-abs(i.uv.x - lineX) * (5.0 / _FadeTime)); // ���� ������ �ڸ��� ���� ����

                float brightness = max(fade, trail); // �� + �ܻ�

                fixed4 col = _Color; // �⺻�� ������
                col.rgb += brightness; // ��� �߰� (�������)
                col.a = brightness; // ���Ĵ� ��� ���

                return col;
            }
            ENDCG
        }
    }
}
