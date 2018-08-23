Shader "Pure/MultiPlane/Additive" {
	Properties {
	
		[Enum (UnityEngine.Rendering.CullMode)] _Cull("Cull Mode",Float)=2
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		 
		
		[Toggle(TRANSFER_UV)]_TransformUV ("Transform UV",Float) = 0	
		[KeywordEnum(Normal, Multiply,Screen,Add, Overlay,Multi_All)] _Blend ("Blend2D(PS Blend)" ,Float)=0 	
		[KeywordEnum(L2,L3,L4)] _Tex ("Number Layers" ,Float)=0   

	
		[HideInInspector]_Tex_0 ("Texture 0", 2D) = "white" {}
		[HideInInspector]_Rotate_0 ("Rotate_0", Float ) = 1.0
		[HideInInspector]_Offset_0 ("Offset_0(x,y)/Scale(z,w)", vector) = (0,0,1,1) 
		[HideInInspector]_Pivot_0("Pivot_0(x,y)",vector) = (0,0,0,0)
	
		[HideInInspector]_Tex_1 ("Texture 1", 2D) = "white" {}
		[HideInInspector]_Rotate_1 ("Rotate_1", Float ) = 1.0
		[HideInInspector]_Offset_1 ("Offset_1(x,y)/Scale(z,w)", vector) = (0,0,1,1) 
		[HideInInspector]_Pivot_1("Pivot_1(x,y)",vector) = (0,0,0,0)
	
		[HideInInspector]_Tex_2 ("Texture 2", 2D) = "white" {}
		[HideInInspector]_Rotate_2 ("Rotate_2", Float ) = 1.0
		[HideInInspector]_Offset_2 ("Offset_2(x,y)/Scale(z,w)", vector) = (0,0,1,1) 
		[HideInInspector]_Pivot_2("Pivot_2(x,y)",vector) = (0,0,0,0)
	
		[HideInInspector]_Tex_3 ("Texture 3", 2D) = "white" {}
		[HideInInspector]_Rotate_3 ("Rotate_3", Float ) = 1.0
		[HideInInspector]_Offset_3 ("Offset_3(x,y)/Scale(z,w)", vector) = (0,0,1,1) 
		[HideInInspector]_Pivot_3("Pivot_3(x,y)",vector) = (0,0,0,0)


		[Space(10)]
		[Toggle(DISSOLVE_ON)] _dissolve ("Dissolve Enabled",Float)=0
		
		[HideInInspector]_DissolveMap("DissolveMap",2D) = "white"{}		
		[HideInInspector]_DissolveColor("Dissolve Color", Color)= (1,1,1,1)
		[HideInInspector]_DissolveEdgeColor("Dissolve EdgeColor", Color)= (1,1,1,1)
		[HideInInspector]_DissolveThreshold("DissolveThreshhold",Range(0,1)) =0
		[HideInInspector]_ColorEdge("ColorFactor",Range(0,1)) =0.1
		[HideInInspector]_DissolveEdge("DissolveEdge",Range(0,1)) =0.8

	}

	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
		Blend SrcAlpha One
		ColorMask RGB
		Cull [_Cull]
		Lighting Off ZWrite Off
	
		SubShader { 
			Pass {		
				CGPROGRAM
					#include "mp_diffuse.cginc"
					#pragma vertex vert
					#pragma fragment frag
					#pragma target 2.0 
					#pragma multi_compile_particles
					#pragma multi_compile_fog			
					#pragma multi_compile _ FOG_Y_AXIS
					#pragma multi_compile P_ADD	
					#pragma multi_compile _ TRANSFER_UV  
					#pragma multi_compile _ DISSOLVE_ON
					#pragma multi_compile _BLEND_NORMAL _BLEND_MULTIPLY  _BLEND_SCREEN _BLEND_ADD _BLEND_OVERLAY _BLEND_MULTI_ALL
					#pragma multi_compile _TEX_L2 _TEX_L3 _TEX_L4 
				ENDCG
			}
		}	
	}	
	CustomEditor "InspShader_MultiPlane"
	FallBack Off
}
 