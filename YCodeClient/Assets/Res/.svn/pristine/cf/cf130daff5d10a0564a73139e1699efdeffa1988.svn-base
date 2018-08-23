#ifndef SIMPLE_UNLIT_CG 
#define SIMPLE_UNLIT_CG
    
	#include "UnityCG.cginc"   
	#include "../Core/fog_tools.cginc" 
	#include "../Core/math_tools.cginc"
  
    struct v2f{
        float2 uv : TEXCOORD0;// xy: uv, z:specular_ceoff;
        float4 vertex : SV_POSITION; 	
		UNITY_FOG_COORDS(1)
			  
    }; 
	 
	half4 _Color;   

	sampler2D _MainTex;	 
	half4 _MainTex_ST;

    v2f vert (appdata_full v){
        v2f o;   
		#if VANIM_ON
			half4 off = vertAnim(v.vertex,v.color.r);  
			o.vertex = UnityObjectToClipPos(off);  
		#else 
			o.vertex = UnityObjectToClipPos(v.vertex); 
		#endif  
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex); 		
		PURE_TRANSFER_FOG(o,v.vertex); 
		return o;
    }
               
		  
	#if CUT_OFF
		half _CutThreshold; 
	#endif

    fixed4 frag (v2f i) : SV_Target{	 
		half4 col =  tex2D(_MainTex, i.uv.xy); 
		#if CUT_OFF
			clip(col.a *_Color.a - _CutThreshold);
		#endif  
		col *=_Color; 		
		
		PURE_APPLY_FOG(i.fogCoord, col); // apply fog		 
        return col;
    }
  

#endif 