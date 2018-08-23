#ifndef PLANE_T_DIFFUSE_CG 
#define PLANE_T_DIFFUSE_CG
   
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_particles
#pragma multi_compile_fog
#include "UnityCG.cginc"
#include "../Core/math_tools.cginc"
#include "../Core/particle_blend_mode.cginc"
#include "../Core/dissolve_tools.cginc"
#include "../Core/fog_tools.cginc"
 
		sampler2D _MainTex;
		float4 _MainTex_ST;
		fixed4 _TintColor;      	

		struct appdata_t {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			half2 texcoord : TEXCOORD0;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
			UNITY_FOG_COORDS(1)
		 
		};
			
  

		#if TRANSFER_UV		
			half _Rotate;
			half4 _Offset;  
			half4 _Pivot;    
		#endif

		v2f vert (appdata_t v){
			v2f o;
			UNITY_SETUP_INSTANCE_ID(v);
			o.vertex = UnityObjectToClipPos(v.vertex);
		 
			o.color = pvertColor(1,_TintColor);
			o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);

			#if SEQUENCE_ON
				o.texcoord = calcSequenceUV(o.texcoord );
			#endif

			#if TRANSFER_UV
				o.texcoord = transformCoords(o.texcoord,_Rotate.x,_Offset.xy,_Offset.zw,_Pivot.xy);
			#endif 

		
			 
			PURE_TRANSFER_FOG(o,v.vertex);        
			return o;
		}

 
	 
			 
		fixed4 frag (v2f i) : SV_Target{  
			half2 uv = i.texcoord;

		
			fixed4 tex=tex2D(_MainTex, i.texcoord);			
			fixed4 col =pfragColor(tex,i.color,_TintColor); 
			#if DISSOLVE_ON                  
				col = fragDissolve(i.texcoord.xy,col);
			#endif
			#if P_NORMAL
				PURE_APPLY_FOG_COLOR(i.fogCoord, col,unity_FogColor); 
			#else
				 PURE_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0,0,0,0)); 
			#endif
 	 		// fog towards black due to our blend mode
			return col;
		}
  

#endif 