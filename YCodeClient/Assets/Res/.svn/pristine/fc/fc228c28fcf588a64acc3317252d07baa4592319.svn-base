#ifndef LIGHT_SPACE_CG 
#define LIGHT_SPACE_CG
 
	#include "UnityCG.cginc"   
		half4 specularLightDir;
		// the Unity's light space caculation seem has some problem on transfer for the LDR , we use here for general adjustment;
		inline half3 ParseLightmap(half4 data){ 		 
			#if defined(UNITY_NO_RGBM)   
				#if defined(UNITY_COLORSPACE_GAMMA)
					return 2.0 * data.rgb;
				#else    
					return unity_Lightmap_HDR.x * data.rgb; 
				#endif
			#else         
				#if defined(UNITY_COLORSPACE_GAMMA)    
					return unity_Lightmap_HDR.x * data.rgb;
				#else 
					return (unity_Lightmap_HDR.x * pow(data.a, unity_Lightmap_HDR.y)) * data.rgb; 
				#endif
			#endif
		}

		struct LambertData{
			half3 albedo;
			half alpha; 
			half3 lightDir; 
			half2 lightmapUV;
			half3 nrm; 
			half dotVNrm; 
			half bumpPower;
		};

		half calcBumpCeoff(half3 lightDir,half3 nrm, half dotVNrm,half bumpPower){
			half3 dir = normalize(lightDir); 	 
			half d = dot(nrm,dir); 	   
			d = (1+ (d-dotVNrm));
			d = bumpPower + d* (1-bumpPower);
			return d;
		}
  
		inline half4 samplerLightmap_lambert(LambertData o){
			half4 ct=UNITY_SAMPLE_TEX2D(unity_Lightmap,o.lightmapUV.xy);
			half3 color = ParseLightmap(ct);	  		 
			half d = calcBumpCeoff(o.lightDir,o.nrm,o.dotVNrm,o.bumpPower);
			half4 c ;
			c.rgb= o.albedo * color*d; 
			c.a=o.alpha;
			return c; 
		}


		struct BlinnphoneData{
			half3 albedo;
			half alpha; 
			half3 lightDir; 
			half2 lightmapUV;
			half3 nrm; 
			half dotVNrm; 
			half bumpPower;
			half3 viewDir; 
			half specular; 
			half gloss; 
			half3 specColor; 
			
		};

		inline half4 samplerLightmap_blinnphone(BlinnphoneData o){
			half4 ct=UNITY_SAMPLE_TEX2D(unity_Lightmap,o.lightmapUV.xy);
			half3 color = ParseLightmap(ct);	  
			half3 dir = normalize(o.lightDir); 	 
			half3 h = normalize(dir + o.viewDir);  
			o.nrm  = normalize(o.nrm);
			half nh = max(0, dot(o.nrm,h));     
			half spec = pow(nh,o.specular*128.0) * o.gloss ;   
			half bump =calcBumpCeoff(o.lightDir,o.nrm,o.dotVNrm,o.bumpPower);
			half4 c;
			c.rgb = o.albedo * color * bump; 
			c.rgb += o.specColor.rgb * spec; 
			c.a = o.alpha;
			return c;
		}

#endif 