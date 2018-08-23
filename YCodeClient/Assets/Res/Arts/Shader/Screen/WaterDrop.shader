Shader "Pure/Screen/WaterDrop" {
	Properties { 	  	
		[HideInInspector] _MainTex ("Main Tex",2D) = "white"{}
		 _WaterDropTex("WaterDrop",2D) = "white"{}
		
		 _SizeX("SizeX",Range(0,1)) = 1 
		 _SizeY("SizeY", Range(0,7)) = 0.5
		 _DropSpeed("Speed", Range(0,10)) = 3.6
		 _Distortion("Distortion",Range(5,64)) =8

		[HideInInspector]   _CurTime ("Time",Float) = 0
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
				uniform sampler2D _WaterDropTex;
				uniform float _CurTime;
				uniform float _DropSpeed;
				uniform float _SizeX;
				uniform float _SizeY;
				uniform float _Distortion;
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


				fixed4 frag(v2f i):COLOR{
					float2 uv =i.uv; 
					#if UNITY_UV_STARTS_AT_TOP
						if(_MainTex_TexelSize.y <0){
							_DropSpeed =1-_DropSpeed;
						}
					#endif
			 
					float3  rainTex0 = tex2D(_WaterDropTex, float2(uv.x*1.15 * _SizeX,		(uv.y*_SizeY*1.10)  + _CurTime *_DropSpeed*0.15)).rgb/_Distortion;
					float3  rainTex1 = tex2D(_WaterDropTex, float2(uv.x*1.25 * _SizeX-0.1,	(uv.y*_SizeY*1.20)  + _CurTime *_DropSpeed*0.20)).rgb/_Distortion;
					float3  rainTex2 = tex2D(_WaterDropTex, float2(uv.x* _SizeX*0.9,		(uv.y*_SizeY*1.25)  + _CurTime *_DropSpeed*0.32)).rgb/_Distortion;

					float2 cuv= uv.xy - (rainTex0.xy-rainTex1.xy - rainTex2.xy)/3; 
					float3 c = tex2D(_MainTex,cuv.xy).rgb; 
					return fixed4(c.rgb,1.0);
				}
			 
			ENDCG
		 
		}
	}
	Fallback OFF 
	 
}
 