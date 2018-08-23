#ifndef BAKED_DIFFUSE_BASE_CG
#define BAKED_DIFFUSE_BASE_CG

	#pragma multi_compile_fog 
	#define UNITY_PASS_FORWARDBASE 
		   

	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"
			
	#include "../Core/LightSpace.cginc"
	#include "../Core/fog_tools.cginc"
	#include "../Core/math_tools.cginc"
	#include "aux_color_tools.cginc"


	sampler2D _MainTex; 			
	half4 _MainTex_ST;  
	half4 _Color;
	 
	#if ENABLE_ILLUMIN 
		half _Emission;	
		sampler2D _IlluminTex;
		half4 _IlluminTex_ST;
	#endif
	# if CUT_OFF
		half _cutThreshhold;
	#endif

	 struct SurfaceData{
		half3 Albedo; 
		half3 Normal;  
		half3 Emission; 
		half3 Specular; 
		half Alpha; 
		half Gloss; 
	 };

		 
	 
	#ifndef LIGHTMAP_ON
	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0; // _MainTex _BumpMap 
		half3 normal:TEXCOORD1;
		half3 worldPos:TEXCOORD2;
		#if UNITY_SHOULD_SAMPLE_SH
		half3 sh : TEXCOORD3; // SH
		#endif
		SHADOW_COORDS(4)
		UNITY_FOG_COORDS(5)
		float4 lmap:TEXCOORD6;
			 
		 
	};
	#else
 	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0; // _Control _Splat0	 
		 
		half3 normal:TEXCOORD1; 
		half3 worldPos:TEXCOORD2;
		SHADOW_COORDS(3)
		UNITY_FOG_COORDS(4)
			
		half4 lmap:TEXCOORD5;
		#if UNITY_SHOULD_SAMPLE_SH
		half3 sh :TEXCOORD6;
		#endif						  
	};
	#endif	

	// vertex shader
	v2f vert (appdata_full v) {
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f,o); 
					   
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
 
		      
		o.normal =  UnityObjectToWorldNormal(v.normal);  
		#if VANIM_ON
			half4 off = vertAnim(v.vertex,v.color.r); 
			o.worldPos=mul(unity_ObjectToWorld, off).xyz;  
			o.pos = UnityObjectToClipPos(off); 

		#else 
			o.worldPos= mul(unity_ObjectToWorld, v.vertex).xyz; 
			o.pos = UnityObjectToClipPos(v.vertex);
		#endif

	  


		#ifdef DYNAMICLIGHTMAP_ON
			o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
		#endif
		#ifdef LIGHTMAP_ON
			o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
		#endif		 
		#ifndef LIGHTMAP_ON
			#if UNITY_SHOULD_SAMPLE_SH
				o.sh = 0; 
				#ifdef VERTEXLIGHT_ON
				o.sh += Shade4PointLights (
					unity_4LightPosX0,
					unity_4LightPosY0,
					unity_4LightPosZ0,
					unity_LightColor[0].rgb, 
					unity_LightColor[1].rgb, 
					unity_LightColor[2].rgb, 
					unity_LightColor[3].rgb,
					unity_4LightAtten0, o.worldPos, o.normal);
				#endif
				o.sh = ShadeSHPerVertex (o.normal, o.sh);
			#endif
		#endif // !LIGHTMAP_ON

		TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
		PURE_TRANSFER_FOG(o,v.vertex); // pass fog coordinates to pixel shader
		return o; 
	}


			
	inline half4 SampleLightmap(SurfaceData o , half3 viewDir, UnityGIInput data){	 
		half4 ct=UNITY_SAMPLE_TEX2D(unity_Lightmap, data.lightmapUV.xy);
		half3 color = ParseLightmap(ct);	 
		 
		half4 c ;
		c.rgb= o.Albedo * color ;   
		c.a= o.Alpha;
		return c; 

	}
	 
	inline half4 CacuLightedColor(SurfaceData o , half3 viewDir ,UnityGIInput data){
		UnityLight light=data.light; 
		light.color*=data.atten;
		 
		half4 c; 

		half diff = max (0, dot (o.Normal, light.dir)); 
		c.rgb = o.Albedo * light.color * diff ;
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
	half4 frag (v2f IN) : SV_Target {    
		half3 worldPos = IN.worldPos;
		#ifndef USING_DIRECTIONAL_LIGHT
			half3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
		#else
			half3 lightDir = _WorldSpaceLightPos0.xyz;
		#endif
		half3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
 
		 
		SurfaceData o; 

		 
		half4 tex = tex2D(_MainTex, IN.uv.xy);
		#if CUT_OFF 
			clip(tex.a* _Color.a - _cutThreshhold);
		#endif
		o.Albedo = tex.rgb * _Color.rgb;
		o.Gloss = tex.a;
		o.Alpha = tex.a * _Color.a;
		o.Specular = 0;
		o.Normal = IN.normal;
		#if ENABLE_ILLUMIN 
			o.Emission = o.Albedo.rgb * tex2D(_IlluminTex, IN.uv.xy).r; 
			o.Emission *= _Emission; 
		#endif
		// compute lighting & shadowing factor
		UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
		half4 c = 0; 

 
		o.Normal = IN.normal;
 


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
		giInput.worldViewDir = worldViewDir;
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
			  
		#if LIGHTMAP_ON 			
			c = SampleLightmap(o,worldViewDir, giInput);
		#else 
			c = CacuLightedColor(o,worldViewDir, giInput);
		#endif

		#if ENABLE_ILLUMIN 
			c.rgb+=o.Emission;
		#endif
		#if AUX_COLOR_ON 
			c = auxColor(c,IN.uv.xy);
		#endif
		PURE_APPLY_FOG(IN.fogCoord, c); // apply fog 
	 
		#if !TRANSPARENT
		UNITY_OPAQUE_ALPHA(c.a); 
		#endif
		return c; 
	}  

#endif 