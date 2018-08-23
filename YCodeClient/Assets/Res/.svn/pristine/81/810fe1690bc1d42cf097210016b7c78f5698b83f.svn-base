#ifndef MATH_TOOLS_CG 
#define MATH_TOOLS_CG
   
	   /// the rotate is degree 
		inline half2 transformCoords(half2 corrds, half rotate, half2 translate,half2 scale,half2 pivot){
			half rot = radians(rotate); 
			half cc = cos(rot);			half ss = sin(rot);   
			half a = scale.x * cc; 		half b = scale.x * ss; 
			half c = scale.y *-ss; 		half d = scale.y * cc; 
			half tx = translate.x - pivot.x * a - pivot.y * c - pivot.x; 
			half ty = translate.y - pivot.x * b - pivot.y * d - pivot.y; 		 
			half2 t; 
			t.x = a * corrds.x + c * corrds.y + tx; 
			t.y = d * corrds.y + b * corrds.x + ty; 
			return t;  			 
		} 
	  

		#if VANIM_ON 
			half4 _WinDir;
			inline half4 vertAnim(half4 v ,half p){ 
				half3 dir = _WinDir.xyz *_CosTime.w*p;
				v.xyz += dir; 
				return v;
			}
		#endif


		#if SEQUENCE_ON
			half _SizeX;
			half _SizeY; 
			half _SequenceSpeed;

			inline half2 calcSequenceUV(half2 uv){
				int index = floor(_Time.x * _SequenceSpeed);
				int indexY =  index/_SizeX; 
				int indexX = index - indexY *_SizeY;
				half2 tuv = half2 (uv.x /_SizeX, uv.y/_SizeY); 
				tuv.x += indexX/_SizeX;
				tuv.y += indexY/_SizeY; 
				return tuv;
			} 

		#endif
		 



	 
#endif 