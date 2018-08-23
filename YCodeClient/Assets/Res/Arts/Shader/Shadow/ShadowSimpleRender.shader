Shader "Pure/Shadow/SimpleRender" {
	Properties {
	 
	}
	SubShader { 
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
			"RenderTarget" = "Skinnable"
		}  
		Pass {  
		
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f {
				float4 vertex:POSITION; 
			};
 
			v2f vert(float4 v:POSITION){
				v2f o;
				o.vertex=UnityObjectToClipPos(v);  
				return o;
			}

			float4 frag(v2f i):COLOR{				
				 return float4(1,1,1,1);
			}

			ENDCG
		}
	}  
}
 