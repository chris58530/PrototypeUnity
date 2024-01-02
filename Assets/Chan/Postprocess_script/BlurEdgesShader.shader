Shader "Hidden/Custom/BlurEdgesShader"
{
    SubShader
    {
        Pass
        {
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            sampler2D _MainTex;
            float _Strength;

           fixed4 frag(v2f i) : SV_Target
{
    float2 center = float2(0.5, 0.5); // 中心點為螢幕中心

    float2 delta = i.uv - center;
    float distance = length(delta);
    float blur = smoothstep(0, _Strength, distance);

    fixed4 col = tex2D(_MainTex, i.uv) * blur + tex2D(_MainTex, i.uv) * (1 - blur);

    return col;
}
            ENDCG
        }
    }
}
