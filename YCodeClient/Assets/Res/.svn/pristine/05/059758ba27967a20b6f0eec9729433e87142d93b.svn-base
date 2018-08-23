#ifndef SIMPLE_MAGMA_CG 
#define SIMPLE_MAGMA_CG
    
	#include "UnityCG.cginc"   
	#include "../Core/fog_tools.cginc" 
	#include "../Core/math_tools.cginc"
  
    struct v2f{
        half4 uv : TEXCOORD0;
        float4 vertex : SV_POSITION; 	
		UNITY_FOG_COORDS(1)			  
    }; 
	 
	half4 _Color;   

	sampler2D _MainTex;	  
	sampler2D _NoiseTex;
	half4 _MainTex_ST;
	half4 _NoiseTex_ST;

    v2f vert (appdata_full v){
        v2f o;   	
		o.vertex = UnityObjectToClipPos(v.vertex); 
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex); 		
		o.uv.zw = TRANSFORM_TEX(v.texcoord, _NoiseTex); 		
		PURE_TRANSFER_FOG(o,v.vertex); 
		return o;
    }
               
	fixed _NoiseSpeed;
	fixed4 _NoiseData;
	fixed _EmissionPower;

    fixed4 frag (v2f i) : SV_Target{	  
		fixed noiseU = tex2D (_NoiseTex, i.uv.zw + _Time.y * fixed2(0.01, 0) * _NoiseSpeed).rgb;
		fixed noiseV = tex2D (_NoiseTex, i.uv.zw + _Time.y * fixed2(0, 0.01) * _NoiseSpeed).rgb;
		fixed2 noiseUV = (_Time.y * _NoiseData.xy + fixed2(noiseU, noiseV) * _NoiseData.z) * _NoiseData.w;
		fixed4 c = tex2D (_MainTex, i.uv.xy + noiseUV) * _Color;
		fixed3 e = c.rgb * _EmissionPower; 
		c =c*_Color; 
		c.rgb += e.rgb;		
		PURE_APPLY_FOG(i.fogCoord, c); // apply fog		 
        return c;
    }
  

#endif 