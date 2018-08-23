Shader "Pure/Ray/Blinking"{
	Properties{
		_MainTex ("Base Texture", 2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
		_FadeOutDistNear("Near fadeout dist",float)=10
		_FadeOutDistFar("Far fadeout dist",float)=10000
		_Multiplier ("Multiplier",float )=1  
		_BlinklingTimeOffsScale("Time Offset Scale",float)=5
		_TimeOnDuration("Time On Duration",float)=0.5
		_TimeOffDuration("Time Off Duration",float)=0.5
		_NoiseAmount("Noise amount",Range(0,0.5))=0
		_SizeGrowStartDist("Size Glow Start Dist",float)=0
		_SizeGrowEndDist("Size Glow End Dist",float)=10
		_MaxGlowSize("Max Glow Size",float)=2.5
		_Bias("Bias",float)=0
	}
	SubShader{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType" = "Transparent" }
		LOD 100
		Blend One One
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }  
		Pass{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			struct v2f{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0; 
				fixed4 color :TEXCOORD1;
			};
			struct app{ 
				float4 vertex:POSITION;
				float2 uv:TEXCOORD0;
				fixed4 color :COLOR;
				float3 normal:NORMAL;
			};
		 
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _FadeOutDistNear;
			float _FadeOutDistFar; 
			float _Multiplier;
			half _BlinklingTimeOffsScale;
			float _TimeOnDuration;
			float _TimeOffDuration;
			float _NoiseAmount;
			float _SizeGrowStartDist;
			float _SizeGrowEndDist;
			float _MaxGlowSize;
			float _Bias;
			float4 _Color;

			
			v2f vert (app v){
				v2f o;
				float time = _Time.y + _BlinklingTimeOffsScale*v.color.b;
				float3 viewPos	= mul(UNITY_MATRIX_MV,v.vertex);
				float dist		= length(viewPos);
			
				float fracTime =fmod(time,_TimeOnDuration + _TimeOffDuration) ;
				float wave = smoothstep(0,_TimeOnDuration *0.25,fracTime)*(1-smoothstep(_TimeOnDuration*0.75,_TimeOnDuration,fracTime));
				float noiseTime =time * (6.2831853f/_TimeOnDuration);
				float noise =sin(noiseTime) * (0.5f * cos(noiseTime *0.6366f  + 56.7272f)+0.5f); 
				float noiseWave = _NoiseAmount * noise + (1-_NoiseAmount);				
				wave = _NoiseAmount<0.01f?wave:noiseWave;
				wave += _Bias;

				float distScale = min(max(dist - _SizeGrowStartDist,0)/_SizeGrowEndDist,1);

				distScale = distScale * distScale * _MaxGlowSize * v.color.a;
				

				float n			= saturate(dist/_FadeOutDistNear);
				float f			= 1 - saturate(max(dist-_FadeOutDistFar,0)*0.2);
				f*=f; 
				n*=n;
				n*=n;
				n*=f;

				float4 mdlpos =v.vertex;  
				mdlpos.xyz += distScale * v.normal;
				 
				o.uv= v.uv.xy;
				o.pos = mul(UNITY_MATRIX_MVP,mdlpos);
				o.color = n * _Color *_Multiplier*wave;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target{
				return tex2D(_MainTex,i.uv.xy) * i.color;
			}

			ENDCG
		}
	}
}
 