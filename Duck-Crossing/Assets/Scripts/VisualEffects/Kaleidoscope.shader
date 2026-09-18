Shader "VisualEffects/Kaleidiscope"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define numPoints 5

            uniform float _Amount = 1.0;

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

            sampler2D _MainTex;


            float rand(float2 n) {
                return frac(sin(dot(n.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            struct Ray
            {
                float2 pnt;
                float2 dir;
            };

            float noise(float2 n) {
                const float2 d = float2(0.0, 1.0);
                float2 b = floor(n), f = smoothstep(float2(0.0, 0.0), float2(1.0, 1.0), frac(n));
                return lerp(lerp(rand(b), rand(b + d.yx), f.x), lerp(rand(b + d.xy), rand(b + d.yy), f.x), f.y);
            }

            float2 noise2(float2 n)
            {
                return float2(noise(float2(n.x + 0.2, n.y - 0.6)), noise(float2(n.y + 3., n.x - 4.)));
            }

            Ray GetRay(float i)
            {
                float2 position = noise2(float2(i * 6.12 + _Time.y * 0.1, i * 4.43 + _Time.y * 0.1));
                Ray r;
                r.pnt = position;
                r.dir = normalize(noise2(float2(i * 7. + _Time.y * 0.05, i * 6.)) * 2.0 - 1.0);
                return r;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float2 curPos = i.uv;
                float2 uv = i.uv;

                for (int i = 0; i < numPoints; i++)
                {
                    Ray ray = GetRay(float(i + 1) * 3.);
                    float offset = dot(curPos - ray.pnt, ray.dir);
                    if (offset < 0.) curPos -= ray.dir * offset * 2.0;
                }

                fixed4 col = tex2D(_MainTex, lerp(uv, frac(curPos), _Amount));
                return col;
            }
            ENDCG
        }
    }
}
