#ifndef WATER_LIGHT_CG 
#define WATER_LIGHT_CG
 
 
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc" 
	#include "../Core/fog_tools.cginc"

	half4 _Specular;

	struct WaterColor{
		 half Gloss;  
		 half Specular;
		 half Alpha;
		 half3 Albedo;
		 half3 Emission;
		 half atten;
		 half3 normal; 
		 half3 viewDir; 
		 half3 lightDir;
		 half3 worldPos;
	};




	struct v2f {
		half4 pos: SV_POSITION; 
		half4 tSpace0:TEXCOORD0; 
		half4 tSpace1:TEXCOORD1; 
		half4 tSpace2:TEXCOORD2; 		 
		half4 proj0:TEXCOORD3; 
		half4 proj1:TEXCOORD4;

		UNITY_FOG_COORDS(5)  
		SHADOW_COORDS(6)
	}; 


	inline v2f vertCacu(appdata_full v){

		v2f o; 
		UNITY_INITIALIZE_OUTPUT(v2f,o); 		 
					
		half4 position  = UnityObjectToClipPos(v.vertex); 
		o.pos = position;
		o.proj0 = ComputeScreenPos(position);	
		COMPUTE_EYEDEPTH(o.proj0.z);	
		 
		o.proj1 = o.proj0;

		#if UNITY_UV_STARTS_AT_TOP
			o.proj1.y = (position.w - position.y) * 0.5;
		#endif 

		half3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
		half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
		o.tSpace0 = half4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
		o.tSpace1 = half4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
		o.tSpace2 = half4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
			 
		PURE_TRANSFER_FOG(o,v.vertex);  
		return o; 
	}

  
 
	inline half4 CacuWaterLight(WaterColor s){	
		half3 nNormal = normalize(s.normal);
		half shininess = s.Gloss * 250.0 + 4.0;
		
		s.lightDir = normalize(s.lightDir);

		//half reflectiveFactor = max(0.0, dot(-s.viewDir, reflect(s.lightDir, nNormal)));	// Phong shading model

		// Blinn-Phong shading model
		half reflectiveFactor = max(0.0, dot(nNormal, normalize(s.lightDir + s.viewDir)));
	 
		half diffuseFactor = max(0.0, dot(nNormal, s.lightDir));
		half specularFactor = pow(reflectiveFactor, shininess) * s.Specular;
	 
		half4 c;
		c.rgb = (s.Albedo * diffuseFactor + _Specular.rgb * specularFactor) * _LightColor0.rgb;
		c.rgb *= (s.atten * 2.0);
		c.a = s.Alpha;  
		return c;
	}

	 
 








#endif 