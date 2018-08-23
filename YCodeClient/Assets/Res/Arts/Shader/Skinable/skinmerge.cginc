#ifndef SKIN_MERGER_CG 
#define SKIN_MERGER_CG
     	 
		  
	#if _SKIN_L0|| _SKIN_L1 ||_SKIN_L2 || _SKIN_L3 || _SKIN_L4		
		float4x4 _ColorMatrix0;
		float4 _ColorOffset0;
		#if  _SKIN_L1 ||_SKIN_L2 || _SKIN_L3 || _SKIN_L4	  
			float4x4 _ColorMatrix1;
			float4 _ColorOffset1;
			half _Emission1;
			#if _SKIN_L2 || _SKIN_L3 || _SKIN_L4
				float4x4 _ColorMatrix2;
				float4 _ColorOffset2;				
				half _Emission2;							 
				#if _SKIN_L3 || _SKIN_L4
					float4x4 _ColorMatrix3;
					float4 _ColorOffset3;
					half _Emission3;
					#if _SKIN_L4
						float4x4 _ColorMatrix4;
						float4 _ColorOffset4;
						half _Emission4;
					#endif		
				#endif					
			#endif
		#endif 
	#endif
		 
	inline half4 MergeColor(half4 source,half4 previous, half control, float4x4 m, float4 o ,half emission){  
		half4 t =source;  
		t= mul(t,m)+o; 
		t *=control;  
		t.rgb += t.rgb * emission;
		t.rgb=saturate(t.rgb);
		return t  + previous* (1-control);
	}

	inline half4 SampleSkin(half4 source, half4 control){
		 half4 col =half4(0,0,0,1);
		 // the basic source col; 
		 col = source;	   
		 #if _SKIN_L0||_SKIN_L1||_SKIN_L2||_SKIN_L3||_SKIN_L4
			 col = mul(source, _ColorMatrix0) +_ColorOffset0;  
			 #if _SKIN_L1||_SKIN_L2||_SKIN_L3||_SKIN_L4
				col=MergeColor(source, col, control.r, _ColorMatrix1, _ColorOffset1, _Emission1);			 
				#if _SKIN_L2 || _SKIN_L3 ||_SKIN_L4
					col=MergeColor(source, col, control.g, _ColorMatrix2, _ColorOffset2,_Emission2); 	 
					#if _SKIN_L3 ||_SKIN_L4
						col=MergeColor(source, col, control.b, _ColorMatrix3, _ColorOffset3,_Emission3);		 
						#if _SKIN_L4
							col=MergeColor(source, col, control.a, _ColorMatrix4, _ColorOffset4,_Emission4);							
						#endif	
					#endif					
				#endif
			#endif  
		 #endif		
		return col;
	}
	 


#endif 