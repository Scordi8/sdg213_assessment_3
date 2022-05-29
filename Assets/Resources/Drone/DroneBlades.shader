Shader "Custom/DroneBladesShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MSRTex ("MSR (RGB)", 2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _angle ("Spin speed", float) = 0.0
        _offsetY ("Y Offset", float) = 0.0
        _offsetX ("X Offset", float) = 0.0
    }
    SubShader
    {
        Cull Off
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow





        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _MSRTex;
        float _angle;
        float _offsetY;
        float _offsetX;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v)
        {
        //v.vertex.z = v.vertex.z + 0.001;
        float3 pos = v.vertex.xyz;
        
        v.vertex.x = (pos.x * cos(_angle*_Time)) - (pos.y * sin(_angle*_Time)) + _offsetX - (sin(_angle*_Time) * -0.0001);
        v.vertex.y = (pos.x * sin(_angle*_Time)) + (pos.y * cos(_angle*_Time)) + _offsetY + (cos(_angle*_Time) * -0.0001);

        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            fixed4 _msr = tex2D (_MSRTex, IN.uv_MainTex);
            o.Metallic = _msr.r;
            o.Smoothness = 1.0 - _msr.b;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
