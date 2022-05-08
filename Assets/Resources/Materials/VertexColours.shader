Shader "Custom/VertexColours"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200
            Pass {Cull Off}

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            // Lambert is a shading type. Vertex sets up for vertex colour
            #pragma surface surf Lambert vertex:vert

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input {
                float4 vertColor; // Make it a colour (r, g, b a) vector
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;


            // If we needed instancing support, it would go under this line. Instancing is good for drawing one thing many times

            void vert(inout appdata_full v, out Input o) // Vertex ShaderB
            {
                UNITY_INITIALIZE_OUTPUT(Input, o);
                o.vertColor = v.color;
            }

            void surf(Input IN, inout SurfaceOutput o) // Fragment Shader
            {
                o.Albedo = IN.vertColor.rgb;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
