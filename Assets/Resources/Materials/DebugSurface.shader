Shader "Custom/DebugSurface"

{
    Properties
    {
        _Primary ("Primary Colour", Color) = (0, 0, 0, 1)
        _Secondary ("Secondary Colour", Color) = (1, 1, 1, 1)
        _LineSize ("Line Size", float) = 0.1
    }
    SubShader {
        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0
        
         struct Input {
            float2 uv;
            float3 worldPos;
         };

        fixed4 _Primary;
        fixed4 _Secondary;
        float _LineSize;

         bool compare (float a, float b, float c)
         {
            return a > b-c && a < b+c;
         }
 
         void surf (Input IN, inout SurfaceOutput o)
         {
            float3 pos = frac(IN.worldPos);

            
            fixed4 c = _Primary;

            if (compare(pos[0], 0, _LineSize))
                {
                c = max(c, _Secondary);
                }
            if (compare(pos[2], 0, _LineSize))
                {
                c = max(c, _Secondary);
                }

            o.Albedo = c;

        }
        ENDCG  
      
        }
    FallBack "Diffuse"
}
