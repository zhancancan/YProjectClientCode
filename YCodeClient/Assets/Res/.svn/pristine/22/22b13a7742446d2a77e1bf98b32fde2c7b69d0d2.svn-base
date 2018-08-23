#ifndef RIMLIGHT_TOOLS_CG
#define RIMLIGHT_TOOLS_CG
 
	#if RIM_ON
		 half4 _RimColor; 
		 half _RimPower; 


		inline fixed4 fragRimLight(half3 viewDir, half3 normal,fixed4 color ){ 
			half rim = 1- max(0,dot(viewDir,normal));
			color += _RimColor * pow(rim ,1/_RimPower);
			return color; 
		}
		
		inline fixed4 vertRimFactor(half3 viewDir, half3 normal){			  
			  half rim = 1- max(0,dot(viewDir,normal));
			  return _RimColor* pow(rim,1/_RimPower);
		}



	#endif
		  
#endif 