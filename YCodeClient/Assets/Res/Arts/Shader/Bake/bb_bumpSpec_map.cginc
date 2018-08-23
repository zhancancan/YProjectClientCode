#ifndef BAKED_BUMPSPEC_MAP_CG
#define BAKED_BUMPSPEC_MAP_CG

 
   
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc" 
			
	#include "../Core/LightSpace.cginc"
	#include "../Core/fog_tools.cginc"
	#include "../Core/math_tools.cginc"
	#include "aux_color_tools.cginc"


	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;
	half _Shininess; 
	half _BumpPower; 

			
	half4 _MainTex_ST;
	half4 _BumpMap_ST;
	 
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
		half3 VNormal;
		half3 Emission; 
		half3 Specular; 
		half Alpha; 
		half Gloss; 
	 };

		 
 
 	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0;
		 
		half4 tSpace0 : TEXCOORD1;			
		half4 tSpace1 : TEXCOORD2;		
		half4 tSpace2 : TEXCOORD3;					 
						 
		UNITY_FOG_COORDS(4)
			
		half4 lmap:TEXCOORD5; 
	}; 
	 
	v2f vert (appdata_full v) {
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f,o); 	
		half3 worldPos ;		
		#if VANIM_ON
			half4 off = vertAnim(v.vertex,v.color.r);  
			o.pos = UnityObjectToClipPos(off); 
			worldPos= mul(unity_ObjectToWorld,off).xyz;
		#else 
			o.pos = UnityObjectToClipPos(v.vertex);
			worldPos= mul(unity_ObjectToWorld, v.vertex).xyz;
		#endif  	    
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.uv.zw = TRANSFORM_TEX(v.texcoord, _BumpMap);		
		o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;  
		
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
		half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
		o.tSpace0 = half4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
		o.tSpace1 = half4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
		o.tSpace2 = half4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);	   
		o.lmap.z = dot(worldNormal.xyz,specularLightDir.xyz);

		PURE_TRANSFER_FOG(o,v.vertex);
		return o; 
	}


 
	half4 frag (v2f i) : SV_Target {    
		half3 worldPos = float3(i.tSpace0.w, i.tSpace1.w, i.tSpace2.w);	 
		half3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));		 
		SurfaceData o; 
		 
		half4 tex = tex2D(_MainTex, i.uv.xy);
		#if CUT_OFF
			clip(tex.a- _Color.a - _cutThreshhold);
		#endif
		o.Albedo = tex.rgb * _Color.rgb;
		o.Gloss = tex.a;
		o.Alpha = tex.a * _Color.a;
		o.Specular = _Shininess;
		o.Normal = UnpackNormal(tex2D(_BumpMap, i.uv.zw)); 

		#if ENABLE_ILLUMIN 
			o.Emission = o.Albedo.rgb * tex2D(_IlluminTex, i.uv.xy).r; 
			o.Emission *= _Emission; 
		#endif
		 

		half4 c = 0; 
		o.VNormal = o.Normal;
		half3 worldN;
		worldN.x = dot(i.tSpace0.xyz, o.Normal);
		worldN.y = dot(i.tSpace1.xyz, o.Normal);
		worldN.z = dot(i.tSpace2.xyz, o.Normal);
		o.Normal = worldN;
		 
	 
		 
		 #ifdef LIGHTMAP_ON  
		 	BlinnphoneData d ; 
			d.albedo = o.Albedo;
			d.alpha = o.Alpha; 
			d.lightDir = specularLightDir; 
			d.lightmapUV = i.lmap.xy; 
			d.dotVNrm = i.lmap.z;
			d.nrm = worldN;
			d.bumpPower = _BumpPower; 
			d.viewDir = worldViewDir; 
			d.specular = o.Specular; 
			d.gloss = o.Gloss; 
			d.specColor = _SpecColor; 
			c = samplerLightmap_blinnphone(d);  
		#else
			c = 1;
		#endif

		#if ENABLE_ILLUMIN 
			c.rgb += o.Emission;  
		#endif 

		#if AUX_COLOR_ON 
			c = auxColor(c,i.uv.xy); 
		#endif

		PURE_APPLY_FOG(i.fogCoord, c); // apply fog
		UNITY_OPAQUE_ALPHA(c.a);
		return c; 
	}  

#endif 