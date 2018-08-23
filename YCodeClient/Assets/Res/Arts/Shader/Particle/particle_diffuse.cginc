#ifndef PATICAL_DIFFUSE_CG 
#define PATICAL_DIFFUSE_CG
   
	#pragma vertex vert
	#pragma fragment frag
	#pragma target 2.0
	#pragma multi_compile_particles
	#pragma multi_compile_fog

	#include "UnityCG.cginc"
	#include "../Core/particle_blend_mode.cginc"
	#include "../Core/fog_tools.cginc"
	#include "../Core/math_tools.cginc"

		sampler2D _MainTex;
		fixed4 _TintColor;
			
		struct appdata_t {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
			UNITY_FOG_COORDS(1)
			//#ifdef SOFTPARTICLES_ON
			//float4 projPos : TEXCOORD2;
			//#endif 
		};
			
		float4 _MainTex_ST;

		v2f vert (appdata_t v){
			v2f o;
			UNITY_SETUP_INSTANCE_ID(v); 
			o.vertex = UnityObjectToClipPos(v.vertex);
			//#ifdef SOFTPARTICLES_ON
			//	o.projPos = ComputeScreenPos (o.vertex);
			//	COMPUTE_EYEDEPTH(o.projPos.z);
			//#endif
			o.color = pvertColor(v.color, _TintColor);
			o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
			#if SEQUENCE_ON
				o.texcoord = calcSequenceUV(o.texcoord);
			#endif
			PURE_TRANSFER_FOG(o,v.vertex);
			return o;
		}

		//sampler2D_float _CameraDepthTexture;
		//float _InvFade;
			
		fixed4 frag (v2f i) : SV_Target{
			//#ifdef SOFTPARTICLES_ON
			//float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
			//float partZ = i.projPos.z;
			//float fade = saturate (_InvFade * (sceneZ-partZ));
			//i.color.a *= fade;
			//#endif 		
			 
			fixed4 tex=tex2D(_MainTex,i.texcoord);
			fixed4 col= pfragColor(tex,i.color,_TintColor); 
 	 
			PURE_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0,0,0,0)); // fog towards black due to our blend mode
			return col;
		}
  

#endif 