#ifndef DISSOLVE_TOOLS_CG
#define DISSOLVE_TOOLS_CG
 
	#if DISSOLVE_ON
		sampler2D _DissolveMap; 
		half4 _DissolveColor; 
		half4 _DissolveEdgeColor; 
		half _DissolveThreshold;
		half _ColorEdge ; 
		half _DissolveEdge; 


		inline fixed4 fragDissolve(half2 uv , fixed4 color ){ 
			fixed4 c = color;   
			fixed4  dissolveValue = tex2D(_DissolveMap, uv); 			 
			half a = saturate(c.a);
			if(dissolveValue.r*a <=_DissolveThreshold){
				discard;
			}

			half percent = _DissolveThreshold / dissolveValue.r; 
			fixed lerpEdge = sign (percent - _ColorEdge - _DissolveEdge); 
			fixed3 edgeColor = lerp (_DissolveEdgeColor.rgb,_DissolveColor.rgb, saturate(lerpEdge)); 
		 
			fixed lerpOut = sign(percent - _ColorEdge); 
			fixed3 colorOut = lerp (color.rgb ,edgeColor,saturate(lerpOut)); 
			c.rgb = colorOut.rgb; 
			 			 
			return c; 
		}
	#endif
		  
#endif 