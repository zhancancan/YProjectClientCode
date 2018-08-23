Shader "Pure/Bake/Bumped Specular" {
 
	Properties { 		
		[Enum (UnityEngine.Rendering.CullMode)] _Cull("Cull Mode",Float)=2
		_Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_BumpPower ("Bump Power", Range(0,1)) = 0.5
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {} 		 
		_IlluminTex ("Illumin(A)", 2D) = "black" {} 
		_Emission("Emission" ,Float)=0 
		[Toggle(ENABLE_ILLUMIN)] _illumin("Use Illumin",Float)=0
		[Toggle(VANIM_ON)] _vertexAnim("Vertex Anim",Float)=0	
		_WinDir ("Wind Dir", Vector) = (0.0, -0.7, 0.7, 1)   
		
		[Toggle(AUX_COLOR_ON)] _aux("Use Aux Color",Float)=0	 
		_AuxTex("Aux Texture",2D) = "white"{}
		_AUxCtrlTex("Aux Control" ,2D)= "white"{}
		_AuxEmission("Emission",Float)=0
		_AuxRatio("Aux Ratio",Range(0,1))=0 
	} 
  	 

	SubShader { 
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque"			
		}  
		Cull [_Cull]
		Pass { 
	
			Name "FORWARD"
			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM 
				#include "bb_bumpSpec_base.cginc"
				#pragma multi_compile_fwdbase
				#pragma vertex vert
				#pragma fragment frag  				  
				#pragma only_renderers d3d11 	
				#pragma multi_compile _ ENABLE_ILLUMIN		
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS			   
				#pragma multi_compile _ VANIM_ON		   
				#pragma multi_compile _ AUX_COLOR_ON	
		 
			ENDCG  
		}


		Pass { 
		
			Tags { "LightMode" = "ForwardAdd" }
			ZWrite Off Blend One One 
			CGPROGRAM 
				#include "bb_bump_add.cginc"
				#pragma multi_compile_fwdadd 
				#pragma vertex vert
				#pragma fragment frag   
				#pragma only_renderers d3d11 	
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS	
				#pragma multi_compile _ VANIM_ON		   
				#pragma multi_compile _ AUX_COLOR_ON
			 
			ENDCG  
		}


		Pass {
			Name "ShadowCaster"
			Tags{"LightMode" = "ShadowCaster"}
			ZWrite On ZTest LEqual
			CGPROGRAM 
				#include "bb_shadowCaster.cginc"
				#pragma vertex vert
				#pragma fragment frag  
				#pragma multi_compile_shadowcaster
				#pragma only_renderers d3d11 
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS	
				#pragma multi_compile _ VANIM_ON		   
				#pragma multi_compile _ AUX_COLOR_ON
			ENDCG  
		}
	}
	SubShader { 
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}         
		Cull [_Cull]
		Pass {
			Name "FORWARD"
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM 
				#include "bb_bumpSpec_map.cginc"
				#pragma vertex vert
				#pragma fragment frag  
				#pragma multi_compile_fog    
				#pragma multi_compile LIGHTMAP_ON
				#pragma multi_compile _ ENABLE_ILLUMIN		
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS	
				#pragma multi_compile _ VANIM_ON		   
				#pragma multi_compile _ AUX_COLOR_ON
				#pragma exclude_renderers d3d11 d3d9 d3d11_9x xboxone ps4 xbox360 ps3   		 
			ENDCG  
		}
	} 
	Fallback OFF
}
 