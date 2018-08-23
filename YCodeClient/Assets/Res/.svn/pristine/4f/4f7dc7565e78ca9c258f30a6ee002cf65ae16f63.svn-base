#ifndef BAKED_DIFFUSE_MAP_CG
#define BAKED_DIFFUSE_MAP_CG

 
   
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"
			
	#include "../Core/LightSpace.cginc"
	#include "../Core/fog_tools.cginc"
	#include "../Core/math_tools.cginc"


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
		half3 Emission; 
		half3 Specular; 
		half Alpha; 
		half Gloss; 
	 };

		 
 
 	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0; 						 
		UNITY_FOG_COORDS(1)			
		half4 lmap:TEXCOORD2; 
	}; 
	 
	v2f vert (appdata_full v) {
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f,o); 			
		#if VANIM_ON
			half4 off = vertAnim(v.vertex, v.color.r);
			o.pos = UnityObjectToClipPos(off);
			PURE_TRANSFER_FOG(o,off);
		#else 
			o.pos = UnityObjectToClipPos(v.vertex);
			PURE_TRANSFER_FOG(o,v.vertex);
		#endif	   
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);  
		o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;	 
		return o; 
	}


 
	
	half4 frag (v2f IN) : SV_Target {    		
		half4 tex = tex2D(_MainTex, IN.uv.xy);
		#if CUT_OFF 
			clip(tex.a*_Color.a - _cutThreshhold);
		#endif
		half4 ct=UNITY_SAMPLE_TEX2D(unity_Lightmap, IN.lmap.xy);
		half3 color = ParseLightmap(ct);
		half4 c =0;
		c.rgb=tex.rgb * _Color.rgb * color;  	 	  
		c.a= tex.a* _Color.a; 
		  
		#if ENABLE_ILLUMIN 
			c.rgb+=tex.rgb * tex2D(_IlluminTex, IN.uv.xy).r*_Emission; 			 
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