#ifndef TERRAIN_FORWARD_BASE_BUMP_CG 
#define TERRAIN_FORWARD_BASE_BUMP_CG


 
 
				 
		#pragma multi_compile_fog 
		#pragma multi_compile_fwdbase 

		#define UNITY_PASS_FORWARDBASE

		#include "HLSLSupport.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "AutoLight.cginc"
		#include "TerrainStruct_Bump.cginc"
		#include "../../Core/LightSpace.cginc"
		#include "../../Core/fog_tools.cginc"

		#define INTERNAL_DATA
		#define WorldReflectionVector(data,normal) data.worldRefl
		#define WorldNormalVector(data,normal) normal
	  
		half _BumpPower;

		// vertex shader
		inline v2f_surf vertFwdBase (appdata_full v) {  
			v2f_surf o;
			UNITY_INITIALIZE_OUTPUT(v2f_surf,o); 
			 

			vert (v); 
			packSurface(o,v);
			half3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			half3 worldNormal = UnityObjectToWorldNormal(v.normal);
			half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
			half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
			half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
			o.tSpace0 = half4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
			o.tSpace1 = half4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
			o.tSpace2 = half4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
			 

			#ifdef DYNAMICLIGHTMAP_ON
				o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
			#endif
			#ifdef LIGHTMAP_ON
				o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				o.lmap.z = dot(worldNormal,specularLightDir.xyz);
			#endif

			// SH/ambient and vertex lights
			#ifndef LIGHTMAP_ON
				#if UNITY_SHOULD_SAMPLE_SH
					o.sh = 0;
					// Approximated illumination from non-important point lights
					#ifdef VERTEXLIGHT_ON  
						o.sh += Shade4PointLights ( unity_4LightPosX0, 
													unity_4LightPosY0, 
													unity_4LightPosZ0,
													unity_LightColor[0].rgb, 
													unity_LightColor[1].rgb, 
													unity_LightColor[2].rgb, 
													unity_LightColor[3].rgb,
													unity_4LightAtten0, worldPos, worldNormal);
					#endif
					o.sh = ShadeSHPerVertex (worldNormal, o.sh);
				#endif
			#endif // !LIGHTMAP_ON 
			TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
			PURE_TRANSFER_FOG(o,v.vertex); // pass fog coordinates to pixel shader
			return o;
		}

 




			// withiout lightmap;
		inline half4 CacuLightedColor(SurfaceOutput o , half3 viewDir ,UnityGIInput data){
			UnityLight light=data.light; 
			light.color*=data.atten;
		 
			half4 c; 

			half diff = max (0, dot (o.Normal, light.dir));
			
			half3 sd =normalize(specularLightDir.xyz); 
			half3 h = normalize (sd + viewDir);
			float nh = max (0, dot (o.Normal, h));
			float spec = pow (nh, o.Specular*128.0) * o.Gloss;

 
			c.rgb = o.Albedo * light.color * diff +  data.atten*_SpecColor.rgb * spec;
			c.a = o.Alpha;
			 

			half3 diffuse=0;
			#if UNITY_SHOULD_SAMPLE_SH
				diffuse = ShadeSHPerPixel (o.Normal, data.ambient, data.worldPos);
			#endif

				 
			#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
				c.rgb += o.Albedo * diffuse;
			#endif
			return c; 
		}


 
  

		// fragment shader, use blinnphone light mode;
		inline half4 fragFwdBase (v2f_surf IN) : SV_Target { 
			// prepare and unpack data
			Input surfIN;
			UNITY_INITIALIZE_OUTPUT(Input,surfIN); 				
			resetInput(surfIN, IN);

			half3 worldPos = half3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);

			#ifndef USING_DIRECTIONAL_LIGHT
				half3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
			#else
				half3 lightDir = _WorldSpaceLightPos0.xyz;
			#endif

			half3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
			
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

			 

			// call surface function
			surf (surfIN, o);

			// compute lighting & shadowing factor
			UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)

			 

			half3 worldN;
			worldN.x = dot(IN.tSpace0.xyz, o.Normal);
			worldN.y = dot(IN.tSpace1.xyz, o.Normal);
			worldN.z = dot(IN.tSpace2.xyz, o.Normal);
			o.Normal = worldN;

			// Setup lighting environment
			UnityGI gi;
			UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
			gi.indirect.diffuse = 0;
			gi.indirect.specular = 0;

			#if !defined(LIGHTMAP_ON)
				gi.light.color = _LightColor0.rgb;
				gi.light.dir = lightDir;
			#endif

			// Call GI (lightmaps/SH/reflections) lighting function
			UnityGIInput giInput;
			UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
			giInput.light = gi.light;
			giInput.worldPos = worldPos;
			giInput.worldViewDir = viewDir;
			giInput.atten = atten;
			#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
				giInput.lightmapUV = IN.lmap;
			#else
				giInput.lightmapUV = 0.0;
			#endif
			#if UNITY_SHOULD_SAMPLE_SH
				giInput.ambient = IN.sh;
			#else
				giInput.ambient.rgb = 0.0;
			#endif
			giInput.probeHDR[0] = unity_SpecCube0_HDR;
			giInput.probeHDR[1] = unity_SpecCube1_HDR;
			#if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
				giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
			#endif
			#if UNITY_SPECCUBE_BOX_PROJECTION
				giInput.boxMax[0] = unity_SpecCube0_BoxMax;
				giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
				giInput.boxMax[1] = unity_SpecCube1_BoxMax;
				giInput.boxMin[1] = unity_SpecCube1_BoxMin;
				giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
			#endif

			  
			half4 c = 0;
			#if defined(LIGHTMAP_ON) 
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
				c = samplerLightmap_blinnphone(d); 
			#else 
				c = CacuLightedColor(o,viewDir,giInput); 
			#endif
	 
			PURE_APPLY_FOG(IN.fogCoord, c); 
			UNITY_OPAQUE_ALPHA(c.a);
  
			return c;
		}  

		 

#endif 