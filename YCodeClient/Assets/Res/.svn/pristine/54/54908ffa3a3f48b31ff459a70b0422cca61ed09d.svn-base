Shader "Pure/Magma/Diffuse" {
	Properties { 	  
		_Color ("[主颜色]Main Color", Color) = (1,1,1,1)
		_MainTex ("[主纹理]Base (RGB) Gloss (A)", 2D) = "white" {} 
		_EmissionPower ("[自发光强度]EmissionPower", Range(0, 1)) = 0 
		_NoiseTex ("[噪声纹理]NoiseTex", 2D) = "" {}
		_NoiseData ("[噪声数据]uv:xy, power:z rate:w", Vector) = (1, 1, 2, 0.01)
		_NoiseSpeed ("[扭动速度]NoiseSpeed", Range(0, 10)) = 1 
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
				#include "simple_magma.cginc"
				#pragma vertex vert
				#pragma fragment frag    
				#pragma multi_compile_fog 
				#pragma multi_compile _ FOG_Y_AXIS	 
			ENDCG  
		}
	}
	Fallback OFF	 
}
 