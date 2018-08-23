#ifndef BAKED_BUMP_MAP_CG
#define BAKED_BUMP_MAP_CG
  
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"
			
	#include "../Core/LightSpace.cginc"
	#include "../Core/fog_tools.cginc"
	#include "../Core/math_tools.cginc"

	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color; 
			
	half4 _MainTex_ST;
	half4 _BumpMap_ST;
	 
	half _BumpPower;

	 

	#if ENABLE_ILLUMIN 
		half _Emission;	
		sampler2D _IlluminTex;
		float4 _IlluminTex_ST;
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

		 
 
 	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0; 						 
		UNITY_FOG_COORDS(2)			
		half4 lmap:TEXCOORD3; 
		half4 tSpace0 : TEXCOORD4;
		half4 tSpace1 : TEXCOORD5;
		half4 tSpace2 : TEXCOORD6;
	}; 
	 
	v2f vert (appdata_full v) {
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f,o); 		
		#if VANIM_ON
			half4 off = vertAnim(v.vertex,v.color.r);  
			o.pos = UnityObjectToClipPos(off);  
		#else 
			o.pos = UnityObjectToClipPos(v.vertex); 
		#endif  		    
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.uv.zw = TRANSFORM_TEX(v.texcoord, _BumpMap);		
		o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;  
		
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);	
		half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
		half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;	
		o.tSpace0 = half4(worldTangent.x, worldBinormal.x, worldNormal.x,0);
		o.tSpace1 = half4(worldTangent.y, worldBinormal.y, worldNormal.y,0);
		o.tSpace2 = half4(worldTangent.z, worldBinormal.z, worldNormal.z,0);
		o.lmap.z= dot(worldNormal,specularLightDir);
	 
		PURE_TRANSFER_FOG(o,v.vertex);
		return o; 
	}


			
 
	
	fixed4 frag (v2f IN) : SV_Target {     	 
		SurfaceData o; 		 
		half4 tex = tex2D(_MainTex, IN.uv.xy);
		# if CUT_OFF  
			clip(tex.a*_Color.a- _cutThreshhold);
		#endif

		o.Albedo = tex.rgb * _Color.rgb;
		o.Gloss = tex.a;
		o.Alpha = tex.a * _Color.a;
		o.Specular = 0;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv.zw)); 
		
		#if ENABLE_ILLUMIN 
			o.Emission = o.Albedo.rgb * tex2D(_IlluminTex, IN.uv.xy).r; 
			o.Emission *= _Emission; 
		#endif 
		
		half4 c = 0;  
				
		#ifdef LIGHTMAP_ON 
			half3 nrm; 
			nrm.x = dot(IN.tSpace0.xyz, o.Normal);
			nrm.y = dot(IN.tSpace1.xyz, o.Normal);
			nrm.z = dot(IN.tSpace2.xyz, o.Normal); 
			LambertData d ; 
			d.albedo = o.Albedo; 
			d.alpha = o.Alpha;
			d.lightDir = specularLightDir; 
			d.lightmapUV = IN.lmap.xy;
			d.dotVNrm = IN.lmap.z;  
			d.nrm = nrm; 
			d.bumpPower =_BumpPower;  
			c= samplerLightmap_lambert(d);  
		#else
			c = 1;
		#endif

		#if ENABLE_ILLUMIN 
			c.rgb += o.Emission;
		#endif
		#if AUX_COLOR_ON 
			c = auxColor(c,IN.uv.xy);
		#endif  
		PURE_APPLY_FOG(IN.fogCoord, c); // apply fog
		UNITY_OPAQUE_ALPHA(c.a);
		return c; 
	}  

#endif 