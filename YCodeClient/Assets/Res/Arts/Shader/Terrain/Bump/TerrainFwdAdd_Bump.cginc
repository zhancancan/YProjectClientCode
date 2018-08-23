#ifndef TERRAIN_FORWARD_ADD_BUMP_CG 
#define TERRAIN_FORWARD_ADD_BUMP_CG

 
 
	#pragma multi_compile_fog 
	#pragma multi_compile_fwdadd
	#pragma enable_d3d11_debug_symbols

	#define UNITY_PASS_FORWARDADD

	#include "HLSLSupport.cginc"
	#include "UnityShaderVariables.cginc"
 
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"  
	#include "../../Core/fog_tools.cginc"  
	#include "../../Core/LightSpace.cginc"  

	
  
	struct v2f {
		float4 pos : SV_POSITION;
		float4 pack0 : TEXCOORD0; // _Control _Splat0
		float4 pack1 : TEXCOORD1; // _Splat1 _Splat2

		#if TEX_3 || TEX_4
		float2 pack2 : TEXCOORD2; // _Splat3
		#endif

		half3 tSpace0 : TEXCOORD3;
		half3 tSpace1 : TEXCOORD4;
		half3 tSpace2 : TEXCOORD5;

		half3 worldPos : TEXCOORD6;

		SHADOW_COORDS(7)
		UNITY_FOG_COORDS(8)
	};

	float4 _Control_ST;
	float4 _Splat0_ST;
	float4 _Splat1_ST;
	#if TEX_3||TEX_4
	float4 _Splat2_ST;
	#endif
	#if TEX_4
	float4 _Splat3_ST;
	#endif
	 
 

				// vertex shader
		inline v2f vertFwdAdd (appdata_full v) {
			v2f o;
			UNITY_INITIALIZE_OUTPUT(v2f,o);
			vert(v);
	  
			o.pos = UnityObjectToClipPos(v.vertex);		
			o.pack0.xy = TRANSFORM_TEX(v.texcoord, _Control);
			o.pack0.zw = TRANSFORM_TEX(v.texcoord, _Splat0);
			o.pack1.xy = TRANSFORM_TEX(v.texcoord, _Splat1);

			#if TEX_3 || TEX_4
			o.pack1.zw = TRANSFORM_TEX(v.texcoord, _Splat2);
			#endif

			#if TEX_4
			o.pack2.xy = TRANSFORM_TEX(v.texcoord, _Splat3);
			#endif

			half3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			half3 worldNormal = UnityObjectToWorldNormal(v.normal);
			half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
			half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
			half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
			o.tSpace0 = fixed4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
			o.tSpace1 = fixed4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
			o.tSpace2 = fixed4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
			o.worldPos = worldPos;

			TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
			PURE_TRANSFER_FOG(o,v.vertex); // pass fog coordinates to pixel shade
			return o;
		}

	 	inline half4 CacuLightedColor(SurfaceOutput o , half3 viewDir ,UnityLight light){ 
		 
			half4 c;
			 

			half diff = max (0, dot (o.Normal, light.dir));
			
			half3 sd =normalize(specularLightDir.xyz); 
			half3 h = normalize (sd + viewDir);
			float nh = max (0, dot (o.Normal, h));
			float spec = pow (nh, o.Specular*128.0) * o.Gloss;

 
			c.rgb = o.Albedo * light.color * diff +  _SpecColor.rgb * spec;
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
		 
	// fragment shader
	inline half4 fragFwdAdd (v2f IN) : SV_Target { 
	
	//	UNITY_SETUP_INSTANCE_ID(IN);

		Input surfIN;
		UNITY_INITIALIZE_OUTPUT(Input,surfIN);
  

		surfIN.worldPos.x = 1.0;
		surfIN.uv_Control = IN.pack0.xy;
		surfIN.uv_Splat0 = IN.pack0.zw;
		surfIN.uv_Splat1 = IN.pack1.xy;

		#if TEX_3||TEX_4
		surfIN.uv_Splat2 = IN.pack1.zw;
		#endif
		#if TEX_4
		surfIN.uv_Splat3 = IN.pack2.xy; 
		#endif

		float3 worldPos = IN.worldPos;
		#ifndef USING_DIRECTIONAL_LIGHT
			fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
		#else
			fixed3 lightDir = _WorldSpaceLightPos0.xyz;
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
		UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)



		
		fixed4 c = 0;
		 

		half3 worldN;
		worldN.x = dot(IN.tSpace0.xyz, o.Normal);
		worldN.y = dot(IN.tSpace1.xyz, o.Normal);
		worldN.z = dot(IN.tSpace2.xyz, o.Normal);
		o.Normal = worldN;

		// Setup lighting environment
	 
		half3 lightColor =  _LightColor0.rgb * atten;

		 
			
		half diff = max (0, dot (o.Normal, lightDir));
		half3 sd =normalize(specularLightDir.xyz); 
		half3 h = normalize (sd + viewDir);
		half nh = max (0, dot (o.Normal, h));
		half spec = pow (nh, o.Specular*128.0) * o.Gloss;

 
		c.rgb = o.Albedo *  lightColor *diff ;//+  _SpecColor.rgb * spec;
		c.a = o.Alpha;
	 
		PURE_APPLY_FOG(IN.fogCoord, c); // apply fog
		UNITY_OPAQUE_ALPHA(c.a); 
		return c;
	} 

 

#endif 