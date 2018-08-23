Shader "Pure/Screen/ColorMatrix" {
	Properties { 	  	
		[HideInInspector] _MainTex ("Main Tex",2D) = "white"{}
		 
	}   
     
     
	SubShader {	  
		Pass {
			ZTest Always
			CGPROGRAM
				#include "UnityCG.cginc"
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma target 3.0
			 
				uniform sampler2D _MainTex;
				uniform float2 _MainTex_TexelSize;
				float4 _MainTex_ST; 

				struct vertexInput	{
					float4 vertex : POSITION; 
					float2 texcoord : TEXCOORD0;
				};
			 
				struct v2f{
					half2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION; 
				};


				v2f vert(vertexInput i){
					v2f o; 
					o.vertex = mul(UNITY_MATRIX_MVP, i.vertex); 		 
					o.uv =TRANSFORM_TEX( i.texcoord,_MainTex); 
					return o;
				}
				uniform float4x4 _Matrix; 
				uniform float4 _Offset; 

				fixed4 frag(v2f i):COLOR{
					float2 uv =i.uv; 			 
					fixed4 col = tex2D(_MainTex,uv.xy);
					col =mul(col, _Matrix)  + _Offset; 
					return col;  
				}
			 
			ENDCG
		 
		}
	}
	Fallback OFF 
	 
}
 