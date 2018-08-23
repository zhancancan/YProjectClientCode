#ifndef SKINNABLE_MULTI_VERTEX_LITE_CG 
#define SKINNABLE_MULTI_VERTEX_LITE_CG
   
	#include "UnityCG.cginc" 
	#include "Lighting.cginc" 
	#include "skinmerge.cginc" 
  
	#include "../Core/dissolve_tools.cginc" 
	#include "../Core/fog_tools.cginc" 
	#include "../Core/rimlight_tools.cginc" 
  
    struct v2f{
        float2 uv : TEXCOORD0;// xy: uv, z:specular_ceoff;
        float4 vertex : SV_POSITION; 

        fixed4 color : COLOR0;  	    
		fixed3 specColor:COLOR1; 
		UNITY_FOG_COORDS(2)  
		#if RIM_ON
			fixed4 rimColor:COLOR2;
		#endif

		#if REFLECT_ON
			fixed3 worldRefl :COLOR3;  
		#endif

    }; 
	

	half _Shininess;	
	half4 _Color;  
	half _Emission0;
	half _Alpha;

    v2f vert (appdata_base v){
        v2f o;  
        o.vertex = UnityObjectToClipPos(v.vertex);;
        o.uv.xy = v.texcoord.xy;   

        half3 worldNormal = UnityObjectToWorldNormal(v.normal);  
		half3 lightDir= normalize(_WorldSpaceLightPos0.xyz);


        half nl = max(0, dot(worldNormal, lightDir));
		o.color.rgb  = _Emission0;
        o.color.rgb +=nl* _LightColor0.rgb *_Color*0.5;  

		o.color.a=_Color.a; 
		 

		fixed3 viewDir = normalize(WorldSpaceViewDir(v.vertex)); 	
		half3 h = normalize (lightDir + viewDir);
		float nh = max (0, dot (worldNormal, h));  
		half spec = saturate(pow (nh, _Shininess*128.0));
		o.specColor =_SpecColor *  spec* _LightColor0.rgb *0.5; 
		
		#if RIM_ON
			o.rimColor = vertRimFactor(viewDir,worldNormal);			
		#endif	

		#if REFLECT_ON
			o.worldRefl = reflect(-viewDir,worldNormal);	 
		#endif 
		PURE_TRANSFER_FOG(o,v.vertex); 		
		return o;
    }
               
		
	sampler2D _Control;	 
	sampler2D _MainTex;	  
	#if REFLECT_ON 
		samplerCUBE _Cube;
		sampler2D _Reflection;
	#endif
	
    fixed4 frag (v2f i) : SV_Target{	
		half4 control = tex2D(_Control, i.uv.xy);
		half4 source =  tex2D(_MainTex, i.uv.xy);

        fixed4 col = SampleSkin(source,control); 


		fixed4 output = col * i.color; 
		output*=2;
		#if DISSOLVE_ON
			output = fragDissolve(i.uv.xy,output);
		#endif
 		
		#if REFLECT_ON 			
			half3 reflection = texCUBE(_Cube,i.worldRefl) .rgb ; 
			half r =tex2D(_Reflection,i.uv.xy).r ; 
			output.rgb=output.rgb*(1-r)+ reflection.rgb *r; 
 
		#endif
						
		output.rgb += i.specColor* source.a; 
		output.a=_Color.a*_Alpha;

		#if RIM_ON
			output += i.rimColor; 
		#endif
		PURE_APPLY_FOG(i.fogCoord, output);  
        return output;
    }
  

#endif 