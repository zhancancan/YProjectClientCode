#ifndef TERRAIN_LM3_CG 
#define TERRAIN_LM3_CG
 
	#pragma multi_compile_fog  
	#pragma exclude_renderers d3d11 d3d9 d3d11_9x xboxone ps4 xbox360 ps3 
	
	#include "UnityCG.cginc" 
	#include "TerrainStruct.cginc"
	#include "../../Core/LightSpace.cginc"
	#include "../../Core/fog_tools.cginc"

	// vertex shader
	inline v2f_surf vertLM (appdata_full v) {
		UNITY_SETUP_INSTANCE_ID(v);
		v2f_surf o;
		UNITY_INITIALIZE_OUTPUT(v2f_surf,o); 
		

		vert (v); 
		packSurface(o,v);
		UNITY_TRANSFER_INSTANCE_ID(v,o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
			
		half3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;  
		o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;  

		PURE_TRANSFER_FOG(o,v.vertex); // pass fog coordinates to pixel shader
		return o;
	}


 

	// fragment shader
	inline fixed4 fragLM (v2f_surf IN) : SV_Target {
		UNITY_SETUP_INSTANCE_ID(IN); 
		Input surfIN;
		UNITY_INITIALIZE_OUTPUT(Input,surfIN); 			
		resetInput(surfIN, IN);  

		#ifdef UNITY_COMPILER_HLSL
			SurfaceOutput o = (SurfaceOutput)0;
		#else
			SurfaceOutput o;
		#endif

		surf (surfIN, o);
	
	 	half4 ct=UNITY_SAMPLE_TEX2D(unity_Lightmap, IN.lmap.xy);
		half3 color = ParseLightmap(ct); 
		half4 c ;
		c.rgb= o.Albedo * color;  
		c.a= o.Alpha; 

		PURE_APPLY_FOG(IN.fogCoord, c); 
		return c;
	}  
#endif 