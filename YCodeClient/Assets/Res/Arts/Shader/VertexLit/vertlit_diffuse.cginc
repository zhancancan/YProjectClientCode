#ifndef VERTEXLIT_DIFFUSE_CG 
#define VERTEXLIT_DIFFUSE_CG
   
	#include "UnityCG.cginc" 
	#include "Lighting.cginc"  
	#include "../Core/fog_tools.cginc"
	#include "../Core/math_tools.cginc"
  
  
    struct v2f{
        float2 uv : TEXCOORD0;// xy: uv, z:specular_ceoff;
        float4 vertex : SV_POSITION; 

        fixed4 color : COLOR0;  	    
		fixed3 specColor:COLOR1; 
		UNITY_FOG_COORDS(4) 
    }; 
	
	half _Shininess;	
	half4 _Color;  
	half _Emission;

    v2f vert (appdata_full v){
        v2f o;   
		fixed3 viewDir ;
		#if VANIM_ON
			half4 off = vertAnim(v.vertex,v.color.r);  
			o.vertex = UnityObjectToClipPos(off);  
			viewDir = normalize(WorldSpaceViewDir(off)); 	
	   		PURE_TRANSFER_FOG(o,off);
		#else 
			o.vertex = UnityObjectToClipPos(v.vertex); 
			viewDir = normalize(WorldSpaceViewDir(v.vertex)); 	
		  	PURE_TRANSFER_FOG(o,v.vertex);
		#endif  
		 
        o.uv.xy = v.texcoord.xy;   

        half3 worldNormal = UnityObjectToWorldNormal(v.normal);  
		half3 lightDir= normalize(_WorldSpaceLightPos0.xyz);


        half nl = max(0, dot(worldNormal, lightDir));
		o.color.rgb  = _Emission;
        o.color.rgb +=nl* _LightColor0.rgb *_Color*0.5;  

		o.color.a=_Color.a; 
		 
		
		half3 h = normalize (lightDir + viewDir);
		float nh = max (0, dot (worldNormal, h));  
		half spec = saturate(pow (nh, _Shininess*128.0));
		o.specColor =_SpecColor *  spec* _LightColor0.rgb *0.5; 
 
		
		return o;
    }
               
		
	sampler2D _Control;	 
	sampler2D _MainTex;	  
	#if CUT_OFF
		half _CutThreshold; 
	#endif

    fixed4 frag (v2f i) : SV_Target{	 
		half4 col =  tex2D(_MainTex, i.uv.xy); 

		#if CUT_OFF
			clip(col.a *_Color.a - _CutThreshold);
		#endif

		 

		fixed4 output = col * i.color; 
		output*=2;
		output.rgb += i.specColor; 
		output.a=1;      
		
		PURE_APPLY_FOG(i.fogCoord, output);
        return output;
    }
  

#endif 