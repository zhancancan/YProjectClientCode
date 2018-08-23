#ifndef AUX_COLOR_CG 
#define AUX_COLOR_CG

   


	#if AUX_COLOR_ON 
		sampler2D _AuxTex;
		half4 _AuxTex_ST;
		sampler2D _AUxCtrlTex;  
		half4 _AUxCtrlTex_ST; 
		half _AuxRatio;  
		half _AuxEmission;	 
		
		inline half4 auxColor(fixed4 color ,half2 uv){ 
			half4 c = tex2D(_AuxTex,uv);  
			c.rgb *=(1+ _AuxEmission);

			half4 b = tex2D(_AUxCtrlTex,uv);  
			half p = ((b.r -0.5) + _AuxRatio) *2;
			p =saturate(p);
			half4 d =color; 
		 	d.rgb  = color.rgb*(1-p) + c.rgb * p; 	
			return d;

		}
	#endif
 



#endif 