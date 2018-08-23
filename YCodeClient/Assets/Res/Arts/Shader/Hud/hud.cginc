﻿#ifndef SIMPLE_UNLIT_CG 
#define SIMPLE_UNLIT_CG
    
		#include "UnityCG.cginc"   
		#include "../Core/fog_tools.cginc"  
  
		struct v2f{
			float4 vertex	: SV_POSITION; 
			float2 uv		: TEXCOORD0;  
			half4 color		: TEXCOORD1;
			UNITY_FOG_COORDS(2)		  
		}; 
	

		sampler2D _MainTex;	 
		half4 _MainTex_ST; 

		sampler2D _AlphaTex;
		half4 _AlphaTex_ST; 

		  
		v2f vert (appdata_full v){
			v2f o;     
			float4 zero = float4(0,0,0,1);			 
			float4 ori	= mul(UNITY_MATRIX_MVP,v.vertex); 
			float4 nv	= v.texcoord1; 			   
			float4 off	= mul(UNITY_MATRIX_P,nv);
			o.vertex	= ori ; 
			o.vertex.xyz+=off;			
			o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex); 	
			o.color = v.color; 
			return o;
		}

 		fixed4 frag (v2f i) : SV_Target{	 
			half4 col =  tex2D(_MainTex, i.uv.xy);  
			half4 alpha = tex2D(_AlphaTex, i.uv.xy); 
			col.a *= alpha.r;
			col*=i.color;   
			return col;
		}
  

#endif 