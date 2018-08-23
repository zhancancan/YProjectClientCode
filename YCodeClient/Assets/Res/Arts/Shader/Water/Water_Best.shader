Shader "Pure/Water/WaterBest"{
	Properties{
		_WaterTex("Normal Map (RGB), Foam (A)", 2D) = "white" {} 
		_SkyTex("SkyMap (Cube)",Cube) = ""{}
		_Color0("Shallow Color", Color) = (1,1,1,1)
		_Color1("Deep Color", Color) = (0,0,0,0)
		_Reflection("Reflection Color",Color) = (1,1,1,1)
		_Specular("Specular", Color) = (0,0,0,0)
		_Shininess("Shininess", Range(0.01, 1.0)) = 1.0
		_Tiling("Tiling", Range(0.025, 0.25)) = 0.25
		_ReflectionTint("Reflection Tint", Range(0.0, 1.0)) = 0.8
		_InvRanges("Inverse Alpha, Depth and Color ranges", Vector) = (1.0, 0.17, 0.17, 0.0)
	}
   
	SubShader{ 	    
		Tags{ "Queue" = "Transparent-10" }

		GrabPass{
			Name "BASE"
			Tags{ "LightMode" = "Always" } 
		}
		Blend Off
		ZTest LEqual
		ZWrite Off 
		Pass{
			Name "FORWARD"
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM			
				#include "WaterRef_0.cginc" 
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#pragma multi_compile_fwdbase 
				//#pragma target 3.0
				//#pragma skip_variants LIGHTMAP_ON SHADOWS_SCREEN VERTEXLIGHT_ON DYNAMICLIGHTMAP_ON DIRECTIONAL
			ENDCG		
		}		
	}

	//SubShader{ 	     
	//	Tags{ "Queue" = "Transparent-10" }	 
	//	Blend SrcAlpha OneMinusSrcAlpha
	//	ZTest LEqual
	//	ZWrite Off

	//	Pass{
	//		Name "FORWARD"
	//		Tags {"LightMode" = "ForwardBase"}
	//		CGPROGRAM			
	//			#include "WaterRef_1.cginc" 
	//			#pragma vertex vert
	//			#pragma fragment frag
	//			#pragma target 2.0
	//			#pragma multi_compile_fog  
	//		ENDCG		
	//	}		
	//}	 
	Fallback Off
}
 