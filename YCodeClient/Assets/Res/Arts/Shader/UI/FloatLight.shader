Shader "UI/FloatLight"{
	Properties{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1, 1, 1, 1)
        [MaterialToggle] PixelSnap("Pixel snap", float) = 0

        /* Flowlight */
        _FlowlightColor("Flowlight Color", Color) = (1, 0, 0, 1)
        _Lengthlitandlar("LangthofLittle and Large", range(0,0.5)) = 0.005
        _MoveSpeed("MoveSpeed", float) = 5
        _Power("Power", float) = 1
        _LargeWidth("LargeWidth", range(0,0.005)) = 0.0035
        _LittleWidth("LittleWidth", range(0,0.001)) = 0.002
        /* --------- */
            _WidthRate("WidthRate",float) = 0
            _XOffset("XOffset",float) = 0
            _HeightRate("HeightRate",float) = 0
            _YOffset("YOffset",float) = 0

        /* UI */
        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
	}
	SubShader{
		Tags { 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		} 
		Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
        ColorMask[_ColorMask]

		Stencil {
            Ref			[_Stencil]
            Comp		[_StencilComp]
            Pass		[_StencilOp]
            ReadMask	[_StencilReadMask]
            WriteMask	[_StencilWriteMask]
        }

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag  
			#include "UnityCG.cginc"
		     struct appdata_t{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
				float4 worldPosition: TEXCOORD1;
			};

			fixed4 _Color;  
			
			float _Power;
			float _LargeWidth;
			float _LittleWidth;
			float _Lengthlitandlar;
			float _MoveSpeed;
			fixed4 _FlowlightColor; 
			float _UVPosX;

			v2f vert(appdata_t o){
				v2f v;
				v.worldPosition = o.vertex;
				v.vertex = UnityObjectToClipPos(o.vertex);
				v.texcoord = o.texcoord;
				v.color = o.color * _Color;
				#ifdef PIXELSNAP_ON
					v.vertex = UnityPixelSnap(v.vertex);
				#endif
				return v;
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _WidthRate;
			float _XOffset;
			float _HeightRate;
			float _YOffset;
			 
			bool _UseClipRect;
			float4 _ClipRect;
			float _ClipSoftX;
			float _ClipSoftY;
			fixed4 frag(v2f IN) : SV_Target{
				fixed4 c = tex2D(_MainTex, IN.texcoord); 
				if (_UseClipRect){
					float2 factor = float2(0.0, 0.0);
					float2 tempXY = (IN.worldPosition.xy - _ClipRect.xy) / float2(_ClipSoftX, _ClipSoftY)*step(_ClipRect.xy, IN.worldPosition.xy);
					factor = max(factor, tempXY);
					float2 tempZW = (_ClipRect.zw - IN.worldPosition.xy) / float2(_ClipSoftX, _ClipSoftY)*step(IN.worldPosition.xy, _ClipRect.zw);
					factor = min(factor, tempZW);
					c.a *= clamp(min(factor.x, factor.y), 0.0, 1.0);
				} 
							 
				_UVPosX = _XOffset +(fmod(_Time.x*_MoveSpeed, 1) * 2 -0.5)* _WidthRate; 
				_UVPosX += IN.texcoord.y * _HeightRate;
				//_UVPosX +=IN.texcoord.y*2;
				float d0 =1 - _LargeWidth*_WidthRate;				
				float lar =d0*d0;

				d0 = 1 - _LittleWidth*_WidthRate;
				float lit = d0*d0;
				
				d0 = _UVPosX - IN.texcoord.x; 
				fixed4 cadd = _FlowlightColor * saturate((1 - saturate(d0*d0)) - lar)*_Power /(1-lar);

				d0= _UVPosX - _Lengthlitandlar*_WidthRate - IN.texcoord.x;				
				cadd += _FlowlightColor* saturate((1 - saturate(d0*d0)) - lit)*_Power/ (1-lit);

				cadd.rgb *= cadd.a;
				c.rgb += cadd.rgb;
				c.rgb *= c.a;

				return c;
			}
			ENDCG
		}
	}
}
