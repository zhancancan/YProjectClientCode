#ifndef VertLit_BUMP_CG 
#define VertLit_BUMP_CG
   
	#include "UnityCG.cginc" 
	#include "Lighting.cginc"  
	#include "../Core/fog_tools.cginc"
	#include "../Core/math_tools.cginc"
  
  
    struct v2f{
        float2 uv : TEXCOORD0;// xy: uv, z:specular_ceoff;
        float4 vertex : SV_POSITION;  
		fixed4 tSpace0:TEXCOORD1; 
		fixed4 tSpace1:TEXCOORD2; 
		fixed4 tSpace2:TEXCOORD3; 
		UNITY_FOG_COORDS(4) 
		

    }; 
	half4 _Color;  		 
	sampler2D _MainTex;	  
	sampler2D _BumpTex;	  
	half _Shininess;	
	half _Emission; 

    v2f vert (appdata_full v){
        v2f o;  
		o.uv.xy = v.texcoord.xy; 
		float3 worldPos ;
		#if VANIM_ON
			half4 off = vertAnim(v.vertex,v.color.r);  
			o.vertex = UnityObjectToClipPos(off); 
			worldPos= mul(unity_ObjectToWorld,off).xyz;
	   		PURE_TRANSFER_FOG(o,off);
		#else 
			o.vertex = UnityObjectToClipPos(v.vertex);
			worldPos= mul(unity_ObjectToWorld, v.vertex).xyz;
	   		PURE_TRANSFER_FOG(o,v.vertex);
		#endif  
		 
		fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
		fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
		fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;

		o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
		o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
		o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
        return o;
    }
               


	#if CUT_OFF
		half _CutThreshold; 
	#endif

    fixed4 frag (v2f i) : SV_Target{	 
		half4 col = tex2D(_MainTex, i.uv.xy);

		#if CUT_OFF
			clip(col.a *_Color.a - _CutThreshold);
		#endif 

		half3 normal =UnpackNormal(tex2D(_BumpTex, i.uv.xy));		
		 
		fixed4 output = col*_Color;		 
		fixed3 worldPos = fixed3(i.tSpace0.w,i.tSpace1.w,i.tSpace2.w);		
		fixed3 worldN;
		worldN.x = dot(i.tSpace0.xyz, normal);
		worldN.y = dot(i.tSpace1.xyz, normal);
		worldN.z = dot(i.tSpace2.xyz, normal); 

		// lambert;		
		half3 lightDir= normalize(_WorldSpaceLightPos0.xyz);
		half nl = max(0, dot(worldN,lightDir)); 
		fixed3 lcolor = nl* _LightColor0.rgb * _Color.rgb*0.5;
		lcolor += _Emission;
		output.rgb *= lcolor *2; 
		 
		output.rgb  =saturate(output.rgb);
		 
		// specular
		fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos)); 
	 	half3 h = normalize (lightDir + viewDir);
		float nh = max (0, dot (worldN, h));  
		half spec = saturate(pow (nh, _Shininess*128.0));
		half3 sp =_SpecColor *  spec* _LightColor0.rgb *0.5; 
		 
		output.rgb +=sp;  
		PURE_APPLY_FOG(i.fogCoord, output);
        return output;
    }
  

#endif 