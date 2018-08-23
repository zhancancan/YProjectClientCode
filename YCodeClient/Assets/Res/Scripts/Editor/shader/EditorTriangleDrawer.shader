Shader "Hidden/Editor/EditorTriangleDrawer"{ 
Properties{
	[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 0
	[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 0
	[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull",Float)=2
}
	SubShader{ 
		Pass
		{	
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite [_ZWrite]
			ZTest [_ZTest]
			Cull [_Cull]
			Fog{Mode Off}
		}
	}
}
