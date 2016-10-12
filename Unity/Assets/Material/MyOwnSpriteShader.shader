Shader "Custom/MyOwnSpriteShader" {
	Properties {
		_Brightness ("Brightness", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Counter ("Counter",float) = 1
		_TimeSpeed ("Time Speed", float) = 1
	}
	SubShader {
		Tags 
	 	{ 
	     "RenderType" = "Opaque"
	     "Queue" = "Transparent"
	 	}
		//Tags { "Queue" = "Geometry-1" }  // Write to the stencil buffer before drawing any geometry to the screen
		Pass {
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			//user defined variables
			uniform float4 _Brightness;
			uniform float _Counter;
			uniform float _TimeSpeed;
			
			sampler2D _MainTex;
			
			struct vertexInput{
				float4 vertex : POSITION;
				float4 texcoord0 : TEXCOORD0;
			};
			struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 texcoord0 : TEXCOORD0;
			};
			
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				_Counter += 1;
				v.vertex.y += sin(_Time*_TimeSpeed+v.vertex.x)*_Counter;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord0 = v.texcoord0;
				return o;
			}
			
			float4 frag(vertexOutput i) : COLOR {
				fixed4 texcol = tex2D (_MainTex, i.texcoord0);
            	return texcol+_Brightness;
				//return _Color;
			}
			
			ENDCG
		}
	} 
	//FallBack "Diffuse"
}
