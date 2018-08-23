#ifndef PATICLE_BLEND_MODE_CG
#define PATICLE_BLEND_MODE_CG

	inline fixed4 pvertColor(fixed4 vcolor ,fixed4 tint){
		fixed4 col=0;
		#if P_NORMAL
			col = vcolor;
		#endif
		#if P_ADD
			col = vcolor;
		#endif

		#if P_MULTIPLY
			col= vcolor;
		#endif

		#if P_ADD_SMOOTH
			col= vcolor;
		#endif

		#if P_ALPHA_BLEND
			col = vcolor* tint;
		#endif

		#if P_BLEND
			col = vcolor;
		#endif

		#if P_MULTIPLY
			col = vcolor;				
		#endif
		#if P_MULTIPLY_DOUBLE 
			col = vcolor;
		#endif

		#if P_PREMULTIPLY_BLEND
			col = vcolor;
		#endif
			 
		return col;
	}


  
	inline fixed4 pfragColor(fixed4 tex, fixed4 vcolor, fixed4 tint){    
		fixed4 col =1; 
		#if P_NORMAL 
			col = vcolor * tex * tint;
		#endif
		#if P_ADD
			col=2.0f * vcolor * tint * tex;
		#endif

		#if P_ADD_MULTIPLY				 
			col.rgb = tint.rgb * tex.rgb * vcolor.rgb * 2.0f;
			col.a = (1 - tex.a) * (tint.a * vcolor.a * 2.0f);  
		#endif

		#if P_ADD_SMOOTH					
			col = vcolor * tex;
			col.rgb *= col.a;
		#endif

		#if P_ALPHA_BLEND
			col = 2.0f * vcolor * tex;
		#endif

		#if P_BLEND
			col = 2.0f * vcolor * tex;
		#endif
				 
		#if P_MULTIPLY
			half4 prev =vcolor* tex; 
			col = lerp(half4(1,1,1,1),prev,prev.a); 
		#endif

		#if P_MULTIPLY_DOUBLE
			col.rgb = tex.rgb * vcolor.rgb * 2;
			col.a = vcolor.a * tex.a;
			col = lerp(fixed4(0.5f,0.5f,0.5f,0.5f), col, col.a);
		#endif

		#if P_PREMULTIPLY_BLEND
			col = vcolor * tex * vcolor.a;
		#endif
 	  
		return col; 
	  
	}
		  
#endif 