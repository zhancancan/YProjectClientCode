#ifndef FOG_TOOLS_CG 
#define FOG_TOOLS_CG

#include "UnityCG.cginc"
  
	float2 pure_FogDistance; 
	float3 pure_FogCenter;  
	
	#define PURE_CALC_FOG_FACTOR(outpos) \
		 half4 wp= mul(unity_ObjectToWorld, v.vertex);half z =length(pure_FogCenter.xyz- wp.xyz); \
		 half fogFactor=max(0,1-(z-pure_FogDistance.x)/(pure_FogDistance.y-pure_FogDistance.x)); 
		 
		  



    #if defined(FOG_LINEAR) || defined (FOG_EXP) || defined (FOG_EXP2)		 
		 #if defined(FOG_Y_AXIS)
			#define PURE_TRANSFER_FOG(o, outpos) PURE_CALC_FOG_FACTOR(outpos); o.fogCoord.x =fogFactor ;           
		 #else 
			#define PURE_TRANSFER_FOG(o, outpos) \
			outpos = UnityObjectToClipPos(v.vertex);\
			UNITY_CALC_FOG_FACTOR((outpos).z); \
			o.fogCoord.x = unityFogFactor
		 #endif
	#else 		
		#define PURE_TRANSFER_FOG(o,outpos)
	#endif

	#if defined(FOG_LINEAR) || defined (FOG_EXP) || defined (FOG_EXP2)		
		#define PURE_APPLY_FOG_COLOR(coords,col,fogCol)UNITY_FOG_LERP_COLOR(col,fogCol,(coords).x)
	#else 
		#define PURE_APPLY_FOG_COLOR(coord,col,fogCol)
	#endif
	
 
	#ifdef UNITY_PASS_FORWARDADD
		#define PURE_APPLY_FOG(coord, col) PURE_APPLY_FOG_COLOR(coord,col,fixed4(0,0,0,0))
	#else 
		#define PURE_APPLY_FOG(coord, col) PURE_APPLY_FOG_COLOR(coord,col, unity_FogColor)
	#endif


#endif 