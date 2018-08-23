Shader "Pure/Ray/GodRays"{
	Properties{
		_MainTex ("Base Texture", 2D) = "white" {}
		_FadeOutDistNear("Near fadeout dist",float)=10
		_FadeOutDistFar("Far fadeout dist",float)=10000
		_Mutilplier ("Multiplier",float )=1 
		_ConstractionAmount("Near contraction amount",float)=5 
	}
	SubShader{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType" = "Transparent" }
		LOD 100
		Blend One One
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
			float _ConstractionAmount;
			float _Mutilplier;
			
			v2f vert (app v){
				v2f o;
				float3 viewPos	= mul(UNITY_MATRIX_MV,v.vertex);
				float dist		= length(viewPos);
				float n			= saturate(dist/_FadeOutDistNear);
				float f			= 1 - saturate(max(dist-_FadeOutDistFar,0)*0.2);
				f*=f; 
				n*=n;
				n*=n;
				n*=f;

				float4 vpos =v.vertex; 
				vpos.xyz -= v.normal * saturate(1-n)*v.color.a *_ConstractionAmount;
				o.uv= v.uv.xy;
				o.pos = mul(UNITY_MATRIX_MVP,vpos);
				o.color = n * v.color*_Mutilplier;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target{
				return tex2D(_MainTex,i.uv.xy) * i.color;
			}

			ENDCG
		}
	}
}
 