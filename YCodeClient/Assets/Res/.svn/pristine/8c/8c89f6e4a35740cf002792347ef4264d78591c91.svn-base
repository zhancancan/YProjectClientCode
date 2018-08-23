#ifndef TERRAIN_SHADOW_CASTER_CG
#define TERRAIN_SHADOW_CASTER_CG
 
	#pragma multi_compile_fog 
	#pragma multi_compile_shadowcaster 

	#define UNITY_PASS_SHADOWCASTER
	#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
		
	#include "HLSLSupport.cginc"
	#include "UnityShaderVariables.cginc"
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"  

	#define INTERNAL_DATA
	#define WorldReflectionVector(data,normal) data.worldRefl
	#define WorldNormalVector(data,normal) normal
	 


	struct v2f {
		V2F_SHADOW_CASTER; 
		UNITY_VERTEX_INPUT_INSTANCE_ID 
	};
		 
	v2f vertShadowCaster (appdata_full v) {
		UNITY_SETUP_INSTANCE_ID(v);
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f,o);
		UNITY_TRANSFER_INSTANCE_ID(v,o); 
		 
		TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
		return o;
	}
		
	half4 fragShadowCast(v2f i) : SV_Target {
		UNITY_SETUP_INSTANCE_ID(i);		 
		SHADOW_CASTER_FRAGMENT(i)
	} 
#endif 