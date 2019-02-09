Shader "Unlit/CharOutLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
	    Tags {  "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "true" }
	    // No culling or depth
	    Cull Off ZWrite Off 

	    Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
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
			float4 _MainTex_TexelSize;

            fixed4 frag (v2f i) : SV_Target
            { 
				float4 _OutLineColor = float4 (0,0,0 ,1);
				float4 tex = tex2D(_MainTex, i.uv);
				if (tex.a > 0)
				{
					return tex;
				}
				else
				{
					
					float4 oPixelTex1 = tex2D(_MainTex, i.uv + float2(-1, 0) * _MainTex_TexelSize.xy);
					float4 oPixelTex2 = tex2D(_MainTex, i.uv + float2(1, 0) * _MainTex_TexelSize.xy);

					if ((oPixelTex1.x == _OutLineColor.x && oPixelTex1.y == _OutLineColor.y && oPixelTex1.z == _OutLineColor.z && oPixelTex1.w == _OutLineColor.w) == false)
					{
						if (oPixelTex1.w > 0)
						{
							return _OutLineColor;
						}
					}
					if ((oPixelTex2.x == _OutLineColor.x && oPixelTex2.y == _OutLineColor.y && oPixelTex2.z == _OutLineColor.z && oPixelTex2.w == _OutLineColor.w) == false)
					{
						if (oPixelTex2.w > 0)
						{
							return _OutLineColor;
						}
					}
					return tex;
				}
            }
			
			
            ENDCG
        }
		
    }
		Fallback "Sprite/Default"
}
