Shader "Pure/Unlit/Diffuse CutOff" {
	Properties { 	  	
	
		[Enum (UnityEngine.Rendering.CullMode)] _Cull("Cull Mode",Float)=2
		_MainTex ("Main Tex",   2D) = "white" {} 	  
		_Color("Color", Color)= (1,1,1,1)   
		_CutThreshold("Cut Threshold",Range(0,1))	=0.5	
		[Toggle(VANIM_ON)] _vertexAnim("Vertex Anim",Float)=0	
		_WinDir ("Wind Dir", Vector) = (0.0, -0.7, 0.7, 1)  	  
	} 
          
	SubShader {	 
		Tags { 
			"Queue" = "AlphaTest"
			"IgnoreProjector"="True"
			"RenderType"="TransparentCutout"			
		}   
		Cull[_Cull]
		Pass { 
			Name "FORWARD"
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM 
				#include "simple_unlit.cginc"
				#pragma vertex vert
				#pragma fragment frag    
				#pragma multi_compile CUT_OFF 
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS	
				#pragma multi_compile _ VANIM_ON	
			ENDCG  
		}
	}
	Fallback OFF	 
}
 