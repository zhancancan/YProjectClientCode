Shader "Pure/Plane/Multiply (Double)" {
Properties {
	[Enum (UnityEngine.Rendering.CullMode)] _Cull("Cull Mode",Float)=2

	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {} 

	[HideInInspector][Toggle(TRANSFER_UV)]_TransformUV ("Transform UV",Float) = 0	
	[HideInInspector]_Rotate ("Rotate", Float ) = 1.0
	[HideInInspector]_Offset ("Offset(x,y)/Scale(z,w)", vector) = (0,0,1,1) 
	[HideInInspector]_Pivot("Pivot(x,y)",vector) = (0,0,0,0) 

	[HideInInspector][Toggle(DISSOLVE_ON)] _dissolve ("Dissolve Enabled",Float)=0		
	[HideInInspector]_DissolveMap("DissolveMap",2D) = "white"{}		
	[HideInInspector]_DissolveColor("Dissolve Color", Color)= (1,1,1,1)
	[HideInInspector]_DissolveEdgeColor("Dissolve EdgeColor", Color)= (1,1,1,1)
	[HideInInspector]_DissolveThreshold("DissolveThreshhold",Range(0,1)) =0
	[HideInInspector]_ColorEdge("ColorFactor",Range(0,1)) =0.8
	[HideInInspector]_DissolveEdge("DissolveEdge",Range(0,1)) =0.1
	

	[HideInInspector][Toggle(SEQUENCE_ON)] _sequence ("Sequence On",Float)=0
	[HideInInspector]_SizeX("Column",Range(1,10)) =4 
	[HideInInspector]_SizeY("Row",Range(1,10)) = 4 
	[HideInInspector]_SequenceSpeed("Row",Float) =200

}

	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
		Blend DstColor SrcColor
		ColorMask RGB
		Cull [_Cull]
		Lighting Off ZWrite Off
	
		SubShader {
			Pass {
		
				CGPROGRAM
					#include "plane_diffuse.cginc"
					#pragma vertex vert
					#pragma fragment frag
					#pragma target 2.0
					#pragma multi_compile_particles
					#pragma multi_compile_fog
					#pragma multi_compile _ FOG_Y_AXIS	
					#pragma multi_compile P_MULTIPLY_DOUBLE
					#pragma multi_compile _ TRANSFER_UV
					#pragma multi_compile _ DISSOLVE_ON
					#pragma multi_compile _ SEQUENCE_ON 
				ENDCG 
			}
		}
	}
	
	CustomEditor "InspShader_Plane"
}
 