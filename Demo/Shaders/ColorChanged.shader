Shader "Unlit/ColorChanged"
{
    Properties{
        _MainColor("MainColor", color) = (1,0,0,1)     //第一种颜色：绿
        _SecondColor("SecondColor", color) = (1,0,0,1) //第二种颜色：红
        //贴图
        _MainTex("MainTex (RGB)", 2D) = "white" {}
    //Hue的值范围为0-359. 其他两个为0-1 ,这里我们设置到3，因为乘以3后 都不一定能到超过.
    _Hue("Hue", Range(0,359)) = 0
    _Saturation("Saturation", Range(0,3.0)) = 1.0
    _Value("Value", Range(0,3.0)) = 1.0

    _YMinimum("Y-Minimum", range(-180, 180.0)) = 0.0
    _YMaximum("Y-Maximum", range(-180, 180.0)) = 0.0
    }

        SubShader{
            Pass {
                Tags { "RenderType" = "Opaque" }
                LOD 200

                Lighting Off

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                half _Hue;
                half _Saturation;
                half _Value;

                float _YMinimum;
                float _YMaximum;
                fixed4 _MainColor;

                struct v2f {
                    float4 pos:POSITION;
                    float3 uv : TEXCOORD0;
                };

                //RGB to HSV
                float3 RGBConvertToHSV(float3 rgb)
                {
                    float R = rgb.x,G = rgb.y,B = rgb.z;
                    float3 hsv;
                    float max1 = max(R,max(G,B));
                    float min1 = min(R,min(G,B));
                    if (R == max1)
                    {
                        hsv.x = (G - B) / (max1 - min1);
                    }
                    if (G == max1)
                    {
                        hsv.x = 2 + (B - R) / (max1 - min1);
                        }
                    if (B == max1)
                    {
                        hsv.x = 4 + (R - G) / (max1 - min1);
                        }
                    hsv.x = hsv.x * 60.0;
                    if (hsv.x < 0)
                        hsv.x = hsv.x + 360;
                    hsv.z = max1;
                    hsv.y = (max1 - min1) / max1;
                    return hsv;
                }

                //HSV to RGB
                float3 HSVConvertToRGB(float3 hsv)
                {
                    float R,G,B;
                    //float3 rgb;
                    if (hsv.y == 0)
                    {
                        R = G = B = hsv.z;
                    }
                    else
                    {
                        hsv.x = hsv.x / 60.0;
                        int i = (int)hsv.x;
                        float f = hsv.x - (float)i;
                        float a = hsv.z * (1 - hsv.y);
                        float b = hsv.z * (1 - hsv.y * f);
                        float c = hsv.z * (1 - hsv.y * (1 - f));
                        switch (i)
                        {
                            case 0: R = hsv.z; G = c; B = a;
                                break;
                            case 1: R = b; G = hsv.z; B = a;
                                break;
                            case 2: R = a; G = hsv.z; B = c;
                                break;
                            case 3: R = a; G = b; B = hsv.z;
                                break;
                            case 4: R = c; G = a; B = hsv.z;
                                break;
                            default: R = hsv.z; G = a; B = b;
                                break;
                        }
                    }
                    return float3(R,G,B);
                }

                v2f vert(appdata_base v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    return o;
                }

                fixed4 frag(v2f IN) : COLOR//SV_Target
                {

                    float y = IN.uv.y;
                    float g = lerp(_YMaximum, _YMinimum, y);
                    float3 colorHSV;
                    float4 original;
                    original = _MainColor;
                    colorHSV.xyz = RGBConvertToHSV(original.xyz);           //转换为HSV
                    colorHSV.x = (g + _Hue) % 360;                          //调整偏移Hue值
                                                                                                //超过360的值从0开始

                    colorHSV.y *= _Saturation;                              //调整饱和度
                    colorHSV.z = _Value;
                    original.xyz = HSVConvertToRGB(colorHSV.xyz);           //将调整后的HSV，转换为RGB颜色

                    return original;
                }
                ENDCG
        }
    }
        FallBack "Diffuse"
}
