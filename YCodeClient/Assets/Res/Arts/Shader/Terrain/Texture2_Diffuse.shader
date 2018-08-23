Shader "Terrain/Texture2_Diffuse" {
	Properties { 
		_Splat0 ("Layer 0 (R)", 2D) = "white" {}
		_Splat1 ("Layer 1 (G)", 2D) = "white" {}   
		_Control ("Control (RGBA)", 2D) = "white" {}
		_MainTex ("Never Used", 2D) = "white" {}
	} 
 
	CGINCLUDE
		
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		struct Input {
			half3 worldPos;
			half2 uv_Control : TEXCOORD0;
			half2 uv_Splat0 : TEXCOORD1;
			half2 uv_Splat1 : TEXCOORD2; 
		};

		void vert (inout appdata_full v) { 
			half3 T1 = float3(1, 0, 1);
			half3 Bi = cross(T1, v.normal);
			half3 newTangent = cross(v.normal, Bi);	
			normalize(newTangent);
			v.tangent.xyz = newTangent.xyz;	
			if (dot(cross(v.normal,newTangent),Bi) < 0)
				v.tangent.w = -1.0f;
			else
				v.tangent.w = 1.0f;   
		}

		sampler2D _Control;
		sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
		half4 _Tiling3;
		inline void surf (Input IN, inout SurfaceOutput o) {
			half2 splat_control = tex2D (_Control, IN.uv_Control);
			half3 col;
			half4 splat0 = tex2D (_Splat0, IN.uv_Splat0);
			half4 splat1 = tex2D (_Splat1, IN.uv_Splat1);
	
			col = splat_control.r * splat0.rgb;
			col += splat_control.g * splat1.rgb; 			 
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
				#include "Diffuse/TerrainFwdBase.cginc"
				#pragma vertex vertFwdBase
				#pragma fragment fragFwdBase 
				#pragma multi_compile_fog				
				#pragma multi_compile _ FOG_Y_AXIS
				#pragma multi_compile_fwdbase  
				#pragma only_renderers d3d11
				#pragma multi_compile TEX_2			    
			ENDCG  
		}

		Pass {
			Name "FORWARD"
			Tags{"LightMode" = "ForwardAdd"}
			ZWrite Off Blend One One
			CGPROGRAM 
				#include "Diffuse/TerrainFwdAdd.cginc"
				#pragma vertex vertFwdAdd
				#pragma fragment fragFwdAdd 
				#pragma multi_compile_fog				
				#pragma multi_compile _ FOG_Y_AXIS
				#pragma multi_compile_fwdadd
				#pragma only_renderers d3d11
				#pragma multi_compile TEX_2			 
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
				#pragma multi_compile TEX_2			 
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
				#include "Diffuse/Terrain_LM_3.cginc"
				#pragma vertex vertLM
				#pragma fragment fragLM 
				#pragma multi_compile_fog				
				#pragma multi_compile _ FOG_Y_AXIS
				#pragma exclude_renderers d3d11 d3d9 d3d11_9x xboxone ps4 xbox360 ps3 
				#pragma multi_compile TEX_2  
			ENDCG     
		}
		         
	} 
	Fallback OFF
}
 