// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'

Shader "Editor/Project/EventArea" {
	Properties {
		_ShadowTex("ShadowTex", 2D) = "gray" {}     
		_Color("Color", Color)= (1,1,1,1) 
	}
	SubShader {
		Tags { "Queue"="AlphaTest+1" }
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos:POSITION;
				float4 sproj:TEXCOORD0;  
			};

			float4x4 unity_Projector;   

			sampler2D _ShadowTex;  
			uniform half4 _ShadowTex_TexelSize;  
			float4 _Color;

			v2f vert(float4 vertex:POSITION){
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, vertex);
				o.sproj = mul(unity_Projector, vertex); 
				return o;
			}

			float4 frag(v2f i):COLOR{
				
				half4 shadowCol = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.sproj)); 
				if(i.sproj.x<0) return 0; 
				if(i.sproj.x>1)return 0; 
				if(i.sproj.y<0)return 0; 
				if(i.sproj.y>1)return 0; 
				
				half a =shadowCol.a;
				if(a>0)	return shadowCol *_Color; 
				return 0;
				//half a = (shadowCol * maskCol*_Color).a;
				//if(a > 0){
				//	return  float4(1,1,1,1) * (1 -  a);
				//}else{
				//	return float4(1,1,1,1) ;
				//}
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
