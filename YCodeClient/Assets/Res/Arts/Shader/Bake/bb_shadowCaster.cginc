#ifndef BAKE_SHADOW_CASTER_CG
#define BAKE_SHADOW_CASTER_CG
 
	#pragma multi_compile_fog 
	#pragma multi_compile_shadowcaster 

	#define UNITY_PASS_SHADOWCASTER
	#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
		 
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"   
		 
		 
	#if CUT_OFF			
		sampler2D _MainTex;
		half4 _MainTex_ST; 
		half4 _Color;
		half _cutThreshhold;
	#endif

	struct v2f {		
		#if CUT_OFF 
		half2 uv:TEXCOORD1;
		#endif
		V2F_SHADOW_CASTER;
		//UNITY_VERTEX_INPUT_INSTANCE_ID 
	};
	 
	v2f vert (appdata_full v) {
		//UNITY_SETUP_INSTANCE_ID(v);
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f,o); 
		#if CUT_OFF 			 
			o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
		#endif
			 
		TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
		return o;
	};  

	half4 frag(v2f IN) : SV_Target { 
		#if CUT_OFF
			half4 tex = tex2D(_MainTex, IN.uv.xy); 
			clip(tex.a*_Color.a-_cutThreshhold);
		#endif
		SHADOW_CASTER_FRAGMENT(IN)
	}  
  
 #endif 