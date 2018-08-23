Shader "Pure/Hud/Image" {
	Properties { 	  	
		_MainTex ("Main Tex",   2D) = "white" {} 	
		_AlphaTex ("Alpha Tex",   2D) = "white" {} 	   
		[Enum (UnityEngine.Rendering.CullMode)] _Cull("Cull Mode",Float)=1
	} 
     
    
	SubShader {	 
		Tags { 
			"Queue" = "Transparent"
			"RenderType" = "Opaque"
			"PreviewType"="Plane"				
			"IgnoreProjector"="True"
		}  		 
		Blend SrcAlpha OneMinusSrcAlpha

		Cull[_Cull]

		Pass {
			Name "FORWARD"
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM 
				#include "hud.cginc"
				#pragma vertex vert
				#pragma fragment frag    
				#pragma multi_compile_fog   
			ENDCG  
		}
	}
	Fallback OFF	 
}
 