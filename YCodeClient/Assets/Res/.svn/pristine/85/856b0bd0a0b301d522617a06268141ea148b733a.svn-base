Shader "Pure/Unlit/Diffuse" {
	Properties { 	  
	
		[Enum (UnityEngine.Rendering.CullMode)] _Cull("Cull Mode",Float)=2	
		_MainTex ("Main Tex",   2D) = "white" {} 	  
		_Color("Color", Color)= (1,1,1,1)     
		[Toggle(VANIM_ON)] _vertexAnim("Vertex Anim",Float)=0	
		_WinDir ("Wind Dir", Vector) = (0.0, -0.7, 0.7, 1)  
	}      
    
	SubShader {	 
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}  
		Cull[_Cull]
		Pass {
			Name "FORWARD"
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM 
				#include "simple_unlit.cginc"
				#pragma vertex vert
				#pragma fragment frag    
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS	
				#pragma multi_compile _ VANIM_ON	
			ENDCG  
		}
	}
	Fallback OFF	 
}
 