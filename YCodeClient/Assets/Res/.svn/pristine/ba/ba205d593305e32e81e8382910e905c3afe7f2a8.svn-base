#ifndef SKINNABLE_MULTI_BUMP_CG 
#define SKINNABLE_MULTI_BUMP_CG
   
	#include "UnityCG.cginc" 
	#include "Lighting.cginc" 
	#include "skinmerge.cginc" 
	#include "../Core/dissolve_tools.cginc" 
	#include "../Core/fog_tools.cginc" 
	#include "../Core/rimlight_tools.cginc" 
  
  
    struct v2f{
        float4 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;  
		fixed4 tSpace0:TEXCOORD1; 
		fixed4 tSpace1:TEXCOORD2; 
		fixed4 tSpace2:TEXCOORD3;   
		UNITY_FOG_COORDS(4)  	 
    }; 
	
    v2f vert (appdata_full v){
        v2f o; 
        o.vertex = UnityObjectToClipPos(v.vertex);;
        o.uv.xy = v.texcoord.xy;   
		#if UV2_ON  
			o.uv.zw = v.texcoord1.xy; 
		#else 
			o.uv.zw=0;
		#endif
		 
		float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
		fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
		fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;

		o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
		o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
		o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
	 
		PURE_TRANSFER_FOG(o,v.vertex);    


        return o;
    }
               
	half4 _Color;  		
	sampler2D _Control;	 
	sampler2D _MainTex;	  
	sampler2D _BumpTex;	  
	half _Shininess;	
	half _Emission0;
	half _Alpha;


	#if REFLECT_ON 
		samplerCUBE _Cube;
		sampler2D _Reflection; 
	#endif



    fixed4 frag (v2f i) : SV_Target{
	
		half4 control = tex2D(_Control, i.uv.xy);
		half4 source =  tex2D(_MainTex, i.uv.xy);  
		half sourceAlpha = source.a; 
		source.a = 1; 
		half3 normal =UnpackNormal(tex2D(_BumpTex, i.uv.xy));

        fixed4 col = SampleSkin(source,control);  
		fixed4 output = col*_Color;
		 
		fixed3 worldPos = fixed3(i.tSpace0.w,i.tSpace1.w,i.tSpace2.w);
		
		fixed3 worldN;
		worldN.x = dot(i.tSpace0.xyz, normal);
		worldN.y = dot(i.tSpace1.xyz, normal);
		worldN.z = dot(i.tSpace2.xyz, normal);
		 
		  	
		half3 lightDir= normalize(_WorldSpaceLightPos0.xyz); 
		half nl = max(0, dot(worldN,lightDir)); 
		fixed3 lcolor = nl* _LightColor0.rgb * _Color.rgb*0.5;
		lcolor += _Emission0; 

		output.rgb *= (lcolor *2); 
		 
		output.rgb  = saturate(output.rgb);
		 
		// specular
		fixed3 viewDir =normalize(UnityWorldSpaceViewDir(worldPos));
	 	half3 h = normalize (lightDir + viewDir);
		float nh = max (0, dot (worldN, h));  
		half spec = saturate(pow (nh, _Shininess*128.0))* sourceAlpha;
		half3 sp =_SpecColor *  spec* _LightColor0.rgb ;

		#if DISSOLVE_ON 
			#if UV2_ON
				output = fragDissolve(i.uv.zw,output); 
			#else 
				output = fragDissolve(i.uv.xy,output);
			#endif
		#endif
		 

		#if REFLECT_ON 					 
			half3 worldRefl = reflect(-viewDir, worldN);	
			half3 reflection = texCUBE(_Cube,worldRefl).rgb ; 			
			half r =tex2D(_Reflection,i.uv.xy).r ;
			output.rgb=output.rgb*(1-r)+ reflection.rgb *r; 			   		
		#endif

		output.rgb +=sp;  
		output.a=_Color.a*_Alpha;

		#if RIM_ON
			output=fragRimLight(viewDir,worldN,output);
		#endif

		PURE_APPLY_FOG(i.fogCoord, output); 
        return output;
    }
  

#endif 