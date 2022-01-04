Shader "Custom/OutlineShader"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth ("Outline Width", Range(1.0,2.0)) = 1.01
	}

	CGINCLUDE

		#include "UnityCG.cginc"
		#include "AutoLight.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			
		};

		struct v2f
		{
			float4 pos : POSITION;
			float4 color : COLOR;
			float3 normal : NORMAL;
			LIGHTING_COORDS(0, 1)
		};

		float _OutlineWidth;
		float4 _OutlineColor;

		v2f vert(appdata v)
		{   
			
			
			v.vertex.xyz *= _OutlineWidth;
			
			v2f o;
			
			o.color = _OutlineColor;
			o.pos = UnityObjectToClipPos(v.vertex);
			
			//UNITY_TRANSFER_FOG(o, o.pos);

			return o;
		}


	ENDCG



	SubShader
	{

		Tags{ "RenderType" = "Opaque" }
		//Tags{ "LightMode" = "ForwardBase" }
		LOD 100
		Pass 
		{
			
			ZWrite off
			
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#pragma multi_compile_fwdbase
				#include "AutoLight.cginc"

				fixed4 frag(v2f i) : COLOR
				{

					float attenuation = LIGHT_ATTENUATION(i);
					return fixed4(1.0, 0.0, 0.0, 1.0) * attenuation, i.color;
					
				}
		

			ENDCG
		}
		Pass
		{
			ZWrite on

			Material 
			{
					Diffuse[_Color]
					Ambient[_Color]
			}

				Lighting on


			SetTexture[_MainTex]
			{
					ConstantColor[_Color]
			}
			SetTexture[_MainText]
			{
					Combine previous * primary DOUBLE
			}
		}


	}
		Fallback "VertexLit"
}
