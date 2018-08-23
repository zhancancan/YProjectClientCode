#ifndef PLANE_T_DIFFUSE_CG 
#define PLANE_T_DIFFUSE_CG
   
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_particles
#pragma multi_compile_fog
#include "UnityCG.cginc"
#include "../Core/color_tools.cginc"
#include "../Core/math_tools.cginc"
#include "../Core/particle_blend_mode.cginc" 
#include "../Core/dissolve_tools.cginc" 
#include "../Core/fog_tools.cginc" 

 
		sampler2D _Tex_0;
		half4 _Tex_0_ST; 	
		half _Rotate_0;
		half4 _Offset_0;  
		half2 _Pivot_0; 
		
		sampler2D _Tex_1;	
		half4 _Tex_1_ST; 	
		half _Rotate_1;
		half4 _Offset_1;  
		half2 _Pivot_1; 

		#if _TEX_L3 || _TEX_L4			
			sampler2D _Tex_2;
			half4 _Tex_2_ST; 	
			half _Rotate_2;
			half4 _Offset_2;  
			half2 _Pivot_2; 
			#if _TEX_L4						
				sampler2D _Tex_3;
				half4 _Tex_3_ST; 	
				half _Rotate_3;
				half4 _Offset_3;  
				half2 _Pivot_3; 
			#endif
		#endif

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
			float4 uv0 : TEXCOORD0;// xy->0, zw ->1

			#if _TEX_L3 || _TEX_L4
				float4 uv1 :TEXCOORD1;// xy->2, zw-3
			#endif

			UNITY_FOG_COORDS(2)  
		};
			
 
  
   
		v2f vert (appdata_t v){
			v2f o;
			UNITY_SETUP_INSTANCE_ID(v); 
			o.vertex = UnityObjectToClipPos(v.vertex);		 
			o.color = pvertColor(v.color,_TintColor);

			#if TRANSFER_UV
				o.uv0.xy = transformCoords(TRANSFORM_TEX(v.texcoord,_Tex_0).xy,_Rotate_0,_Offset_0.xy,_Offset_0.zw,_Pivot_0.xy);
				o.uv0.zw = transformCoords(TRANSFORM_TEX(v.texcoord,_Tex_1).xy,_Rotate_1,_Offset_1.xy,_Offset_1.zw,_Pivot_1.xy);

				#if _TEX_L3 || _TEX_L4
					o.uv1.xy = transformCoords(TRANSFORM_TEX(v.texcoord,_Tex_2).xy,_Rotate_2,_Offset_2.xy,_Offset_2.zw,_Pivot_2.xy);
					#if _TEX_L4
						o.uv1.zw = transformCoords(TRANSFORM_TEX(v.texcoord,_Tex_3).xy,_Rotate_3,_Offset_3.xy,_Offset_3.zw,_Pivot_3.xy);
					#endif
				#endif
			#else 
				o.uv0.xy = TRANSFORM_TEX(v.texcoord,_Tex_0).xy; 
				o.uv0.zw = TRANSFORM_TEX(v.texcoord,_Tex_1).xy; 	
				#if _TEX_L3 || _TEX_L4
					o.uv1.xy = TRANSFORM_TEX(v.texcoord,_Tex_2).xy;
					#if _TEX_L4
						o.uv1.zw = TRANSFORM_TEX(v.texcoord,_Tex_3).xy;
					#endif
				#endif
			#endif
   
			PURE_TRANSFER_FOG(o,v.vertex);
			return o;
		}
		 

		fixed4 blend2D(fixed4 c0 ,fixed4 c1){
			#if _BLEND_NORMAL
				return normalColor(c0,c1);
			#endif 
			#if _BLEND_MULTIPLY
				return multiply(c0,c1);
			#endif 
			#if _BLEND_SCREEN
				return screen(c0,c1);
			#endif 
			#if _BLEND_ADD 
				return linearDodge(c0,c1);
			#endif 
			#if _BLEND_OVERLAY
				return overlay(c0,c1);
			#endif 
			#if _BLEND_MULTI_ALL
				return c0*c1;
			#endif
			return fixed4(1,0,0,1); 
		}


		fixed4 sampleTex(v2f i){
			fixed4 col = tex2D(_Tex_0,i.uv0.xy); 
			col=blend2D(col,tex2D(_Tex_1,i.uv0.zw));
			#if _TEX_L3 || _TEX_L4
				col= blend2D(col,tex2D(_Tex_2,i.uv1.xy));
				#if _TEX_L4 
					col= blend2D(col,tex2D(_Tex_3,i.uv1.zw));
				#endif
			#endif
			return col;
		} 

		fixed4 frag (v2f i) : SV_Target{ 			 	 
			fixed4 tex=sampleTex(i);			
			fixed4 col =pfragColor(tex,i.color,_TintColor); 

			#if DISSOLVE_ON
				col = fragDissolve(i.uv0.xy,col);  
			#endif   
			
			#if P_NORMAL
				PURE_APPLY_FOG_COLOR(i.fogCoord, col, unity_FogColor); 
			#else 
				PURE_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0,0,0,0)); 
			#endif
		 
			return col;
		}  
		 
#endif 