// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "MicroLight/GridView" {
	Properties {
		_LineColor ("Line Color", Color) = (0,0.9,1,1)//00FBFF
		_CellColor ("Cell Color", Color) = (0,0,0,0)
		 _MainTex ("Albedo (RGB)", 2D) = "white" {}
		 _GridSize("Grid Size", Range(1,100)) = 40
		_LineSize("Line Size", Range(0,1)) = 0.08
	 
	}
	SubShader
	{
		Tags { "Queue" = "AlphaTest" "RenderType" = "TransparentCutout" }
		LOD 200

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

			half _Glossiness = 0.0;
			half _Metallic = 0.0;
			float4 _LineColor;
			float4 _CellColor;

			float _GridSize;
			float _LineSize;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				float gsize = floor(_GridSize);

				gsize += _LineSize;

					float2 id;

					id.x = floor(i.uv.x / (1.0 / gsize));
					id.y = floor(i.uv.y / (1.0 / gsize));

					col = _CellColor;

				if (frac(i.uv.x*gsize) <= _LineSize || frac(i.uv.y*gsize) <= _LineSize)
				{
					col = _LineColor;
				}


					return col;
			}

			ENDCG
		}
	}
}
