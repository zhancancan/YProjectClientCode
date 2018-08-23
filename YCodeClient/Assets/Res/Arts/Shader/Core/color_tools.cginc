#ifndef COLOR_TOOLS_CG 
#define COLOR_TOOLS_CG
 
  
	 
	 inline fixed4 normalColor(fixed4 c0, fixed4 c1){
		return c1 + (c0 * (1- c1.a));
	 }

	inline fixed4 addCompColor(fixed4 t ,fixed4 c0,fixed4 c1 ){ 	 
		return t + c0 * (1-c1.a);
	}


	inline fixed4 darken(fixed4 c0, fixed4 c1 ){  
		fixed4 t=min(c0,c1);
		t*=t.a;
		return addCompColor(t,c0,c1);
	}
	

	inline fixed4 multiply(fixed4 c0, fixed4 c1){	 
		fixed4 t = c0*c1; 
		t.a = c0.a;   
		return addCompColor(t,c0,c1);
	}

	inline fixed4 multiplyAlpha(fixed4 c0 , fixed4 c1){
		return c0 * c1; 
	}
	
	inline fixed4 colorBurn(fixed4 c0, fixed4 c1){  
		fixed4 t = (c0+c1-1)/c1;
		t=saturate(t); 
		return  addCompColor(t,c0,c1);   
	}
	inline fixed4 linearBurn(fixed4 c0, fixed4 c1){ 	
		c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 t = c0+c1-1;
		t=saturate(t); 
		return  addCompColor(t,c0,c1);   
	}

	inline fixed4 lighten(fixed4 c0, fixed4 c1){		
		c0.rgb*=c0.a;c1.rgb*=c1.a;
		return max(c0,c1);
	}
	
	 
	inline fixed4 screen(fixed4 c0 , fixed4 c1){
		c0.rgb*=c0.a;
		c1.rgb*=c1.a;
		return c1 + c0 - (c1*c0);
	}

	inline fixed4 colorDodge(fixed4 c0, fixed4 c1){
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 t = c0/(1-c1); // the formular is c0 + (c1*c0)/(1-c1) => c0/(1-c1);  
		return t;
	}
	// also call add
	inline fixed4 linearDodge(fixed4 c0, fixed4 c1){
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		return c0+ c1;
	}

	
	inline fixed4 lighterColor(fixed4 c0, fixed4 c1){
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed blendLum;
		fixed baseLum;
		fixed minRGB;
		fixed maxRGB; 

		minRGB=min(c1.r,c1.g);	minRGB =min (minRGB,c1.b); 
		maxRGB=max(c1.r,c1.g); 	maxRGB =max (maxRGB,c1.b); 
		blendLum = minRGB+ maxRGB; 

		
		minRGB=min(c0.r,c0.g);	minRGB =min (minRGB,c0.b); 
		maxRGB=max(c0.r,c0.g); 	maxRGB =max (maxRGB,c0.b); 
		baseLum = minRGB + maxRGB; 		 
		if(blendLum>baseLum){
			return c1;
		}else {
			return c0; 
		}
	}



	inline fixed4 overlay(fixed4 c0, fixed4 c1){	
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 high= 1-((1-c1)*(1-c0)*2); 
		fixed4 low = 2*c0*c1; 
		 
		fixed4 t; 
		if(c0.x>=0.5){t.x= high.x;}else {t.x= low.x;}
		if(c0.y>=0.5){t.y= high.y;}else {t.y= low.y;}
		if(c0.z>=0.5){t.z= high.z;}else {t.z= low.z;}
		if(c0.w>=0.5){t.w= high.w;}else {t.w= low.w;}
		
		t=saturate(t);
		return addCompColor(t,c0,c1); 
		return t;
	}



	inline fixed4 softLight(fixed4 c0, fixed4 c1){
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 t = (c0 - 2 * c0*c1 + 2*c1) * c0; 		
		return addCompColor(t,c0,c1); 
	}
	 
	inline fixed4 hardLight(fixed4 c0, fixed4 c1){	
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 t = overlay(c1,c0);		
		return addCompColor(t,c0,c1); 
	}


	inline fixed4 vividLight(fixed4 c0, fixed4 c1){ 
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 low = c0/(1 - (c1*2-1));
		fixed4 high = (c0- 1 + c1*2)/c1*2; 
		fixed4 t;
		if(c1.x<=0.5){t.x= high.x;}else {t.x= low.x;}
		if(c1.y<=0.5){t.y= high.y;}else {t.y= low.y;}
		if(c1.z<=0.5){t.z= high.z;}else {t.z= low.z;}
		if(c1.w<=0.5){t.w= high.w;}else {t.w= low.w;}
		t=saturate(t);
		return addCompColor(t,c0,c1);  
	}


	inline fixed4 linearLight(fixed4 c0, fixed4 c1){
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 t = (c0-1) + c1*2;
		t = saturate(t);
		return addCompColor(t,c0,c1);
	}

	inline fixed4 pinLight(fixed4 c0, fixed4 c1){
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 dark = min(c0, 2* c1); 
		fixed4 lighten = max(c0,2*c1-1); 
		fixed4 t;
		if(c1.x<0.5){t.x= dark.x;}else {t.x= lighten.x;}
		if(c1.y<0.5){t.y= dark.y;}else {t.y= lighten.y;}
		if(c1.z<0.5){t.z= dark.z;}else {t.z= lighten.z;}
		if(c1.w<0.5){t.w= dark.w;}else {t.w= lighten.w;}
		t=saturate(t);
		t.rgb*=t.a;
		return addCompColor(t,c0,c1);

		return t;
	}

	inline fixed4 hardMix(fixed4 c0, fixed4 c1){
		 c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed4 t =min(1-c1,c0); 	
		t=saturate(t); 
		return addCompColor(t,c0,c1);
	}
	inline fixed4 difference(fixed4 c0, fixed4 c1){
		 c0*=c0.a;c1*=c1.a;
		return abs(c0-c1);
	}

	inline fixed4 exclusion(fixed4 c0, fixed4 c1){
		 c0*=c0.a;c1*=c1.a;
		return (-2 * c0 *c1) + c0 + c1;
	}
	inline fixed4 subtract(fixed4 c0, fixed4 c1){
		 c0*=c0.a;c1*=c1.a;
		return (c0-c1);
	}
	inline fixed4 divide(fixed4 c0,fixed4 c1){
		fixed4 t = c0/c1;
		saturate(t);
		t*=c1.a; 
		fixed4 t1=c0 * (1-c1.a);
		return t+t1;
	}

	inline fixed4 average(fixed4 c0, fixed4 c1){
		return (c0+c1)*0.5;
	}
	inline fixed4 reflectColor(fixed4 c0, fixed4 c1){
		return (c0*c0/(1-c1));
	}

	inline fixed4 glowColor(fixed4 c0, fixed4 c1){
		return reflectColor(c0,c1);
	}
	
	inline fixed4 negation(fixed4 c0, fixed4 c1){
		return 1-abs(1-c0-c1);
	}
	
	inline fixed4 phoenix(fixed4 c0, fixed4 c1){
		return min(c0,c1)- max(c0,c1)+1;
	}

	inline fixed4 grainExtract(fixed4 c0, fixed4 c1){
		return (c0-c1 + 0.5);
	}

	inline fixed4 grainMerge(fixed4 c0, fixed4 c1){
		return c0+c1-0.5;
	}



	inline fixed4 darkColor(fixed4 c0, fixed4 c1){
		c0.rgb*=c0.a;c1.rgb*=c1.a;
		fixed blendLum;
		fixed baseLum;
		fixed minRGB;
		fixed maxRGB; 

		minRGB=min(c1.r,c1.g);	minRGB =min (minRGB,c1.b); 
		maxRGB=max(c1.r,c1.g); 	maxRGB =max (maxRGB,c1.b); 
		blendLum = minRGB+ maxRGB; 

		
		minRGB=min(c0.r,c0.g);	minRGB =min (minRGB,c0.b); 
		maxRGB=max(c0.r,c0.g); 	maxRGB =max (maxRGB,c0.b); 
		baseLum = minRGB + maxRGB; 		 
		fixed4 t ;
		if(blendLum<baseLum){t= c1;}else {t= c0;} 
		return addCompColor(t, c0,c1);
	}



	 

	 inline fixed4 blendColor(fixed4 c0, fixed4 c1){ 
		#if CB_Normal
		return c1;
		#endif

		#if  CB_Darken
		return darken(c0,c1);  
		#endif

		#if CB_Multiply 
		return multiply(c0,c1); 
		#endif

		#if CB_ColorBurn					//  no good
		return colorBurn(c0,c1);	
		#endif	

		#if CB_LinearBurn					 // no good
		return  linearBurn(c0,c1);  	 
		#endif
		
		#if CB_DarkColor				 
		return darkColor(c0,c1) ;
		#endif


		#if CB_Lighten						 		 
		return lighten(c0,c1); 
		#endif
		#if CB_Screen						 
		return screen(c0,c1);
		#endif
		 
		#if CB_ColorDodge					
		return colorDodge(c0,c1);
		#endif

		#if CB_LinearDodge						
		return linearDodge(c0,c1);
		#endif

		#if CB_LighterColor				
		return lighterColor(c0,c1);
		#endif



		#if CB_Overlay					 
		return  overlay(c0,c1); 
		#endif

		#if CB_SoftLight						 
		return softLight(c0,c1);   
		#endif

		#if CB_HardLight						 
		return hardLight(c0,c1);  
		#endif
		 
		#if CB_VividLight						// no good
		return vividLight(c0,c1); 		
		#endif

		#if CB_LinearLight					//  no good
		return linearLight(c0,c1);
		#endif
			  
		#if CB_PinLight				 
		return  pinLight(c0,c1); 	 
		#endif

		#if CB_HardMix						// no good
		return hardMix(c0,c1);  	 
		#endif
			 
		#if CB_Difference			 
		return difference(c0,c1);
		#endif

		#if CB_Exclusion				 	 
		return exclusion(c0,c1); 
		#endif

		#if CB_Subtract					 
		return subtract(c0,c1);
		#endif

		#if CB_Divide				 
		return  divide(c0,c1); 		
		#endif 


		
		return fixed4(1,1,1,1);
			 
	 }







#endif 