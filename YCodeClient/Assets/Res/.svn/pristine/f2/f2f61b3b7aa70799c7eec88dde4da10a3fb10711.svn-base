Shader "Pure/Skinnable/Bump" {
	Properties { 	  
	
		[HideInInspector] _Color("Color", Color)= (1,1,1,1) 
		[HideInInspector] _SpecColor("Spec Color",Color) =(0,0,0,0)
		[HideInInspector] _Shininess("shininess",Range(0.03,1))=0.5
		

		[HideInInspector] _Emission0("Emission 0",Range(0,5)) =0
		[HideInInspector] _Emission1("Emission 1",Range(0,5)) =0
		[HideInInspector] _Emission2("Emission 2",Range(0,5)) =0
		[HideInInspector] _Emission3("Emission 3",Range(0,5)) =0
		[HideInInspector] _Emission4("Emission 4",Range(0,5)) =0
		
		[HideInInspector] _ColorT_0("Color Matrix 0" ,Vector) =(0,0,0,0)
		[HideInInspector] _ColorT_1("Color Matrix 1" ,Vector) =(0,0,0,0)
		[HideInInspector] _ColorT_2("Color Matrix 2" ,Vector) =(0,0,0,0)
		[HideInInspector] _ColorT_3("Color Matrix 3" ,Vector) =(0,0,0,0)
		[HideInInspector] _ColorT_4("Color Matrix 4" ,Vector) =(0,0,0,0)


		[HideInInspector] _MainTex ("Main Tex",   2D) = "white" {} 	 
		[HideInInspector] _Control ("Control Tex",2D) = "white"{}  
		[HideInInspector] _BumpTex ("Bump Tex",   2D) = "bump" {}   

		[KeywordEnum(BASE,L0,L1,L2,L3,L4)] _SKIN ("SkinMode", Float)=0

		[Space(10)]
		[HideInInspector][Toggle(DISSOLVE_ON)] _dissolve ("Dissolve Enabled",Float)=0 
		[HideInInspector][NoScaleOffset] _DissolveMap("Dissolve Map",2D) = "black"{}		
		[HideInInspector]_DissolveColor("Dissolve Color", Color)= (1,1,1,1)
		[HideInInspector]_ColorEdge("Color Edge",Range(0,1)) =0.8

		[HideInInspector]_DissolveEdgeColor("Dissolve Color", Color)= (1,1,1,1)
		[HideInInspector]_DissolveEdge("Dissolve Edge",Range(0,1)) =0.1

		[HideInInspector]_DissolveThreshold("Dissolve Threshhold",Range(0,1)) =0		
		[HideInInspector][Toggle(UV2_ON)] _DissolveUV2 ("Use uv2",Float)=0 

		[HideInInspector][Toggle(RIM_ON)] _rim ("RimLight Enabled",Float)=0 
		[HideInInspector]_RimColor ("RimColor", Color) = (0,0,0,0)
		[HideInInspector]_RimPower("Rim Power",Range(0.000001,3.0)) =0.1


		
		[HideInInspector][Toggle(REFLECT_ON)] _reflect ("Reflect Enabled",Float)=0 		
		[HideInInspector] _Reflection ("Reflect Control",   2D) = "white" {} 	  
		[HideInInspector] _Cube ("Cube Tex",   Cube) = "" {} 	  

		
		[HideInInspector] _Alpha("Alpha",Range(0,1))=1 
					
		[Enum(UnityEngine.Rendering.BlendMode)] _Source ("Blend Source",Float)=5
		[Enum(UnityEngine.Rendering.BlendMode)] _Dest ("Blend Dest",Float)=10 		
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode",Float)=2
		[Enum(UnityEngine.Rendering.CompareFunction)]_ZTest("ZTest",Float)=2
		[Toggle] _ZWrite ("ZWrite",Float)=1		
	}   
     
    
	SubShader {	 
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
			"RenderTarget"="Skinnable"
		}   

		Blend [_Source] [_Dest]
		Cull [_Cull]
		ZTest [_ZTest]
		ZWrite [_ZWrite]

		Pass {
			Name "FORWARD"
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM 
				#include "skin_bump.cginc"
				#pragma vertex vert
				#pragma fragment frag   
				#pragma multi_compile _SKIN_BASE _SKIN_L0 _SKIN_L1 _SKIN_L2 _SKIN_L3 _SKIN_L4   
				#pragma multi_compile _ DISSOLVE_ON
				#pragma multi_compile_fog	
				#pragma multi_compile _ UV2_ON
				#pragma multi_compile _ RIM_ON 		
				#pragma multi_compile _ REFLECT_ON 		 
			ENDCG   
		}
	}
	 
	Fallback OFF 	
	CustomEditor "InspShader_MultiSkin"
}
 