#ifndef TERRAIN_FORWARD_ADD_CG 
#define TERRAIN_FORWARD_ADD_CG



 
 
	#pragma multi_compile_fog 
	#pragma multi_compile_fwdadd

	#define UNITY_PASS_FORWARDADD

	#include "HLSLSupport.cginc"
	#include "UnityShaderVariables.cginc"
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"
	#include "TerrainStruct.cginc"
	#include "../../Core/fog_tools.cginc"

	#define INTERNAL_DATA
	#define WorldReflectionVector(data,normal) data.worldRefl
	#define WorldNormalVector(data,normal) normal
	 
 

	
	inline v2f_surf vertFwdAdd (appdata_full v) {
		v2f_surf o; 	
		UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
		packSurface(o,v);

		vert (v);

		half3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		o.worldPos = worldPos;
		o.worldNormal = worldNormal;

		TRANSFER_SHADOW(o); 
		PURE_TRANSFER_FOG(o,v.vertex); 
		return o;
	}

	

	// fragment shader
	inline fixed4 fragFwdAdd (v2f_surf IN) : SV_Target { 
		Input surfIN;
		UNITY_INITIALIZE_OUTPUT(Input,surfIN);
		resetInput(surfIN, IN);

		half3 worldPos = IN.worldPos;
		#ifndef USING_DIRECTIONAL_LIGHT
			half3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
		#else
			half3 lightDir = _WorldSpaceLightPos0.xyz;
		#endif
		#ifdef UNITY_COMPILER_HLSL
			SurfaceOutput o = (SurfaceOutput)0;
		#else
			SurfaceOutput o;
		#endif
		o.Albedo = 0.0;
		o.Emission = 0.0;
		o.Specular = 0.0;
		o.Alpha = 0.0;
		o.Gloss = 0.0; 
		o.Normal = IN.worldNormal; 

		// call surface function
		surf (surfIN, o);
		UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
		half4 c = 0;

		// Setup lighting environment
		UnityGI gi;
		UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
		gi.indirect.diffuse = 0;
		gi.indirect.specular = 0;
		#if !defined(LIGHTMAP_ON)
			gi.light.color = _LightColor0.rgb;
			gi.light.dir = lightDir;
		#endif
		gi.light.color *= atten;
		c += LightingLambert (o, gi);
		c.a = 0.0;
		PURE_APPLY_FOG(IN.fogCoord, c); // apply fog
		UNITY_OPAQUE_ALPHA(c.a);
		return c;
	} 

	

#endif 