#ifndef DEPTH_ONLY_CG
#define DEPTH_ONLY_CG
  
#include "UnityCG.cginc" 		 
	 
	struct v2f {		
		half4 pos :SV_POSITION	 
	};
	 
	v2f vert (float4 vertex:POSITION) { 
		v2f o;  	
		o.pos = mul(UNITY_MATRIX_MVP, vertex);
		return o;
	};  

	half4 frag(v2f IN) : SV_Target { 
		return 0
	}  
  
 #endif 