Shader "Custom/Protanopia" {
  Properties {
    _Intensity ("Intensity", Range(0,1)) = 1.0
  }

  SubShader {
    Tags {
      "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline"
    }

    Pass {
      Name "ProtanopiaSim"
      ZTest Always ZWrite Off Cull Off

      HLSLPROGRAM
      #pragma vertex Vert
      #pragma fragment Frag
      #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

      TEXTURE2D(_BlitTexture);
      SAMPLER(sampler_BlitTexture);
      float _Intensity;
      
      static const float3x3 protanopiaMatrix = float3x3(
        0.56667, 0.43333, 0.00000,
        0.55860, 0.44140, 0.00000,
        0.00000, 0.24100, 0.75900
      );

      struct Attributes
      {
        float4 positionOS : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct Varyings
      {
        float4 positionCS : SV_POSITION;
        float2 uv : TEXCOORD0;
      };

      Varyings Vert(Attributes input)
      {
        Varyings output;
        output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
        output.uv = input.uv;
        return output;
      }

      half4 Frag(Varyings input) : SV_Target
      {
        float3 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, input.uv).rgb;
        float3 simulated = mul(protanopiaMatrix, color);
        float3 finalColor = lerp(color, simulated, _Intensity);

        return half4(finalColor, 1.0);
      }
      ENDHLSL
    }
  }
}