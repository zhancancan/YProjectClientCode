#ifndef TERRAIN_STRUCT_CG
#define TERRAIN_STRUCT_CG
 
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc" 


	


	float4 _Control_ST;
	float4 _Splat0_ST;
	float4 _Splat1_ST;
	# if TEX_3||TEX_4	
		float4 _Splat2_ST;
	#endif
	# if TEX_4
		float4 _Splat3_ST;
	#endif

			 
   

	
	 # ifndef LIGHTMAP_ON
	 struct v2f_surf {
			float4 pos : SV_POSITION;
			float4 pack0 : TEXCOORD0; // _Control _Splat0 
			float4 pack1 : TEXCOORD1; // _Splat1 _Splat2 
			float4 pack2 : TEXCOORD2;// _Splat3

			half3 worldNormal : TEXCOORD3;
			float3 worldPos : TEXCOORD4;
			 

			#if UNITY_SHOULD_SAMPLE_SH
				half3 sh : TEXCOORD5;  
			#endif  

			SHADOW_COORDS(6)

			UNITY_FOG_COORDS(7) 

			float4 lmap : TEXCOORD8; 
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO
		};
	#endif	 

	#ifdef LIGHTMAP_ON
		struct v2f_surf {
			half4 pos : SV_POSITION;
			half4 pack0 : TEXCOORD0; // _Control _Splat0				 
			half4 pack1 : TEXCOORD1; // _Splat1 _Splat2 
			half4 pack2 : TEXCOORD2;// _Splat3

			half3 worldNormal : TEXCOORD2;
			half3 worldPos : TEXCOORD3;
			half4 lmap : TEXCOORD4;
			SHADOW_COORDS(5)
			UNITY_FOG_COORDS(6)
			#ifdef DIRLIGHTMAP_COMBINED
				half3 tSpace0 : TEXCOORD7;			
				half3 tSpace1 : TEXCOORD8;		
				half3 tSpace2 : TEXCOORD9;						 
			#endif
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO
		};
	 #endif
	   

	
		

	 inline void packSurface(inout v2f_surf o, appdata_full v){
	 
		
		o.pos = UnityObjectToClipPos(v.vertex);			

		#if TEX_2
			o.pack0.xy = TRANSFORM_TEX(v.texcoord, _Control);
			o.pack0.zw = TRANSFORM_TEX(v.texcoord, _Splat0); 
			o.pack1.xy = TRANSFORM_TEX(v.texcoord, _Splat1); 
		
		#elif TEX_3
			o.pack0.xy = TRANSFORM_TEX(v.texcoord, _Control);
			o.pack0.zw = TRANSFORM_TEX(v.texcoord, _Splat0);
			o.pack1.xy = TRANSFORM_TEX(v.texcoord, _Splat1); 
			o.pack1.zw = TRANSFORM_TEX(v.texcoord, _Splat2);

		#elif TEX_4
			o.pack0.xy = TRANSFORM_TEX(v.texcoord, _Control);
			o.pack0.zw = TRANSFORM_TEX(v.texcoord, _Splat0);
			o.pack1.xy = TRANSFORM_TEX(v.texcoord, _Splat1);
			o.pack1.zw = TRANSFORM_TEX(v.texcoord, _Splat2);
			o.pack2.xy = TRANSFORM_TEX(v.texcoord, _Splat3);
		#endif
	
	}
	 
		
	inline void resetInput(inout Input surfIN, v2f_surf IN){ 

		surfIN.worldPos.x = 1.0;   
		surfIN.uv_Control = IN.pack0.xy;
		#if TEX_2
		
			surfIN.uv_Splat0 = IN.pack0.zw;
			surfIN.uv_Splat1 = IN.pack1.xy; 

		#elif TEX_3
		
			surfIN.uv_Splat0 = IN.pack0.zw;
			surfIN.uv_Splat1 = IN.pack1.xy;
			surfIN.uv_Splat2 = IN.pack1.zw;

		#elif TEX_4

			surfIN.uv_Splat0 = IN.pack0.zw;
			surfIN.uv_Splat1 = IN.pack1.xy;
			surfIN.uv_Splat2 = IN.pack1.zw;
			surfIN.uv_Splat3 = IN.pack2.xy;

		#endif

	}




#endif 