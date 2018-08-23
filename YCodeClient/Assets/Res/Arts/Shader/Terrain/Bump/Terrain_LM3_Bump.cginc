#ifndef TERRAIN_LM3_BUMP_CG 
#define TERRAIN_LM3_BUMP_CG
 
	#define UNITY_PASS_META
	#pragma multi_compile_fog  
	#pragma multi_compile_fwdbase 
	#pragma exclude_renderers d3d11 d3d9 d3d11_9x xboxone ps4 xbox360 ps3 
	
	#include "UnityCG.cginc" 
	#include "TerrainStruct_Bump.cginc"
	#include "../../Core/LightSpace.cginc"
	#include "../../Core/fog_tools.cginc"

  
	half _BumpPower;

	// vertex shader
	inline v2f_surf vertLM (appdata_full v) { 
		v2f_surf o;
		UNITY_INITIALIZE_OUTPUT(v2f_surf,o);

		vert (v); 
		packSurface(o,v); 
			
		half3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;  
		o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;  
			 
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
		half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
		o.tSpace0 = half4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
		o.tSpace1 = half4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
		o.tSpace2 = half4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
			 
			 
		o.lmap.z =dot(worldNormal,specularLightDir.xyz); 

		PURE_TRANSFER_FOG(o,v.vertex); // pass fog coordinates to pixel shader
		return o;
	}


 

	// fragment shader
	inline half4 fragLM (v2f_surf IN) : SV_Target { 
		Input surfIN;
		UNITY_INITIALIZE_OUTPUT(Input,surfIN); 			
		resetInput(surfIN, IN);  
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
		o.Normal = 0;

		surf (surfIN, o); 
				 
		half3 worldN;
		worldN.x = dot(IN.tSpace0.xyz, o.Normal);
		worldN.y = dot(IN.tSpace1.xyz, o.Normal);
		worldN.z = dot(IN.tSpace2.xyz, o.Normal);
		o.Normal = worldN;
			
		half3 worldPos = half3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);
		half3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
	 
		BlinnphoneData d ; 
		d.albedo = o.Albedo;
		d.alpha = o.Alpha; 
		d.lightDir = specularLightDir; 
		d.lightmapUV = IN.lmap.xy; 
		d.dotVNrm = IN.lmap.z;
		d.nrm = worldN;
		d.bumpPower = _BumpPower; 
		d.viewDir = viewDir; 
		d.specular = o.Specular; 
		d.gloss = o.Gloss; 
		d.specColor = _SpecColor; 
		half4 c = samplerLightmap_blinnphone(d); 
		PURE_APPLY_FOG(IN.fogCoord, c);    
		UNITY_OPAQUE_ALPHA(c.a);
		return c; 
	}  

 

#endif 