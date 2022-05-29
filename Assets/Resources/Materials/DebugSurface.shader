Shader "Custom/DebugSurface"
{
    Properties
    {
        _Primary ("Primary Colour", Color) = (0, 0, 0, 1)
        _Secondary ("Secondary Colour", Color) = (1, 1, 1, 1)
        _LineSize ("Line Size", float) = 0.1
    }
    SubShader {
                
      Pass {
        CGPROGRAM

        fixed4 _Primary;
        fixed4 _Secondary;
        float _LineSize;

         #pragma vertex vert  
         #pragma fragment frag 

         struct vertexInput {
            float4 vertex : POSITION;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 position_in_world_space : TEXCOORD0;
         };



         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output; 
 
            output.pos =  UnityObjectToClipPos(input.vertex);
            output.position_in_world_space = 
               mul(unity_ObjectToWorld, input.vertex);
               // transformation of input.vertex from object 
               // coordinates to world coordinates;
            return output;
         }

         bool compare(float a, float b, float c)
         {
            return a > b-c && a < b+c;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
            float3 pos = frac(input.position_in_world_space);
            
            fixed4 c = _Primary;

            if (compare(pos.x, 0, _LineSize))
                {
                c = max(c, _Secondary);
                }
            if (compare(pos.z, 0, _LineSize))
                {
                c = max(c, _Secondary);
                }

            return c;

         }
 
         ENDCG  
      }
   }
    FallBack "Diffuse"
}
