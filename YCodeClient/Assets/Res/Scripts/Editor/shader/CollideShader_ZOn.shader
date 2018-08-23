Shader "Hidden/CollideEditor/CollideShader_ZOn"{ 

	SubShader{ 
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			ZWrite Off
			ZTest LEQUAL

			Cull Off

			Fog{
			Mode Off
		}
		}
	}
}
