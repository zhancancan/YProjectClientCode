#ifndef BAKED_DIFFUSE_ADD_CG 
#define BAKED_DIFFUSE_ADD_CG

 
 		#pragma multi_compile_fog
		#pragma multi_compile_fwdadd 
  
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "AutoLight.cginc" 
		#include "../Core/fog_tools.cginc"
		#include "../Core/math_tools.cginc"
	 
    

		sampler2D _MainTex; 
		
		half4 _MainTex_ST; 
		half4 _Color;
		 # if CUT_OFF
			half _cutThreshhold;
		#endif


	  
	 
		struct v2f_surf {
		  half4 pos : SV_POSITION;
		  half4 uv : TEXCOORD0; 
		  half3 normal:TEXCOORD2;
		  half3 worldPos:TEXCOORD3;
		  SHADOW_COORDS(4)
		  UNITY_FOG_COORDS(5)
		};

		// vertex shader
		v2f_surf vert (appdata_full v) {
			v2f_surf o;
			UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
			#if VANIM_ON
				half4 off = vertAnim(v.vertex,v.color.r);  
				o.pos = UnityObjectToClipPos(off); 
				o.worldPos= mul(unity_ObjectToWorld,off).xyz;
			#else  
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldPos= mul(unity_ObjectToWorld, v.vertex).xyz;
			#endif
			 
			o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);  			
			o.normal =  UnityObjectToWorldNormal(v.normal);


			TRANSFER_SHADOW(o);
			PURE_TRANSFER_FOG(o,v.vertex); 
			return o;
		}

			// fragment shader
		half4 frag (v2f_surf IN) : SV_Target { 
		 
			half3 worldPos = IN.worldPos;
			#ifndef USING_DIRECTIONAL_LIGHT
				half3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
			#else
				half3 lightDir = _WorldSpaceLightPos0.xyz;
			#endif

			half3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
			 
			 
			UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)


			// call surface function 
			half4 tex = tex2D(_MainTex, IN.uv.xy); 			
			#if CUT_OFF
				clip(tex.a* _Color.a - _cutThreshhold);
			#endif
			half3 lightColor =  _LightColor0.rgb * atten; 
			half diff = max (0, dot (IN.normal, lightDir)); 
			half4 c=0;
			c.rgb = tex.rgb*_Color.rgb*  lightColor *diff ;
			c.a = 0; 
			
 
			PURE_APPLY_FOG(IN.fogCoord, c); // apply fog
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}
#endif 