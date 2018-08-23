Shader "Pure/Water/DepthSplit"{
 
	SubShader{ 	    
		Tags{ 
				"Queue" = "Transparent-10"
				"RenderType" = "Opaque"
			} 	 
		Blend Off
		ZTest LEqual
		ZWrite Off 
		Pass{		 
			CGPROGRAM	 
			//#pragma glsl
			#pragma fragmentoption ARB_precision_hint_nicest
			#pragma target 3.0
			#include "unityCG.cginc" 

			#pragma vertex vert
			#pragma fragment frag

			sampler2D_float _CameraDepthTexture;
			struct d2f{ 
				float4 pos :SV_POSITION;
				float4 scrPos:TEXCOORD0; 
			};
			 
			 d2f vert(appdata_base v){ 
				d2f o;
				o.pos = mul(UNITY_MATRIX_MVP,v.vertex); 
				o.scrPos = ComputeScreenPos(o.pos);
				return o;
			 }

			float4 frag(d2f i) :COLOR{ 			 			
				float s = 	tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r;  
				float depthValue = 1 - Linear01Depth(s);
				return float4(depthValue, depthValue, depthValue, 1.0f);
			}	 
							 
			ENDCG		
		}		
	}

	 
	Fallback Off
}
 