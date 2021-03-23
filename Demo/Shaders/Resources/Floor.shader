Shader "MicroLight/Floor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
              
                return o;
            }
			float FrontWidth;
			float IntersectionAngle;
			float RightWidth;
            fixed4 frag (v2f i) : SV_Target
            {
				  float Angles = IntersectionAngle - 90;
				  float x = sin(Angles * 0.0174532924F) *RightWidth;
				  float len = 2 * x;
				  float step = len / _ScreenParams.y;
				  float2 nuv = i.uv - float2(0.5f, 0.5f);
				  nuv.x = nuv.x * (i.vertex.y*step + FrontWidth) / (FrontWidth+ len);
				  nuv+= float2(0.5f, 0.5f);
           
				  // sample the texture
					 fixed4 col = tex2D(_MainTex, nuv);
            
                return col;
            }
            ENDCG
        }
    }
}
