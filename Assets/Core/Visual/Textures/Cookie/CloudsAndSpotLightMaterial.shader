Shader "CustomRenderTexture/Light Cookie Shader"
{
    Properties
    {
        _Color1 ("Color1", Color) = (1,1,1,1)
        _SpotTex("SpotTex", 2D) = "white" {}
        _Color2 ("Color2", Color) = (1,1,1,1)
        _CloudTex("CloudTex", 2D) = "white" {}
     }

     SubShader
     {
        Blend One Zero

        Pass
        {
            Name "Light Cookie Shader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color1;
            sampler2D   _SpotTex;
            float4      _SpotTex_ST;
            float4      _Color2;
            sampler2D   _CloudTex;
            float4      _CloudTex_ST;

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv1 = IN.globalTexcoord.xy * _SpotTex_ST.xy + _SpotTex_ST.zw;
                float2 uv2 = IN.globalTexcoord.xy * _CloudTex_ST.xy + _CloudTex_ST.zw;
                float4 color = (tex2D(_SpotTex, uv1) * _Color1) + (tex2D(_CloudTex, uv2) * _Color2);

                return color;
            }
            ENDCG
        }
    }
}
