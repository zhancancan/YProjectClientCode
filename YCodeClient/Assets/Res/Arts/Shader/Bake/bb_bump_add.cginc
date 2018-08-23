#ifndef BAKED_BUMP_ADD_CG 
#define BAKED_BUMP_ADD_CG

  
		#pragma multi_compile_fwdadd 
  
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "AutoLight.cginc" 
		#include "../Core/fog_tools.cginc" 
		#include "../Core/math_tools.cginc" 

	 
 

		sampler2D _MainTex;
		sampler2D _BumpMap;
		
		half4 _MainTex_ST;
		half4 _BumpMap_ST;

		half4 _Color;
		half _Shininess; 
		half _Specular;

		# if CUT_OFF
			half _cutThreshhold;
		#endif
	  
	 
		struct v2f_surf {
		  half4 pos : SV_POSITION;
		  half4 uv : TEXCOORD0; // _MainTex _BumpMap
		  half4 tSpace0 : TEXCOORD1;
		  half4 tSpace1 : TEXCOORD2;
		  half4 tSpace2 : TEXCOORD3; 
		  SHADOW_COORDS(5)
		  UNITY_FOG_COORDS(6)
		};

		// vertex shader
		v2f_surf vert (appdata_full v) {
			v2f_surf o;
			UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
			half3 worldPos;
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
			half3 worldNormal = UnityObjectToWorldNormal(v.normal);
			half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
			half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
			half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
			o.tSpace0 = half4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
			o.tSpace1 = half4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
			o.tSpace2 = half4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z); 

			TRANSFER_SHADOW(o);
			PURE_TRANSFER_FOG(o,v.vertex); 
			return o;
		}

			// fragment shader
		fixed4 frag (v2f_surf IN) : SV_Target { 
		 
			half3 worldPos = half3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
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
				clip(tex.a*_Color.a-_cutThreshhold);
			#endif

			
			half3 nrm = UnpackNormal(tex2D(_BumpMap, IN.uv.zw));
			half4 c = 0;
			half3 worldN;
			worldN.x = dot(IN.tSpace0.xyz, nrm);
			worldN.y = dot(IN.tSpace1.xyz, nrm);
			worldN.z = dot(IN.tSpace2.xyz, nrm);
			nrm = worldN; 

			half3 lightColor =  _LightColor0.rgb * atten; 
			half diff = max (0, dot (nrm, lightDir)); 

			c.rgb = tex.rgb*_Color.rgb*  lightColor *diff ;
			c.a = 0;

	 

			PURE_APPLY_FOG(IN.fogCoord, c); // apply fog
			UNITY_OPAQUE_ALPHA(c.a); 
		 
			return c;
		}
#endif 