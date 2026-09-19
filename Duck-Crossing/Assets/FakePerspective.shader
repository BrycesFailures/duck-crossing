// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/FakePerspective" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
        _BeginSize("Foreground Scale", float) = 1.0
        _EndSize("Background Scale", float) = 0.0
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            LOD 100

            Pass {
                CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #pragma target 2.0
                    #pragma multi_compile_fog

                    #include "UnityCG.cginc"

                    struct appdata_t {
                        float4 vertex : POSITION;
                        float2 texcoord : TEXCOORD0;
                        float2 btexcoord : TEXCOORD1;
                        UNITY_VERTEX_INPUT_INSTANCE_ID
                    };

                    struct v2f {
                        float4 vertex : SV_POSITION;
                        float2 texcoord : TEXCOORD0;
                        float2 btexcoord : TEXCOORD1;
                        UNITY_FOG_COORDS(1)
                        UNITY_VERTEX_OUTPUT_STEREO
                    };

                    float4 _Color;
                    sampler2D _MainTex;
                    float4 _MainTex_ST;
                    float _BeginSize;
                    float _EndSize;

                    v2f vert(appdata_t v)
                    {
                        v2f o;
                        UNITY_SETUP_INSTANCE_ID(v);
                        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                        o.vertex = UnityObjectToClipPos(v.vertex);
                        o.btexcoord = v.texcoord;
                        o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                        UNITY_TRANSFER_FOG(o,o.vertex);
                        return o;
                    }

                    float map(float value, float inMin, float inMax, float outMin, float outMax) {
                        return outMin + (value - inMin) * (outMax - outMin) / (inMax - inMin);
                    }

                    fixed4 frag(v2f i) : SV_Target
                    {
                        i.texcoord.x = (i.texcoord.x * 2.0 - 1.0) * lerp(_EndSize, _BeginSize, i.vertex.z) * 0.5 + 0.5;
                        fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
                        //col.a *= 1.0 - pow(i.btexcoord.y, 5.0);
                        //col.a *= pow(1.0 - abs(i.btexcoord.x * 2.0 - 1.0), 1.0);
                        col.a *= pow(map(abs(i.btexcoord.x * 2.0 - 1.0), 0.3, 1.0, 1.0, 0.0), 2.0);
                        UNITY_APPLY_FOG(i.fogCoord, col);
                        return col;
                    }
                ENDCG
            }
    }

}