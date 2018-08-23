Shader "Pure/Bake/DepthOnly" { 
	Properties {} 		
	SubShader {
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}
				
		Cull [_Cull] 
		Pass { 
			Name "ShadowCaster"
			Tags{"LightMode" = "ShadowCaster"}
			ZWrite On ZTest LEqual
			CGPROGRAM   
				#include "bb_shadowCaster.cginc"
				#pragma vertex vert
				#pragma fragment frag  
				#pragma multi_compile_shadowcaster 
			ENDCG  	  
		} 
	} 
	Fallback OFF
}
 