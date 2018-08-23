#ifndef WATER_REFLECTION_0 
#define WATER_REFLECTION_0
 
	#include "WaterLightCG.cginc" 
	#include "AutoLight.cginc" 
	#include "../Core/fog_tools.cginc" 
	#include "UnityCG.cginc"
	#include "HLSLSupport.cginc"
 

		sampler2D_half _GrabTexture ;
		uniform half4 _GrabTexture_TexelSize;

		half _Shininess;
		half _Tiling;
		half _ReflectionTint;
		half4 _Reflection;
		half4 _InvRanges;
		half4 _Color0;
		half4 _Color1;

		sampler2D_float _CameraDepthTexture;
		sampler2D _WaterTex; 
		samplerCUBE _SkyTex;
 
		v2f vert(appdata_full v){
			return vertCacu(v);
		}
 
		
		inline half3 getReflection(half3 viewDir, half3 nrm, half fresnel){   
			return texCUBE(_SkyTex,reflect(viewDir,nrm)).rgb * _ReflectionTint;
		}
		inline half3 getRefraction(v2f IN, WaterColor o, half4 col, half3 ranges) {
			// High-quality reflection uses the dynamic reflection texture
			IN.proj0.xy += o.normal.xy * 0.5; 
			// High-quality refraction uses the grab pass texture  
			IN.proj1.xy += o.normal.xy * _GrabTexture_TexelSize.xy * (20 * IN.proj1.z * col.a);   
		 
			half3 refraction = tex2Dproj(_GrabTexture, IN.proj1).rgb; 

			refraction = lerp(refraction, refraction * col.rgb, ranges.z);			
		
			//// Color the refraction based on depth
			refraction = lerp(lerp(col.rgb, col.rgb * refraction, ranges.y), refraction, ranges.y);
			return refraction;

		}
		
		inline void CacuReflect(v2f i, inout WaterColor o) {
			// Calculate the world-space view direction (Y-up)
			// We can't use IN.viewDir because it takes the object's rotation into account, and the water should not.
			half3 worldView = (o.worldPos - _WorldSpaceCameraPos);

			// Calculate the object-space normal (Z-up)
			half offset = _Time.x * 0.5;
			half2 tiling = o.worldPos.xz * _Tiling;
			half4 nmap = (tex2D(_WaterTex, tiling + offset) + tex2D(_WaterTex, half2(-tiling.y, tiling.x) - offset)) * 0.5;
		
			#ifndef UNITY_COLORSPACE_GAMMA
				nmap=pow(nmap,0.45);  
			#endif
		 
		 	o.normal = nmap.xyz * 2.0 - 1.0;			
		 

			// World space normal (Y-up)
			half3 worldNormal = o.normal.xzy;
			worldNormal.z = -worldNormal.z; 

			// Calculate the depth difference at the current pixel   			
			float depth = tex2Dproj(_CameraDepthTexture, i.proj0).r;
			 
			depth = LinearEyeDepth(depth);
			depth -= i.proj0.z; 
			// Calculate the depth ranges (X = Alpha, Y = Color Depth)
			half3 ranges = saturate(_InvRanges.xyz * depth);
			ranges.y = 1.0 - ranges.y;
			ranges.y = lerp(ranges.y, ranges.y * ranges.y * ranges.y, 0.5);
		 
			// Calculate the color tint
			half4 col;
			
			col.rgb = lerp(_Color1.rgb, _Color0.rgb, ranges.y);		
			col.a = ranges.x;
			
 

			// Initial material properties
			o.Alpha =col.a;
			o.Specular = col.a;
			o.Gloss = _Shininess;

			// Dot product for fresnel effect
			half fresnel = sqrt(1.0 - dot(-normalize(worldView), worldNormal));
		
			half3 reflection = getReflection(worldView,worldNormal,fresnel);
			half3 refraction = getRefraction(i, o, col, ranges);
			 

			// The amount of foam added (35% of intensity so it's subtle)
			half foam = nmap.a * (1.0 - abs(col.a * 2.0 - 1.0)) * 0.35;

			// Always assume 20% reflection right off the bat, and make the fresnel fade out slower so there is more refraction overall
			fresnel *= fresnel * fresnel;
			fresnel = (0.8 * fresnel + 0.2) * col.a;
			
		 

			// Calculate the initial material color 
			o.Albedo = lerp(refraction, reflection, fresnel)  + foam;		
			o.Albedo*=_Reflection;
		   
			// Calculate the amount of illumination that the pixel has received already
			// Foam is counted at 50% to make it more visible at night
			fresnel = min(1.0, fresnel + foam * 0.5);
			o.Emission = o.Albedo * (1.0 - fresnel);  
		 
			//// Set the final color			
			o.Albedo *= fresnel;
		}



		
		half4 frag(v2f i):SV_TARGET0{ 
		 
			WaterColor o ; 
			UNITY_INITIALIZE_OUTPUT(WaterColor, o);   
			o.Albedo = 0.0;
			o.Emission = 0.0;
			o.Specular = 0.0;
			o.Alpha = 0.0;
			o.Gloss = 0.0;
		
			half3 worldPos= half3 (i.tSpace0.w,i.tSpace1.w,i.tSpace2.w);
			o.viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
			o.worldPos = worldPos; 
			CacuReflect(i, o);			
			
			UNITY_LIGHT_ATTENUATION(atten,i,worldPos);
			o.atten = atten;
			o.lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
 
			half4 c=0;	
		
			half3 worldN;
			worldN.x = dot(i.tSpace0.xyz, o.normal); 
			worldN.y = dot(i.tSpace1.xyz, o.normal); 
			worldN.z = dot(i.tSpace2.xyz, o.normal); 
			o.normal = worldN; 
			
			c += CacuWaterLight (o);
		
			c.rgb += o.Emission;
			PURE_APPLY_FOG(i.fogCoord,c);    
			 
			return c; 
		}

#endif