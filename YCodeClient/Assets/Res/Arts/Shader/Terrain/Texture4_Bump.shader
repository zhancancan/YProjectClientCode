Shader "Terrain/Texture4_Bump" {
	Properties { 	
		_SpecColor ("Specular Color", Color) = (1, 1, 1, 1)

		_ShininessL0 ("Shininess 0", Range (0.03, 1)) = 0.078125
		_Splat0 ("Layer 0 (R)", 2D) = "white" {}
		_BumpSplat0 ("NormalMap 0", 2D) = "bump" {} 
		
		_ShininessL1 ("Shininess 1", Range (0.03, 1)) = 0.078125
		_Splat1 ("Layer 1 (G)", 2D) = "white" {}    
		_BumpSplat1 ("NormalMap 1", 2D) = "bump" {} 
		
		_ShininessL2 ("Shininess 2", Range (0.03, 1)) = 0.078125
		_Splat2 ("Layer 2 (G)", 2D) = "white" {}    
		_BumpSplat2 ("NormalMap 2", 2D) = "bump" {} 
		
		_ShininessL3 ("Shininess 3", Range (0.03, 1)) = 0.078125
		_Splat3 ("Layer 3 (G)", 2D) = "white" {}   
		_BumpSplat3 ("NormalMap 3", 2D) = "bump" {} 

		_Control ("Control (RGBA)", 2D) = "white" {}
		_MainTex ("Never Used", 2D) = "white" {} 

		_BumpPower("Bump Power",Range(0,1)) =0.5
	           
	} 
 
	CGINCLUDE		
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		struct Input {
			half3 worldPos;
			half2 uv_Control : TEXCOORD0;
			half2 uv_Splat0 : TEXCOORD1;
			half2 uv_Splat1 : TEXCOORD2; 
			half2 uv_Splat2 : TEXCOORD3; 
			half2 uv_Splat3 : TEXCOORD4; 
		};

		void vert (inout appdata_full v) { 
			float3 T1 = float3(1, 0, 1);
			float3 Bi = cross(T1, v.normal);
			float3 newTangent = cross(v.normal, Bi);	
			normalize(newTangent);
			v.tangent.xyz = newTangent.xyz;	
			if (dot(cross(v.normal,newTangent),Bi) < 0)
				v.tangent.w = -1.0f;
			else
				v.tangent.w = 1.0f; 
		}

		sampler2D _Control;
		sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
		sampler2D _BumpSplat0,_BumpSplat1,_BumpSplat2,_BumpSplat3;
		half _ShininessL0;
		half _ShininessL1; 
		half _ShininessL2; 
		half _ShininessL3; 

		inline void surf (Input IN, inout SurfaceOutput o) {
			half4 splat_control = tex2D (_Control, IN.uv_Control);
			half3 col;
			half4 splat0 = tex2D (_Splat0, IN.uv_Splat0);
			half4 splat1 = tex2D (_Splat1, IN.uv_Splat1);
			half4 splat2 = tex2D (_Splat2, IN.uv_Splat2);
			half4 splat3 = tex2D (_Splat3, IN.uv_Splat3);
	
			col  = splat_control.r * splat0.rgb;
			o.Normal = splat_control.r * UnpackNormal(tex2D(_BumpSplat0, IN.uv_Splat0));
			o.Gloss = splat0.a * splat_control.r ;
			o.Specular = _ShininessL0 * splat_control.r;

			col += splat_control.g * splat1.rgb;
			o.Normal += splat_control.g * UnpackNormal(tex2D(_BumpSplat1, IN.uv_Splat1));
			o.Gloss += splat1.a * splat_control.g;
			o.Specular += _ShininessL1 * splat_control.g;
	 
			col += splat_control.b * splat2.rgb;
			o.Normal += splat_control.b * UnpackNormal(tex2D(_BumpSplat2, IN.uv_Splat2));
			o.Gloss += splat2.a * splat_control.b;
			o.Specular += _ShininessL2 * splat_control.b;
	

			col += splat_control.a * splat3.rgb;
			o.Normal += splat_control.a * UnpackNormal(tex2D(_BumpSplat3, IN.uv_Splat3));
			o.Gloss += splat3.a * splat_control.a;
			o.Specular += _ShininessL3 * splat_control.a; 

			o.Albedo = col;
			o.Alpha = 0.0;
		}
			
	ENDCG

	  
	  SubShader {
		Tags { 
			"Queue" = "Geometry-100"
			"RenderType" = "Opaque"
		}  
		Pass {
			Name "FORWARD"
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM 
				#include "Bump/TerrainFwdBase_Bump.cginc"
				#pragma vertex vertFwdBase
				#pragma fragment fragFwdBase 
				#pragma multi_compile_fog
				#pragma multi_compile _ FOG_Y_AXIS
				#pragma multi_compile_fwdbase  
				#pragma only_renderers d3d11
				#pragma multi_compile TEX_4 			 
			ENDCG  
		}


		Pass {
			Name "FORWARD ADD"
			Tags{"LightMode" = "ForwardAdd"}
			ZWrite Off Blend One One
			CGPROGRAM 
				#include "Bump/TerrainFwdAdd_Bump.cginc"
				#pragma vertex vertFwdAdd
				#pragma fragment fragFwdAdd 
				#pragma multi_compile_fog
				#pragma multi_compile _ FOG_Y_AXIS
				#pragma multi_compile_fwdadd
				#pragma only_renderers d3d11
				#pragma multi_compile TEX_4
		 
			ENDCG  
		}

		Pass {
			Name "ShadowCaster"
			Tags{"LightMode" = "ShadowCaster"}
			ZWrite On ZTest LEqual
			CGPROGRAM 
				#include "Diffuse/TerrainShadowCaster.cginc"
				#pragma vertex vertShadowCaster
				#pragma fragment fragShadowCast 
				#pragma multi_compile_fog
				#pragma multi_compile _ FOG_Y_AXIS
				#pragma multi_compile_shadowcaster
				#pragma only_renderers d3d11
				#pragma multi_compile TEX_4 
			 
			ENDCG  
		}
	}
	 
		 
	SubShader {
		Tags { 
			"Queue" = "Geometry-100"
			"RenderType" = "Opaque"
		}        

		Pass {
			Name "FORWARD"
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM 
				#include "Bump/Terrain_LM3_Bump.cginc" 
				#pragma vertex vertLM
				#pragma fragment fragLM 
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS
				#pragma multi_compile_fwdbase  
				#pragma exclude_renderers d3d11 d3d9 d3d11_9x xboxone ps4 xbox360 ps3 
				#pragma multi_compile TEX_4	 
			ENDCG   
		}
	} 
	Fallback OFF
}
 