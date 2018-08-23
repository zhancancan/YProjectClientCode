Shader "Pure/Ray/BlinkingBillboardRay"{
	Properties{
		_MainTex ("Base Texture", 2D) = "white" {}
		_Color ("Color",Color) = (1,1,1,1)
		_FadeOutDistNear("Near fadeout dist",float)=10
		_FadeOutDistFar("Far fadeout dist",float)=10000
		_Multiplier ("Multiplier",float )=1  
		_BlinklingTimeOffsScale("Time Offset Scale",float)=5
		_TimeOnDuration("Time On Duration",float)=0.5
		_TimeOffDuration("Time Off Duration",float)=0.5
		_NoiseAmount("Noise amount(where zero,pulse wave is used)",Range(0,0.5))=0
		_SizeGrowStartDist("Size Glow Start Dist",float)=5
		_SizeGrowEndDist("Size Glow End Dist",float)=50
		_MaxGrowSize("Max Glow Size",float)=2.5
		_Bias("Bias",float)=0
		_VerticalBillboarding("Vertical billboarding factor",Range(0,1))=1
		 _ViewerOffset("Viewer offset", float) = 0  

	}
	SubShader{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType" = "Transparent" }
		LOD 100
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) } 
		ZTest Less
		Blend One One
		Pass{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag	
			#pragma fragmentoption ARB_precision_hint_fastest 	
			#include "UnityCG.cginc"

			struct v2f{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0; 
				fixed4 color :TEXCOORD1;
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
			float _MaxGrowSize;
			float _Bias;
			float _VerticalBillboarding;
			float _ViewerOffset;
			float4 _Color;
		




			void calcOrthoNormalBasis(float3 dir,out float3 right,out float3 up){ 
				up = abs(dir.y)>0.999f ? float3(0,0,1):float3(0,1,0);
				right = normalize(cross(up,dir));
				up =cross(dir,right);
			}

			float calcFadeOutFactor(float dist){ 
				float n = saturate(dist/_FadeOutDistNear);
				float f = 1-saturate(max(dist- _FadeOutDistFar,0)*0.2);
				f*=f;
				n*=n;
				n*=n;
				n*=f;
				return n;
			} 
			float calcDistScale(float dist){  
				float distScale   = min(max(dist - _SizeGrowStartDist,0) / _SizeGrowEndDist,1);            
				return distScale * distScale * _MaxGrowSize;  
			}

			
			v2f vert (appdata_full v){
				v2f o;



				#if 0 
					float3 centerOffs = float3(float(0.5).xx- v.color.rg,0)*v.texcoord1.xyy;
					float3 bbcenter= v.vertex
					float viewPos= mul(UNITY_MATRIX_MV,float4(bbcenter,1))-centerOffs
				#else 
					float3 centerOffs = 0;//float3(0.5 - v.color.rg,0)* v.texcoord1.xyy;
					float3 centerLocal = v.vertex.xyz + centerOffs.xyz;
					float3 viewLocal = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1));
					float3 localDir = viewLocal - centerLocal;
					localDir[1] = lerp(0, localDir[1],_VerticalBillboarding);
					float localDirLength = length(localDir);
					float3 rightLocal;
					float3 upLocal;
					calcOrthoNormalBasis(localDir/localDirLength, rightLocal,upLocal); 
					float distScale = calcDistScale(localDirLength)*v.color.a;
					float3 bbnrm = rightLocal* v.normal.x +upLocal * v.normal.y;
					float3 bbLocalPos = centerLocal - (rightLocal * centerOffs.x + upLocal * centerOffs.y) + bbnrm* distScale;
					bbLocalPos += _ViewerOffset* localDir;
				#endif

 

				float time = _Time.y + _BlinklingTimeOffsScale*v.color.b;  			
				float fracTime =fmod(time,_TimeOnDuration + _TimeOffDuration) ;
				float wave = smoothstep(0,_TimeOnDuration *0.25,fracTime)*(1-smoothstep(_TimeOnDuration*0.75,_TimeOnDuration,fracTime));
				float noiseTime =time * (6.2831853f/_TimeOnDuration);
				float noise =sin(noiseTime) * (0.5f * cos(noiseTime *0.6366f  + 56.7272f)+0.5f); 
				float noiseWave = _NoiseAmount * noise + (1-_NoiseAmount);				
				wave = _NoiseAmount<0.01f?wave:noiseWave;
				wave += _Bias;
				     
				o.uv= v.texcoord.xy;
				o.pos = mul(UNITY_MATRIX_MVP,float4(bbLocalPos,1));
				o.color = calcFadeOutFactor(localDirLength) * _Color * _Multiplier * wave;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR{
				return tex2D(_MainTex,i.uv.xy) * i.color;
			}

			ENDCG
		}
	}
}
 